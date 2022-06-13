using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OrangePeel : MonoBehaviourPunCallbacks
{
    //�I�����W�̔�Əd�Ȃ�����
    void OnTriggerEnter(Collider col)
	{
        //�����������̂����������삷��v���C���[�ł�������
        if(col.gameObject.tag == "OwnPlayer")
		{
            //�U�����ꂽ����ɂ���B
            col.gameObject.GetComponent<AvatarController>().SetIsAttacked();
		}
        //�Ԃ������������
        photonView.RPC(nameof(DestroyHittedOrangePeel), RpcTarget.All, this.gameObject.name);
    }

    [PunRPC]
    //�Ԃ�������̖��O�ł��̃C���X�^���X��j������B
    public void DestroyHittedOrangePeel(string hittedOrangePeelName)
    {
        //�����̔�C���X�^���X�̖��O�ŃI�u�W�F�N�g������
        GameObject orange = GameObject.Find(hittedOrangePeelName);
        
        if (orange != null)
        {
            //��̏���
            Destroy(orange.gameObject);
        }
    }
}
