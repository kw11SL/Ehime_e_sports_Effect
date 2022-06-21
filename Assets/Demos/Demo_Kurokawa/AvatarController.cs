using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarController : MonoBehaviourPunCallbacks
{
    Rigidbody m_rb = null;                              //���蓖�Ă�ꂽ���W�b�h�{�f�B
    Vector3 m_moveDir = Vector3.zero;                   //�ړ��������
    Vector3 m_moveSpeed = Vector3.zero;                 //�ړ��X�s�[�h
    Vector3 m_rot = Vector3.zero;                       //�ǂ���ɉ�]���邩�̌���
    Vector3 m_corseDir = Vector3.zero;                  //���ݑ����Ă���R�[�X�̑�܂��ȕ���
    Vector3 m_alongWallDir = Vector3.zero;
    private GameObject m_paramManager = null;           //�p�����[�^��ۑ�����C���X�^���X�i�V�[���ׂ��j
    private bool m_canMove = false;                     //�ړ�����������Ă��Ȃ���
    private float m_runningTime = 0.0f;                 //���s����
    private float m_stiffenTime = 0.0f;                 //�U�����ꂽ���̍d�����Ă��鎞��
    private float m_starTime = 0.0f;                    //�X�^�[���Ԃ̎g�p���Ă��鎞��
    private float m_dashTime = 0.0f;                    //�L�m�R���g���ă_�b�V�����Ă��鎞��
    private float m_killerTime = 0.0f;                  //�L���[���g�p���Ă��鎞��
    private float m_spinedAngle = 0.0f;                 //��e���ĉ�]��������
    private bool m_isGoaled = false;                    //�����̓S�[��������
    private bool m_isToldRecord = false;                //�����̑��j���R�[�h���z�X�g�N���C�A���g�ɑ��������ǂ����̃t���O
    private bool m_isToldReady = false;                 //���[���ɎQ�����ď������ł������Ƃ���x�����ʐM���邽�߂̃t���O
    private bool m_isUsingStar = false;                 //���݁A�X�^�[���g�p���Ă��邩
    private bool m_isUsingKiller = false;               //���݁A�L���[���g�p���Ă��邩
    private bool m_isUsingJet = false;                  //���݁A�W�F�b�g���g�p���Ă��邩
    private bool m_isAttacked = false;                  //�U�����ꂽ��
    private bool m_hittedWall = false;                  //�ǂɓ������Ă��邩
    private Quaternion m_prevTrasnform;                 //�O��̉�]�̓x����
    

    public float MOVE_POWER = 25.0f;                  �@//���W�b�h�{�f�B�ɂ�����ړ��̔{��
    public float MOVE_POWER_USING_STAR = 35.0f;         //�X�^�[�g�p���̃��W�b�h�{�f�B�ɂ�����ړ��̔{��
    public float MOVE_POWER_USING_JET = 50.0f;          //�W�F�b�g�g�p���̃��W�b�h�{�f�B�ɂ�����ړ��̔{��
    public float MOVE_POWER_USING_KILLER = 60.0f;       //�L���[�g�p���̃��W�b�h�{�f�B�ɂ�����ړ��̔{��
    public float ROT_POWER = 0.5f;                      //�n���h�����O
    public float MAX_STAR_REMAIN_TIME = 10.5f;          //�X�^�[�̍ő�p������
    public float MAX_KILLER_REMAIN_TIME = 3.0f;         //�L���[�̍ő�p������
    public float MAX_DASH_TIME = 1.0f;                  //�_�b�V���̍ő�p������
    public float MAX_STIFFIN_TIME = 1.5f;               //�U���������������̍ő�d������
    public float KILLER_HANDLING_RATE = 5.0f;           //�L���[���g�p�����ۂ̃J�����̒Ǐ]���x
    public float SPIN_AMOUNT = 0.5f;                    //��e���̉�]��

    private AlongWall m_alongWall = null;

    void Start()
    {
        //���W�b�h�{�f�B���擾
        m_rb = GetComponent<Rigidbody>();
        //�C���Q�[�����ł����
        if (SceneManager.GetActiveScene().name == "DemoInGame")
        {
            //�d�͂��I���ɂ���
            m_rb.useGravity = true;
            //�C���Q�[���Ɉڍs�ł������Ƃ�ʐM
            photonView.RPC(nameof(TellReadyOK), RpcTarget.MasterClient);
        }
        //�Q�[�����̃p�����[�^�ۑ��C���X�^���X���擾
        m_paramManager = GameObject.Find("ParamManager");
        //�l�b�g���[�N�œ�������閼�O��ݒ�
        PhotonNetwork.NickName = "Player" + m_paramManager.GetComponent<ParamManage>().GetPlayerID();
        gameObject.tag = "Player";
        //�������������ꂽ�C���X�^���X�ł����
        if (photonView.IsMine)
        {
            //�T���₷�����O��t����B�i�q�G�����L�[�ɂ��K�p�����j
            gameObject.name = "OwnPlayer";
            //�^�O������
            gameObject.tag = "OwnPlayer";
        }

        //�O��̉�]��������
        m_prevTrasnform = this.transform.rotation;

        m_alongWall = new AlongWall();


        //1�b�Ԃɉ���ʐM���邩
        PhotonNetwork.SendRate = 3;
        //1�b�Ԃɉ��񓯊����s����
        PhotonNetwork.SerializationRate = 3;
    }

    //���[���v���p�e�B�̉������X�V���ꂽ���̊֐�
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //�X�V���ꂽ���[���̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
        foreach (var prop in propertiesThatChanged)
        {
            //�e�v���C���[�̖��G��Ԃ������L�[�������쐬
            string name = PhotonNetwork.NickName + "Invincible";
            //�X�V���ꂽ�����̃L�[������String�Ŏ擾
            string key = prop.Key.ToString();
            if (name == key)
			{
                bool isUsingStar = false;
                int isUsing = (PhotonNetwork.CurrentRoom.CustomProperties[prop.Key] is int value) ? value : 0;
                if (isUsing == 1)
				{
                    isUsingStar = true;
				}
                m_isUsingStar = isUsingStar;
            }
            Debug.Log($"{prop.Key}: {prop.Value}");
        }
    }

    //�v���C���[�̃C���v�b�g���󂯂���Ĉړ��\�ɂ���
    public void SetMovable()
    {
        m_canMove = true;
    }

    //�v���C���[���S�[����������ݒ肷��
    public void SetGoaled()
    {
        m_isGoaled = true;
    }

    //�������U�����ꂽ���Ƃ�ݒ肷��
    public void SetIsAttacked()
    {
        //�X�^�[���L���[���g�p���Ă��Ȃ���ԂȂ��
        if(!m_isUsingKiller && !m_isUsingStar)
		{
            //�U�����ꂽ
            m_isAttacked = true;
        }
    }

    //�X�^�[���g�p���Ă����Ԃɂ���
    public void SetIsUsingStar()
	{
        //���[���v���p�e�B�̎����̖��G��Ԃ𖼑O���g���Č����A�ύX���s��
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        string name = PhotonNetwork.NickName + "Invincible";
        hashtable[name] = 1;
        //���[���v���p�e�B���X�V
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    //�W�F�b�g���g�p���Ă����Ԃɂ���
    public void SetIsUsingJet()
	{
        m_isUsingJet = true;
	}

    //�L���[���g�p���Ă����Ԃɂ���
    public void SetIsUsingKiller()
	{
        m_isUsingKiller = true;
	}

    //�X�^�[���g�p���Ă��邩���擾����
    public bool GetIsUsingStar()
	{
        return m_isUsingStar;
	}

    public bool GetIsAttacked()
	{
        return m_isAttacked;
	}

    //�z�X�g�փN���A�^�C���𑗂�
    [PunRPC]
    void TellRecordTime(string name, float time)
    {
        GameObject.Find("SceneDirector").GetComponent<InGameScript>().AddGoaledPlayerNameAndRecordTime(name, time);
    }

    //���g�����[�X�̎Q���̗p�ӂ��ł������z�X�g�ɑ���
    [PunRPC]
    private void TellReadyOK()
    {
        //���������Ȃ��悤�ɂ���i�ʐM�̊֌W��t���O�Ō��ɂ߂�j
        if (!m_isToldReady)
        {
            GameObject.Find("SceneDirector").GetComponent<InGameScript>().AddReadyPlayerNum();
            m_isToldReady = true;
        }
    }

    //�������Փ˂�����
    private void OnCollisionEnter(Collision col)
	{
        //�ՓˑΏۂ����̃v���C���[�Ȃ��
        if(col.gameObject.tag == "Player")
        {
            //�R���W�����̎������PhotonNetwork�Ɋւ���ϐ����擾
            Player pl = col.gameObject.GetComponent<PhotonView>().Owner;
            //���̃v���C���[�̖��G��Ԃ����[���v���p�e�B���玝���Ă���
            string plName = pl.NickName + "Invincible";
            int stat = (PhotonNetwork.CurrentRoom.CustomProperties[plName] is int value) ? value : 0;

            //�U�����󂯂���
            bool isCrash = false;
            //���̃v���C���[�����G�ŁA�������X�^�[���L���[���g���Ă��Ȃ���
            if (stat == 1 && !m_isUsingStar && !m_isUsingKiller)
			{
                //�U�����󂯂�
                isCrash = true;
			}

            if (isCrash)
			{
                //�����̃v���C���[�͍U�����󂯂�
                m_isAttacked = true;
			}
		}
        //�v���C���[�łȂ��A�^�C�Ȃ��
		else if(col.gameObject.name == "Snapper")
		{
            //�L���[���X�^�[���g���Ă��Ȃ����
            if(!m_isUsingKiller && !m_isUsingStar)
			{
                //�U�����ꂽ
                m_isAttacked = true;
			}
		}

        if(col.gameObject.tag == "Wall")
		{
            m_hittedWall = true;
            Debug.Log("WALL");
            m_alongWall.CollisionEnter(col, m_rb, ref m_moveDir);
            m_alongWallDir = m_moveDir;
            Debug.Log("AvatarController : m_moveDir = " + m_alongWallDir);
        }
    }

	private void OnCollisionStay(Collision col)
	{
        if (col.gameObject.tag == "Wall")
        {
            m_hittedWall = true;
            Debug.Log("WALL");
            m_alongWall.CollisionEnter(col, m_rb, ref m_moveDir);
            m_alongWallDir = m_moveDir;
            Debug.Log("AvatarController : m_moveDir = " + m_alongWallDir);
        }
    }

	private void Update()
	{
        //�R�[�X�̌��������݂̃E�F�C�|�C���g�ʉߏ󋵂��璲�ׂ�
        m_corseDir = this.GetComponent<WayPointChecker>().GetNextWayPoint() - this.GetComponent<WayPointChecker>().GetCurrentWayPoint();
        //���K��
        m_corseDir.Normalize();
        //�����̕����͂���Ȃ�
        m_corseDir.y = 0.0f;

        //���݂̃V�[�����C���Q�[���ŃJ�E���g�_�E�����I�����ē������ԂȂ��
        if (SceneManager.GetActiveScene().name == "DemoInGame" && m_canMove)
        {
            // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
            if (photonView.IsMine)
            {
                //�O�����Ɉړ�
                m_moveDir = this.transform.forward * (Input.GetAxis("Vertical"));
                //�L���[���g���Ă��鎞�̈ړ��X�s�[�h���v�Z����B
                if (m_isUsingKiller)
                {
                    m_moveSpeed = m_moveDir * MOVE_POWER_USING_KILLER;
                }
                //�X�^�[���g���Ă��鎞�̈ړ��X�s�[�h���v�Z����B
                else if (m_isUsingStar)
				{
                    m_moveSpeed = m_moveDir * MOVE_POWER_USING_STAR;
                }
                //�L�m�R���g���Ă��鎞�̈ړ��X�s�[�h���v�Z����B
                else if (m_isUsingJet)
				{
                    m_moveSpeed = m_moveDir * MOVE_POWER_USING_JET;
                }
                //�ʏ펞�̈ړ��X�s�[�h���v�Z����B
                else
                {
                    m_moveSpeed = m_moveDir * MOVE_POWER;
                }

                //���͂ɂ���]��
                m_rot = new Vector3(0.0f, Input.GetAxis("Horizontal") * ROT_POWER, 0.0f);
            }

            //�S�[�����Ă��Ȃ�����
            if(!m_isGoaled)
			{
                //���s���Ԃ��Q�[���^�C���Ōv����������B
                m_runningTime += Time.deltaTime;
            }
			else if(!m_isToldRecord)
			{
                //�N���A�^�C�����z�X�g�����ɑ���
                photonView.RPC(nameof(TellRecordTime), RpcTarget.MasterClient, PhotonNetwork.NickName, m_runningTime);
                m_isToldRecord = true;
            }

			//Y�L��
			if (this.transform.position.y <= -2.0f)
			{
				this.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
			}
		}
    }

	private void LateUpdate()
	{
        if (m_isUsingKiller)
        {
            //��]�ɂ��āAFixedUpdate�ł��ƌĂяo���񐔂����Ȃ����ăK�N�����߂����ōX�V
            Quaternion rot;
            //�����̑O��������R�[�X�̌����ւ̉�]���v�Z
            Vector3 newForward = (this.GetComponent<WayPointChecker>().GetNextWayPoint() - this.transform.position) - this.transform.forward;
            newForward.y = 0.0f;
            rot = Quaternion.LookRotation(newForward);
            //�ɂ₩�ɂ��ēK�p
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * KILLER_HANDLING_RATE);
            //���݂̉�]��ۑ�
            m_prevTrasnform = this.transform.rotation;
            return;
        }

        //���͂ɂ���]�����������Ȃ�
        if (!m_isAttacked)
        { 
            //���ݓ��͂��Ă����]��K�p����Transform��K�X
            Transform appliedTrasnform = this.transform;
            appliedTrasnform.Rotate(m_rot);

            //�R�[�X�̌����ƃv���C���[�̑O������45�x�ȓ��ł����
            if (Vector3.Dot(m_corseDir, appliedTrasnform.forward) >= 0.7f)
            {
                //��]�����ۂɓK�p����
                transform.Rotate(m_rot);
                //�K�؂ȉ�]��ۑ�
                m_prevTrasnform = this.transform.rotation;
            }
            //���Ɍ��������Ă���Ȃ��
            else
            {

                //�O��K�p�����A�K�؂ȉ�]�ŕ␳
                this.transform.rotation = m_prevTrasnform;

                //�悱�Ɍ��������Ă���
                if (Vector3.Dot(m_corseDir, this.transform.forward) < 0.7f)
                {
                    Quaternion rot;
                    //�R�[�X�̌����ɖ߂��悤�ȉ�]���v�Z���ēK�p����
                    rot = Quaternion.LookRotation(m_corseDir - this.transform.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);

                    m_prevTrasnform = this.transform.rotation;
                }
            }
        }

        if(m_isAttacked)
		{
            m_spinedAngle += SPIN_AMOUNT;

            if(m_spinedAngle < 360.0f)
			{
                this.transform.Rotate(0.0f, SPIN_AMOUNT, 0.0f, Space.World); // ��]�p�x��ݒ�            
            }
        }
    }

	//���Ɉˑ�����Ȃ��A�����Ԃ�Update�֐��i�ړ��͂����ɂ������Ɓj
	private void FixedUpdate()
    {
        //�L���[���g���Ă�����
        if (m_isUsingKiller)
        {
            //���̃E�F�C�|�C���g�ւ̌������v�Z
			Vector3 direction = this.GetComponent<WayPointChecker>().GetNextWayPoint() - this.transform.position;
            //���K��
            direction.Normalize();
            //�����͂���Ȃ�
            direction.y = 0.0f;
            //TRASNFORM�ňʒu���X�V�iRigidbody���g���Ƒ������o�Ȃ����A����������R�[�X�A�E�g����
            this.transform.position += direction * 1.5f;

            //�L���[���g�p���Ă��鎞�Ԃ��Q�[���^�C���ŃC���N�������g
            m_killerTime += Time.deltaTime;
            //�ő�p�����Ԃ𒴂�����
            if(m_killerTime >= MAX_KILLER_REMAIN_TIME)
			{
                //���Ԃ����Z�b�g
                m_killerTime = 0.0f;
                //�L���[���g���Ă��Ȃ���Ԃɂ���
                m_isUsingKiller = false;
            }

            //�A�C�e���̏d�ˊ|���������Ȃ��悤�ɁA�g�p���������A�C�e���̌p�����Ԃ����炵�Ă����B
            if (m_isUsingJet)
            {
                m_dashTime += Time.deltaTime;
                if (m_dashTime >= MAX_DASH_TIME)
                {
                    m_isUsingJet = false;
                    m_dashTime = 0.0f;
                }
            }
            if (m_isUsingStar)
            {
                m_starTime += Time.deltaTime;
                if (m_starTime >= MAX_STAR_REMAIN_TIME)
                {
                    m_starTime = 0.0f;
                    m_isUsingStar = false;

                    var hashtable = new ExitGames.Client.Photon.Hashtable();
                    string name = PhotonNetwork.NickName + "Invincible";
                    hashtable[name] = 0;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
                }
            }

            //�L���[�g�p���͈ȍ~�̏��������s���Ȃ�
            return;
        }

        if(!m_isAttacked)
		{
            if(m_hittedWall)
			{
                m_moveSpeed = m_alongWallDir * MOVE_POWER;
                Debug.Log("moveSpeed : " + m_moveSpeed + " m_alongWallDir : " + m_alongWallDir);
                //�O���։���
                
                m_hittedWall = false;
            }
            m_rb.AddForce(m_moveSpeed - m_rb.velocity);
        }
		//�U������Ă�����
		else
		{
            //�d�����Ԃ��Q�[�����Ԃő��₷
            m_stiffenTime += Time.deltaTime;
            //�ݒ肵���ő�d�����Ԃ𒴂�����

            if (m_stiffenTime >= MAX_STIFFIN_TIME)
			{
                //�v�������d�����Ԃ����Z�b�g
                m_stiffenTime = 0.0f;
                //��e���̉�]���A�N�V�����̑���]�ʂ����Z�b�g
                m_spinedAngle = 0.0f;
                //�U���t���O�𒼂�
                m_isAttacked = false;
            }
		}

        //�W�F�b�g���g�p���Ă���Ȃ��
        if(m_isUsingJet)
		{
            //�g�p���Ԃ𑝂₵��
            m_dashTime += Time.deltaTime;
            //�ő�p�����Ԃ𒴂�����
            if(m_dashTime >= MAX_DASH_TIME)
			{
                //�g���Ă��Ȃ����Ƃɂ���
                m_isUsingJet = false;
                //�^�C�������Z�b�g
                m_dashTime = 0.0f;
			}
		}
		if (m_isUsingStar)
		{
			m_starTime += Time.deltaTime;
			if (m_starTime >= MAX_STAR_REMAIN_TIME)
			{
				m_starTime = 0.0f;
				m_isUsingStar = false;

                //���[���v���p�e�B�̖��G��Ԃ��߂��Ă���
				var hashtable = new ExitGames.Client.Photon.Hashtable();
				string name = PhotonNetwork.NickName + "Invincible";
				hashtable[name] = 0;
				PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
            }
		}
	}
}