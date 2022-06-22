using UnityEngine;
using Photon.Pun;

//�I�����W�̔�N���X�B�l�b�g���[�N�I�u�W�F�N�g
public class OrangePeel : MonoBehaviourPunCallbacks
{
    //�I�����W�̔�Əd�Ȃ�����
    void OnTriggerEnter(Collider col)
	{
        //�����������̂����������삷��v���C���[�ł�������
        if(col.gameObject.tag == "OwnPlayer")
		{
            //���������v���C���[���U�����ꂽ����ɂ���B
            col.gameObject.GetComponent<AvatarController>().SetIsAttacked();
		}
        //�I�����W������
        Destroy(this.gameObject);
    }
}
