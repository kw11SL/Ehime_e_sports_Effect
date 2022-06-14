using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObtainItemController : MonoBehaviourPunCallbacks
{
    private enum EnItemType
	{
        enOrangePeel,                                   //�I�����W�̔�
        enOrangeJet,                                    //�I�����W�W���[�X�W�F�b�g
        enTrain,                                        //�V������ԃL���[
        enStar,                                         //�X�^�[
        enSnapperCannon,                                //�^�C�C
        enItemTypeNum                                   //�A�C�e���̎�ނ̐�
	}

    private GameObject m_paramManager = null;           //�p�����[�^��ۑ�����C���X�^���X�i�V�[���ׂ��j

    private void Start()
	{
        //�Q�[�����̃p�����[�^��ۑ�����C���X�^���X���擾
        m_paramManager = GameObject.Find("ParamManager");
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

    //�^�C�������̑O�ɒu��
    [PunRPC]
    public void InstantiateSnapper(Vector3 popPos)
    {
        //���[�J���ŃI�����W�̔���w�肳�ꂽ���W�ɐ���
        var snapper = PhotonNetwork.Instantiate("Snapper", popPos, Quaternion.identity);
        //�����瑤�ł����O�ɑ�����������t�^����B
        snapper.name = "Snapper";
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
            //�e�X�g�Ń{�^������������X�^�[�g�p��Ԃɂ���B
            if (Input.GetKeyDown(KeyCode.L))
            {
                this.GetComponent<AvatarController>().SetIsUsingJet();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Vector3 snapperPos = this.gameObject.transform.position + (this.gameObject.transform.forward * -2.0f);
                photonView.RPC(nameof(InstantiateSnapper), RpcTarget.All, snapperPos);
            }
        }
    }
}
