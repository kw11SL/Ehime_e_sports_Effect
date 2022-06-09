using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarController : MonoBehaviourPunCallbacks
{
    Rigidbody m_rb = null;                              //���蓖�Ă�ꂽ���W�b�h�{�f�B
    Vector3 m_moveDir = Vector3.zero;                   //�ړ��������
    Vector3 m_rot = Vector3.zero;                       //�ǂ���ɉ�]���邩�̌���
    private GameObject m_paramManager = null;           //�p�����[�^��ۑ�����C���X�^���X�i�V�[���ׂ��j
    private bool m_canMove = false;                     //�ړ�����������Ă��Ȃ���
    private float m_runningTime = 0.0f;                 //���s����
    private bool m_isGoaled = false;                    //�����̓S�[��������
    private bool m_isToldRecord = false;                //�����̑��j���R�[�h���z�X�g�N���C�A���g�ɑ��������ǂ����̃t���O
    private bool m_isToldReady = false;                 //���[���ɎQ�����ď������ł������Ƃ���x�����ʐM���邽�߂̃t���O

    public float MOVE_POWER = 100.0f;                   //���W�b�h�{�f�B�ɂ�����ړ��̔{��
    public float ROT_POWER = 1.0f;                      //�n���h�����O

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
        //�������������ꂽ�C���X�^���X�ł����
        if(photonView.IsMine)
		{
            //�T���₷�����O��t����B�i�q�G�����L�[�ɂ��K�p�����j
            gameObject.name = "OwnPlayer";
            //�^�O������
            gameObject.tag = "OwnPlayer";
		}

        //1�b�Ԃɉ���ʐM���邩
        PhotonNetwork.SendRate = 3;
        //1�b�Ԃɉ��񓯊����s����
        PhotonNetwork.SerializationRate = 3;
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

    //�Q�[����փN���A�^�C���𑗂�
    [PunRPC]
    void TellRecordTime(float time)
	{
        GameObject.Find("SceneDirector").GetComponent<InGameScript>().AddGoaledPlayerNameAndRecordTime(PhotonNetwork.NickName, time);
    }

    //���g�����[�X�̎Q���̗p�ӂ��ł������z�X�g�ɑ���
    [PunRPC]
    private void TellReadyOK()
	{
        if(!m_isToldReady)
		{
            GameObject.Find("SceneDirector").GetComponent<InGameScript>().AddReadyPlayerNum();
            m_isToldReady = true;
        }
    }

    private void Update()
	{
        //���݂̃V�[�����C���Q�[���ŃJ�E���g�_�E�����I�����ē������ԂȂ��
        if (SceneManager.GetActiveScene().name == "DemoInGame" && m_canMove)
        {
            // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
            if (photonView.IsMine)
            {
                //�O�����Ɉړ�
                m_moveDir = this.transform.forward * (Input.GetAxis("Vertical") * MOVE_POWER);
                //��]
                m_rot = new Vector3(0.0f, Input.GetAxis("Horizontal") * ROT_POWER, 0.0f);

                //�e�X�g�Ń{�^������������o�i�i���o��悤�ɂ���B
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Vector3 orangePeelPos = this.transform.position + (this.transform.forward * -2.0f);
                    photonView.RPC(nameof(InstantiateOrangePeel), RpcTarget.All, orangePeelPos);
                }
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
                photonView.RPC(nameof(TellRecordTime), RpcTarget.MasterClient, m_runningTime);

                m_isToldRecord = true;
            }

            //Y�L��
            if (this.transform.position.y <= -2.0f)
            {
                this.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
            }
        }
    }

    //�I�����W�̔�������̌��ɒu��
    [PunRPC]
    public void InstantiateOrangePeel(Vector3 popPos)
    {
        //�Q�[���S�̂Ő��������I�����W�̔�̐���c���ł���悤�ɁA���[�J���̃C���X�^���X�ł���������B
        m_paramManager.GetComponent<ParamManage>().AddOrangePeelNum();
        
        //���[�J���ŃI�����W�̔���w�肳�ꂽ���W�ɐ���
        var orange = PhotonNetwork.Instantiate("OrangePeel", popPos, Quaternion.identity);
        //�����瑤�ł����O�ɑ�����������t�^����B
        orange.name = "OrangePeel" + m_paramManager.GetComponent<ParamManage>().GetOrangePeelNumOnField();
    }

    //���Ɉˑ�����Ȃ��A�����Ԃ�Update�֐��i�ړ��͂����ɂ������Ɓj
    private void FixedUpdate()
    {
        m_rb.AddForce(m_moveDir);
        transform.Rotate(m_rot);
    }
}