using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class MatchingSceneScript : MonoBehaviourPunCallbacks
{
    private GameObject text = null;
    private GameObject playerObject = null;
    private float matchingWaitTime = 30.0f;
    private GameObject operation = null;
    private GameObject timeText = null;
    private int prevMatchingWaitTime = 0;

    private void Start()
    {
        //������Ď����邽�߁A����C���X�^���X���擾
        operation = GameObject.Find("OperationManager");
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        //���O�Ń����o�[��\������C���X�^���X���擾
        text = GameObject.Find("MemberList");
        //�}�b�`���O�ҋ@���Ԃ�\������C���X�^���X���擾
        timeText = GameObject.Find("WaitTime");
        //�V�[���̑J�ڂ̓z�X�g�N���C�A���g�Ɉˑ�����
        PhotonNetwork.AutomaticallySyncScene = true;
    }

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
        playerObject = PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    }

    //�֐��̒ʐM�̍ۂɕK�v�ȕ\�L
    [PunRPC]
    //�c��ҋ@���Ԃ�\������
    void SetWaitTime(int currentTime)
	{
        //�e�L�X�g�̒��g���c��ҋ@���Ԃɏ���������B���l�̓z�X�g�N���C�A���g���Ōv��
        timeText.GetComponent<Text>().text = currentTime.ToString();
    }

    void Update()
    {
        //���[���̃����o�[���X�g���X�V����B
        text.GetComponent<Text>().text = ".+*SpecialRoomMember*+.\n";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            text.GetComponent<Text>().text += player.NickName + "\n";
        }

        //�z�X�g�̂ݎ��s���镔��
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            //�z�X�g�N���C�A���g�Ń{�^���𒷉��������
            if(operation.GetComponent<Operation>().GetIsLongTouch())
			{
                //�C���Q�[���ɑJ�ڂ���
                SceneManager.LoadScene("DemoInGame");
            }

            //�}�b�`���O�ҋ@���Ԃ��Q�[�����ԂŌ��炵�Ă���
            matchingWaitTime -= Time.deltaTime;
            //���݂̑ҋ@���Ԃ̐����������擾
            int currentMatchingWaitTime = (int)matchingWaitTime;
            //�҂����Ԃ��Ȃ��Ȃ�����
            if (matchingWaitTime < 0.0f)
            {
                //�Q�[���J�n
                SceneManager.LoadScene("DemoInGame");
            }
            //�ҋ@���Ԃ̕b�����ς�����炻��𓯊�����
            if (prevMatchingWaitTime != currentMatchingWaitTime)
			{
                //�\�����Ԃ��X�V����悤�Ƀ��[���̑S���ɒʒm����i�����Ŏ������c��ҋ@���Ԃ��X�V�j
                photonView.RPC(nameof(SetWaitTime), RpcTarget.All, currentMatchingWaitTime);
            }

            //���݂̑ҋ@���Ԃ̐���������ۑ����Ă���
            prevMatchingWaitTime = currentMatchingWaitTime;
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
