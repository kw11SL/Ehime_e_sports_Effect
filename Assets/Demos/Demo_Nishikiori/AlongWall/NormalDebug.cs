using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�o�b�O�p�@�@����`�悷��I�u�W�F�N�g
/// </summary>
public class NormalDebug : MonoBehaviour
{
    //�`�悷��@��
    private Vector3 m_normal = new Vector3(1.0f,0.0f,0.0f);

    //�@���̃v���p�e�B
    public Vector3 Normal
    { 
        get
        {
            return this.m_normal;
        }
        set
        {
            //���K������
            m_normal = value.normalized;
        }
    }

    //�`�悷��@���̒���
    public float m_lineLength = 2.5f;

    // Update is called once per frame
    void Update()
    {
        //�@����`�悷��
        Debug.DrawRay(transform.position, m_normal * m_lineLength, Color.magenta);
    }
}
