using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class MatchingSceneScript : MonoBehaviourPunCallbacks
{
    private GameObject m_memberListText = null;                     //�����o�[���X�g��\������e�L�X�g�C���X�^���X
    private GameObject m_waitTimeText = null;                       //�c��ҋ@���Ԃ�\������e�L�X�g�C���X�^���X
    private GameObject m_operation = null;                          //����Ǘ��̃C���X�^���X
    private int m_prevMatchingWaitTime = 0;                         //�O�܂ł̎c��ҋ@���Ԃ̐�������
    private float m_matchingWaitTime = 50.0f;                        //�c��ҋ@����
    private bool m_isInstantiateAI = false;                         //AI�C���X�^�X�𐶐�������

    private void Start()
    {
        //������Ď����邽�߁A����C���X�^���X���擾
        m_operation = GameObject.Find("OperationManager");
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        //���O�Ń����o�[��\������C���X�^���X���擾
        m_memberListText = GameObject.Find("MemberList");
        //�}�b�`���O�ҋ@���Ԃ�\������C���X�^���X���擾
        m_waitTimeText = GameObject.Find("WaitTime");
        //�V�[���̑J�ڂ̓z�X�g�N���C�A���g�Ɉˑ�����
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //�쐬���郋�[���̐ݒ�C���X�^���X
    private RoomOptions roomOptions = new RoomOptions()
    {
        //0���Ɛl�������Ȃ�
        MaxPlayers = 4,
        //�����ɎQ���ł��邩
        IsOpen = true,
        //���̕��������r�[�Ƀ��X�g����邩
        IsVisible = true, 
    };

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //�����_���ȃ��[���ɎQ������
        PhotonNetwork.JoinRandomRoom();
    }

    //��Ń����_���ȃ��[���ɎQ���ł��Ȃ�������
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //���������
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        //���ݐڑ����Ă���v���C���[�����擾����B
        int currentPlayerNumber = PhotonNetwork.PlayerList.Length - 1;
        //�v���C���[�����ɕ��ׂĂ���
        var position = new Vector3(currentPlayerNumber, 0.0f, 0.0f);
        //Prefab����v���C���[�����삷�郂�f���𐶐�
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    }

    //�֐��̒ʐM�̍ۂɕK�v�ȕ\�L
    [PunRPC]
    //�c��ҋ@���Ԃ�\������
    void SetWaitTime(int currentTime)
	{
        //�e�L�X�g�̒��g���c��ҋ@���Ԃɏ���������B���l�̓z�X�g�N���C�A���g���Ōv��
        m_waitTimeText.GetComponent<Text>().text = currentTime.ToString();
    }

    //�ҋ@���Ԃ��I���������A��x����AI��s���v���C���[����������B
    private void InstantiateAIOnce()
	{
        //AI�𐶐����Ă��Ȃ����
        if (!m_isInstantiateAI)
        {
            //�ő�v���C���[���܂�AI�𐶐�
            for (int i = 0; i < 4 - PhotonNetwork.PlayerList.Length; i++)
            {
                //�v���C���[�����ɕ��ׂĂ���
                var position = new Vector3(i + 1.5f, 0.0f, 0.0f);
                //Prefab����AI�𐶐�
                PhotonNetwork.Instantiate("AI", position, Quaternion.identity);
            }
            //AI�̐����I��
            m_isInstantiateAI = true;
            //�c��ҋ@���Ԃ̃e�L�X�g��j��
            Destroy(m_waitTimeText.gameObject);
        }
    }

    //�c��ҋ@���Ԃ𑼂̃v���C���[�Ɠ���������
    private void SynchronizeWaitTime()
	{
        //�}�b�`���O�ҋ@���Ԃ��Q�[�����ԂŌ��炵�Ă���
        m_matchingWaitTime -= Time.deltaTime;
        //���݂̑ҋ@���Ԃ̐����������擾
        int currentMatchingWaitTime = (int)m_matchingWaitTime;
        //�҂����Ԃ��Ȃ��Ȃ�����
        if (m_matchingWaitTime < 0.0f)
        {
            //AI�𐶐�
            InstantiateAIOnce();
            //2�b���炢�҂��ăC���Q�[���Ɉڍs
            if (m_matchingWaitTime < -2.0f)
            {
                //�Q�[���J�n
                SceneManager.LoadScene("DemoInGame");
            }
        }
        //�ҋ@���Ԃ̕b�����ς�����炻��𓯊�����
        if (m_prevMatchingWaitTime != currentMatchingWaitTime)
        {
            //�\�����Ԃ��X�V����悤�Ƀ��[���̑S���ɒʒm����i�����Ŏ������c��ҋ@���Ԃ��X�V�j
            photonView.RPC(nameof(SetWaitTime), RpcTarget.All, currentMatchingWaitTime);
        }

        //���݂̑ҋ@���Ԃ̐���������ۑ����Ă���
        m_prevMatchingWaitTime = currentMatchingWaitTime;
    }

    void Update()
    {
        //���[���̃����o�[���X�g���X�V����B
        m_memberListText.GetComponent<Text>().text = ".+*SpecialRoomMember*+.\n";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            m_memberListText.GetComponent<Text>().text += player.NickName + "\n";
        }

        //�z�X�g�̂ݎ��s���镔��
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            //�z�X�g�N���C�A���g���{�^���𒷉��������
            if(m_operation.GetComponent<Operation>().GetIsLongTouch())
			{
                //�����I�ɃC���Q�[���ɑJ�ڂ���
                SceneManager.LoadScene("DemoInGame");
            }

            //�c�莞�Ԃ𑼃v���C���[�Ɠ�������
            SynchronizeWaitTime();
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
