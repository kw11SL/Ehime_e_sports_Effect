using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//�v���C���[���擾���Ă���A�C�e���̊Ǘ��Ǝg�p����N���X�B
public class ObtainItemController : MonoBehaviourPunCallbacks
{
    private GameObject m_paramManager = null;           //�p�����[�^��ۑ�����C���X�^���X�i�V�[���ׂ��j
    private EnItemType m_obtainItemType = EnItemType.enNothing;

    //�A�C�e���̎��
    private enum EnItemType
	{
        enNothing = -1,
        enOrangePeel,                                   //�I�����W�̔�
        enOrangeJet,                                    //�I�����W�W���[�X�W�F�b�g
        enTrain,                                        //�V������ԃL���[
        enStar,                                         //�X�^�[
        enSnapperCannon,                                //�^�C�C
        enItemTypeNum                                   //�A�C�e���̎�ނ̐�
	}

    private void Start()
	{
        //�Q�[�����̃p�����[�^��ۑ�����C���X�^���X���擾
        m_paramManager = GameObject.Find("ParamManager");
	}

    //�����_���ȃA�C�e���𒊑I����
    public void GetRandomItem()
	{
        //���������Ă��Ȃ����
        if(m_obtainItemType == EnItemType.enNothing)
		{
            //�A�C�e���̃i���o�[�������_���Ɏ擾
            int type = (int)Random.Range((float)EnItemType.enOrangePeel, (float)EnItemType.enItemTypeNum);
            m_obtainItemType = (EnItemType)type;
        }

        Debug.Log("�擾�����A�C�e���ԍ��@���@" + m_obtainItemType);
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

    //�v���C���[���Ăяo���B�^�C�������̑O�ɒu��
    [PunRPC]
    public void InstantiateSnapper(Vector3 popPos)
    {
        //���[�J���ŃI�����W�̔���w�肳�ꂽ���W�ɐ���
        var snapper = PhotonNetwork.Instantiate("Snapper", popPos, Quaternion.identity);
        //���O�B
        snapper.name = "Snapper";
        //�v���C���[�����߂Œʉ߂����E�F�C�|�C���g�̔ԍ��A���W��^����
        snapper.GetComponent<WayPointChecker>().SetCurrentWayPointDirectly(popPos, this.gameObject.GetComponent<WayPointChecker>().GetCurrentWayPointNumber());
    }

    void Update()
    {
        //���������������C���X�^���X�Ȃ��
        if (photonView.IsMine)
        {
			//�e�X�g�Ń{�^������������o�i�i���o��悤�ɂ���B
			if (Input.GetKeyDown(KeyCode.K))
			{
				Vector3 orangePeelPos = this.gameObject.transform.position + (this.gameObject.transform.forward * -2.0f);
				photonView.RPC(nameof(InstantiateOrangePeel), RpcTarget.All, orangePeelPos);
			}
			//�e�X�g�Ń{�^������������X�^�[�g�p��Ԃɂ���B
			if (Input.GetKeyDown(KeyCode.J))
			{
				this.GetComponent<AvatarController>().SetIsUsingStar();
			}
			//�e�X�g�Ń{�^������������L�m�R�g�p��Ԃɂ���B
			if (Input.GetKeyDown(KeyCode.L))
			{
				this.GetComponent<AvatarController>().SetIsUsingJet();
			}
			//����o��
			if (Input.GetKeyDown(KeyCode.I))
			{
				Vector3 snapperPos = this.gameObject.transform.position + (this.gameObject.transform.forward * 2.0f);
				photonView.RPC(nameof(InstantiateSnapper), RpcTarget.All, snapperPos);
			}
			//�e�X�g�Ń{�^������������L���[�g�p��Ԃɂ���B
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.GetComponent<AvatarController>().SetIsUsingKiller();
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
                switch(m_obtainItemType)
				{
                    case EnItemType.enOrangePeel :
                        Vector3 orangePeelPos = this.gameObject.transform.position + (this.gameObject.transform.forward * -2.0f);
                        photonView.RPC(nameof(InstantiateOrangePeel), RpcTarget.All, orangePeelPos);
                        m_obtainItemType = EnItemType.enNothing;
                        break;
                    case EnItemType.enOrangeJet:
                        this.GetComponent<AvatarController>().SetIsUsingJet();
                        m_obtainItemType = EnItemType.enNothing;
                        break;
                    case EnItemType.enTrain:
                        this.GetComponent<AvatarController>().SetIsUsingKiller();
                        m_obtainItemType = EnItemType.enNothing;
                        break;
                    case EnItemType.enStar:
                        this.GetComponent<AvatarController>().SetIsUsingStar();
                        m_obtainItemType = EnItemType.enNothing;
                        break;
                    case EnItemType.enSnapperCannon:
                        Vector3 snapperPos = this.gameObject.transform.position + (this.gameObject.transform.forward * 2.0f);
                        photonView.RPC(nameof(InstantiateSnapper), RpcTarget.All, snapperPos);
                        m_obtainItemType = EnItemType.enNothing;
                        break;
                    default:
                        return;
                }
			}
        }
    }
}
