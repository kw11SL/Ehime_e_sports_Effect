using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǉ����Ɉړ�����N���X�̌��ؗp�Q�[���I�u�W�F�N�g
/// </summary>
public class TestMoveObject : MonoBehaviour
{
    private Rigidbody m_rigidbody = null;                                   //�Q�[���I�u�W�F�N�g�̍���
    private Vector3 m_moveDirection = new Vector3(-1.0f, 0.0f, -1.0f);      //�Q�[���I�u�W�F�N�g�̈ړ�����
    private float m_moveSpeed = 100.0f;                                     //�Q�[���I�u�W�F�N�g�̈ړ����x
    private AlongWall m_alongWall = null;                                   //�ǉ����̈ړ������̌v�Z�N���X

    // Start is called before the first frame update
    void Start()
    {
        //���̂ƕǉ����ړ��v�Z�N���X��������
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_alongWall = new AlongWall();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���̂ɗ͂�`����
        m_rigidbody.AddForce(m_moveDirection * m_moveSpeed);
    }

    void Update()
    {
        //�f�o�b�O�p�A�ړ������̃x�N�g������ŕ`��
        Debug.DrawRay(m_rigidbody.position, m_moveDirection, Color.green);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //�ǂɂԂ��������p�̏���
        m_alongWall.CollisionEnter(collision,m_rigidbody,ref m_moveDirection);
    }

}
