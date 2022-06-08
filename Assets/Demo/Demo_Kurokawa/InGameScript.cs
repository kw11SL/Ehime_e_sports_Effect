using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class InGameScript : MonoBehaviourPunCallbacks
{
    private GameObject m_memberListText = null;
    private GameObject m_countDownText = null;
    private float m_countDownNum = 3.0f;
    private int m_prevCountDownNum = 0;

    private void Start()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();

        int currentPlayerNumber = PhotonNetwork.CountOfPlayersInRooms;

        // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(currentPlayerNumber, 0.0f, 0.0f);
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);

        m_memberListText = GameObject.Find("MemberList");
        m_countDownText = GameObject.Find("CountDown");

        //�V�[���J�ڂ��z�X�g�ɓ�������
        PhotonNetwork.AutomaticallySyncScene = true;

        //�C���Q�[���ɑJ�ڂ�����������ۂɂ���B
        PhotonNetwork.CurrentRoom.IsOpen = false;

        m_prevCountDownNum = (int)m_countDownNum;

    }



    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
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
            //�}�b�`���O�ҋ@���Ԃ��Q�[�����ԂŌ��炵�Ă���
            m_countDownNum -= Time.deltaTime;
            //�҂����Ԃ��Ȃ��Ȃ�����
            if (m_countDownNum < 0.0f)
            {
                //game�J�n�t���O�𗧂Ă�悤�ɒʐM�𑗂�
                //GameObject.Find("OwnPlayer").GetComponent<AvatarController>().SetMovable();
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
