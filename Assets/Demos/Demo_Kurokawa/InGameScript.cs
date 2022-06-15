using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
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
    private GameObject m_paramManager = null;
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
        m_paramManager = GameObject.Find("ParamManager");

        if(m_paramManager.GetComponent<ParamManage>().GetIsOfflineMode())
		{
            PhotonNetwork.OfflineMode = true;
            m_paramManager.GetComponent<ParamManage>().SetPlayerID(1);
        }
		else
		{
            // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
            PhotonNetwork.ConnectUsingSettings();
            //�V�[���J�ڂ��z�X�g�ɓ�������
            PhotonNetwork.AutomaticallySyncScene = true;
            //�C���Q�[���ɑJ�ڂ�����������ۂɂ���B
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        int id = m_paramManager.GetComponent<ParamManage>().GetPlayerID();
        //Debug.Log("OfflineMode = " + PhotonNetwork.OfflineMode + "PlayerID = " + id);

        // ���g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        string spawnPointName = "PlayerSpawnPoint" + (id - 1);

        GameObject spawnPoint = GameObject.Find(spawnPointName);
        //Debug.Log(spawnPoint.name);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        var position = spawnPoint.transform.position;
        GameObject ownPlayer = PhotonNetwork.Instantiate("Player", position, Quaternion.identity);

        Debug.Log(ownPlayer.name + " " + ownPlayer.tag);

        spawnPoint.GetComponent<PlayerSpawnPoint>().SetPlayerSpawned();

        //�z�X�g�̂ݎ��s���镔��
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            Debug.Log("I am Master Client");
            //�����|�b�v�����Ă��Ȃ��X�|�[���|�C���g��T���AAI�𐶐�����
            FindEmptySpawnPointAndPopAI();

            var hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("Player1Invincible", 0); 
            hashtable.Add("Player2Invincible", 0); 
            hashtable.Add("Player3Invincible", 0); 
            hashtable.Add("Player4Invincible", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        }

        m_memberListText = GameObject.Find("MemberList");
        m_countDownText = GameObject.Find("CountDown");
        m_resultText = GameObject.Find("Result");

        //�b���̐��������̕ω������邽�߂ɕۑ�����B
        m_prevCountDownNum = (int)m_countDownNum;
    }

    //�I�t���C�����[�h�̎��Ɏg�p����
    public override void OnConnectedToMaster()
    {
        if(m_paramManager.GetComponent<ParamManage>().GetIsOfflineMode())
		{
            //�쐬���郋�[���̐ݒ�C���X�^���X
            RoomOptions roomOptions = new RoomOptions()
            {
                //0���Ɛl�������Ȃ�
                MaxPlayers = 1,
                //�����ɎQ���ł��邩
                IsOpen = false,
                //���̕��������r�[�Ƀ��X�g����邩
                IsVisible = false,
                //���[�U�[ID�̔��z���s���B
                PublishUserId = true,
            };
            //�I�t���C���̕��������
            PhotonNetwork.CreateRoom(null, roomOptions);
        }
    }

    public override void OnJoinedRoom()
    {
        string spawnPointName;
        for(int i = 0; i < 3; i++)
		{
            spawnPointName = "PlayerSpawnPoint" + (i + 1);
            Vector3 popPos = GameObject.Find(spawnPointName).transform.position;
            GameObject ai = PhotonNetwork.InstantiateRoomObject("AI", popPos, Quaternion.identity);
            ai.gameObject.tag = "Player";
        }
    }

    private void FindEmptySpawnPointAndPopAI()
	{
        if (!m_isInstantiateAI)
        {
            //���[���ɂ��鑼�̃v���C���[���擾
            Player[] allPlayers = PhotonNetwork.PlayerList;
            //���̃v���C���[�Ɋ��蓖�Ă��Ă���A�g���Ȃ����O��ID��ۑ����Ă����z����`
            var cantUsePosition = new List<string>();
            foreach (var pl in allPlayers)
            {
                //���Ɏg���Ă���ID��ۑ����Ă���
                cantUsePosition.Add(pl.NickName);
            }

			//�������Ȃ��Ă͂Ȃ�Ȃ�AI�̐�����
			for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.PlayerList.Length; i++)
			{
				GameObject AISpawnPoint;
				//Player1�Ƃ������O�̃��[�U�[�����Ȃ���΁AID1���g�p����B
				if (!cantUsePosition.Contains("Player1"))
				{
					AISpawnPoint = GameObject.Find("PlayerSpawnPoint0");
					//Prefab����AI�����[���I�u�W�F�N�g�Ƃ��Đ���
					GameObject ai = PhotonNetwork.InstantiateRoomObject("AI", AISpawnPoint.transform.position, Quaternion.identity);
					ai.gameObject.tag = "Player";
					cantUsePosition.Add("Player1");
				}
				else if (!cantUsePosition.Contains("Player2"))
				{
					AISpawnPoint = GameObject.Find("PlayerSpawnPoint1");
					//Prefab����AI�����[���I�u�W�F�N�g�Ƃ��Đ���
					GameObject ai = PhotonNetwork.InstantiateRoomObject("AI", AISpawnPoint.transform.position, Quaternion.identity);
					ai.gameObject.tag = "Player";
					cantUsePosition.Add("Player2");
				}
				else if (!cantUsePosition.Contains("Player3"))
				{
					AISpawnPoint = GameObject.Find("PlayerSpawnPoint2");
					//Prefab����AI�����[���I�u�W�F�N�g�Ƃ��Đ���
					GameObject ai = PhotonNetwork.InstantiateRoomObject("AI", AISpawnPoint.transform.position, Quaternion.identity);
					ai.gameObject.tag = "Player";
					cantUsePosition.Add("Player3");
				}
				else if (!cantUsePosition.Contains("Player4"))
				{
					AISpawnPoint = GameObject.Find("PlayerSpawnPoint3");
					//Prefab����AI�����[���I�u�W�F�N�g�Ƃ��Đ���
					GameObject ai = PhotonNetwork.InstantiateRoomObject("AI", AISpawnPoint.transform.position, Quaternion.identity);
					ai.gameObject.tag = "Player";
					cantUsePosition.Add("Player4");
				}
			}

			//AI�𐶐������B
			m_isInstantiateAI = true;
        }
    }

    public void AddReadyPlayerNum()
	{
        m_playerReadyNum++;
    }

    //�S�[�������v���C���[���ƃ^�C�����z�X�g�ɋL�^
    public void AddGoaledPlayerNameAndRecordTime(string playerName, float time)
    {
        //�v���C���[�����L�[�ɁA�N���A�^�C�����o�����[��
        m_scoreBoard.Add(playerName, time);
        //�S�[�������v���C���[�̑������C���N�������g
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
            //Debug.Log(m_playerReadyNum + "      /      "+ PhotonNetwork.PlayerList.Length);
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
