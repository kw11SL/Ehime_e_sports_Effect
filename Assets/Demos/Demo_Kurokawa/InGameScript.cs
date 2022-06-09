using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class InGameScript : MonoBehaviourPunCallbacks
{
    private GameObject m_memberListText = null;
    private GameObject m_countDownText = null;
    private GameObject m_resultText = null;
    private float m_countDownNum = 3.0f;
    private int m_prevCountDownNum = 0;

    private int m_goaledPlayerNum = 0;
    private float[] m_playerGoaledTime = new float[4]{ 0.0f, 0.0f, 0.0f, 0.0f };
    private bool isShownResult = false;
    private bool m_shouldCountDown = true;

    Dictionary<string, float> m_scoreBoard = new Dictionary<string, float>();

    private bool m_isInstantiateAI = false;
    private int m_playerReadyNum = 0;

    private void Start()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();

        int id = GameObject.Find("ParamManager").GetComponent<ParamManage>().GetPlayerID();

        // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(id, 0.0f, 0.0f);
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);

        //�z�X�g�̂ݎ��s���镔��
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (!m_isInstantiateAI)
            {
                for (int i = 0; i < 4 - PhotonNetwork.PlayerList.Length; i++)
                {
                    //�v���C���[�����ɕ��ׂĂ���
                    var AIPos = new Vector3(i + 1, 0.0f, 0.0f);
                    //Prefab����AI�����[���I�u�W�F�N�g�Ƃ��Đ���
                    PhotonNetwork.InstantiateRoomObject("AI", AIPos, Quaternion.identity);
                }
                //AI�𐶐������B
                m_isInstantiateAI = true;
            }
        }

        m_memberListText = GameObject.Find("MemberList");
        m_countDownText = GameObject.Find("CountDown");
        m_resultText = GameObject.Find("Result");

        //�V�[���J�ڂ��z�X�g�ɓ�������
        PhotonNetwork.AutomaticallySyncScene = true;

        //�C���Q�[���ɑJ�ڂ�����������ۂɂ���B
        PhotonNetwork.CurrentRoom.IsOpen = false;

        //�b���̐��������̕ω������邽�߂ɕۑ�����B
        m_prevCountDownNum = (int)m_countDownNum;
    }

    public void AddReadyPlayerNum()
	{
        m_playerReadyNum++;
    }

    public void AddGoaledPlayerNameAndRecordTime(string playerName, float time)
    {
        //this.m_playerGoaledTime[m_goaledPlayerNum] = time;
        //this.m_goaledPlayerNum++;
        //Debug.Log("RECORED");
        //Debug.Log("Result " + m_goaledPlayerNum + " / " + PhotonNetwork.PlayerList.Length);

        m_scoreBoard.Add(playerName, time);
        this.m_goaledPlayerNum++;
    }

    [PunRPC]
    private void SetCountDownTime(int countDownTime)
	{
        m_countDownText.GetComponent<Text>().text = countDownTime.ToString();
    }

    [PunRPC]
    private void SetPlayerMovable()
	{
        GameObject.Find("OwnPlayer").GetComponent<AvatarController>().SetMovable();
        Destroy(m_countDownText.gameObject);
    }

    [PunRPC]
    private void ShowResult(Dictionary<string, float> scoreBoard)
    {
        foreach(var score in scoreBoard)
		{
            m_resultText.GetComponent<Text>().text += "1st : " + score.Key + " : " + score.Value;
        }

        //�������牺�ɂ`�h�̂��Ƃ������Ă���
    }

    void Update()
	{
        m_memberListText.GetComponent<Text>().text = ".+*SpecialRoomMember*+.\n";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            m_memberListText.GetComponent<Text>().text += player.NickName + "\n";
        }

        //�z�X�g�̂ݎ��s���镔��
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            Debug.Log(m_playerReadyNum + "      /      "+ PhotonNetwork.PlayerList.Length);
            if (m_shouldCountDown && m_playerReadyNum == PhotonNetwork.PlayerList.Length)
            {
                //�}�b�`���O�ҋ@���Ԃ��Q�[�����ԂŌ��炵�Ă���
                m_countDownNum -= Time.deltaTime;
                //�҂����Ԃ��Ȃ��Ȃ�����
                if (m_countDownNum < 0.0f)
                {
                    m_shouldCountDown = false;
                    //game�J�n�t���O�𗧂Ă�悤�ɒʐM�𑗂�
                    photonView.RPC(nameof(SetPlayerMovable), RpcTarget.All);

                }
                //�ҋ@���Ԃ̕b�����ς�����炻��𓯊�����
                if (m_prevCountDownNum != (int)m_countDownNum)
                {
                    //�\�����Ԃ��X�V����悤�Ƀ��[���̑S���ɒʒm����i�����Ŏ������c��ҋ@���Ԃ��X�V�j
                    photonView.RPC(nameof(SetCountDownTime), RpcTarget.All, (int)m_countDownNum);
                }

                //���݂̑ҋ@���Ԃ̐���������ۑ����Ă���
                m_prevCountDownNum = (int)m_countDownNum;
            }

            //�S�[�������v���C���[�̐������[�����̃v���C���[�̐��ƈ�v������
            if (m_goaledPlayerNum == PhotonNetwork.PlayerList.Length && !isShownResult)
            {
                //�����^�C����\������悤�ɑS���ɒʒm
                photonView.RPC(nameof(ShowResult), RpcTarget.All, m_scoreBoard);
                //�����ʒm���s����
                isShownResult = true;
            }
        }

        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
