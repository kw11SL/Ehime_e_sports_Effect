using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�o�b�O�p�@�@����`�悷��I�u�W�F�N�g�̃}�l�[�W���[�I�u�W�F�N�g
/// </summary>
public class NormalDebugManager : MonoBehaviour
{
    //�@���`��I�u�W�F�N�g�̃C���X�^���X
    public GameObject m_nornalDebugInstance;

    /// <summary>
    /// �@���`��I�u�W�F�N�g�̐���
    /// </summary>
    /// <param name="position">�����ʒu</param>
    /// <param name="normal">�@���̌���</param>
    public void Spawn(Vector3 position,Vector3 normal)
    {
        //�@���`�悷��I�u�W�F�N�g�𐶐�
        GameObject normalDebugObject = GameObject.Instantiate(m_nornalDebugInstance, position,Quaternion.identity);

        //�@���̌������Z�b�g
        normalDebugObject.GetComponent<NormalDebug>().Normal = normal;
    }
}
