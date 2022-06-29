using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CPU�̃��[�X�L�����p�̃X�N���v�g
/// </summary>
public class RaceAIScript : MonoBehaviour
{
    private Rigidbody m_rigidbody = null;                                      //AI�L�����N�^�[�̍���
    private const float m_kMaxSpeed = 25.0f;                                   //�ō����x
    private Vector3 m_RightSteeringVector = new Vector3(0.0f, 5.0f, 0.0f);     //�E�����ւ̉�]�p�x�N�g��
    private Vector3 m_LeftSteeringVector = new Vector3(0.0f, -5.0f, 0.0f);     //�������ւ̉�]�p�x�N�g��

    //AI�̓E�F�C�|�C���g�Ƃ̋����ɉ����Ăǂ̊p�x�ȓ��Ȃ�n���h����؂邩��ω�������
    //��:
    //���������̏ꍇ�������E�F�C�|�C���g�ւ̌����Ɛi�s����������Ă��n���h����؂�K�v���Ȃ�
    //�߂������̏ꍇ���E�F�C�|�C���g�Ɍ������ăn���h����؂�Ȃ���΂����Ȃ�
    private const float m_kMinSteeringLength = 10.0f;   //AI���n���h����؂锻�f������p�x�̕����ŏ��ɂȂ鋗��
    private const float m_kMaxSteeringAngle = 1.0f;     //�n���h����؂锻�f������p�x�̕��̍ő�
    private const float m_kMinSteeringAngle = 0.1f;     //�n���h����؂锻�f������p�x�̕��̍ŏ�

    private void Awake()
    {
        //���̂��擾
        m_rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���ݖڎw���Ă���E�F�C�|�C���g�ւ̃x�N�g�����v�Z
        Vector3 toNextPoint = this.GetComponent<WayPointChecker>().GetNextWayPoint() - this.transform.position;

        //�ڎw���������v�Z
        Vector3 newForward = toNextPoint;
        newForward.y = 0.0f;
        newForward.Normalize() ;

        //�E�F�C�|�C���g�֌������߂̊p�x���v�Z(���E�̔��f�����邽�߉E�����̃x�N�g���ƌv�Z)
        float steeringAngle = Vector3.Dot(transform.right, newForward);

        //���̃E�F�C�|�C���g�ւ̋������擾
        float toNextLength = toNextPoint.magnitude;

        //�E�F�C�|�C���g�ւ̋�������AI���n���h����؂锻�f������p�x�̕����ŏ��ɂȂ鋗���ɑ΂��Ă̊������v�Z
        float lerpRate = (m_kMinSteeringLength - toNextLength) / m_kMinSteeringLength;

        //��������n���h����؂�p�x�̂������l���v�Z
        float angleThresold = Mathf.Lerp(m_kMinSteeringAngle, m_kMaxSteeringAngle, lerpRate);

        //�n���h����؂�p�x���������l���Ȃ�
        if(Mathf.Abs(steeringAngle) > angleThresold)
        {
            if (steeringAngle > 0.0f)
            {
                //�E�Ƀn���h����؂�(��]������)
                transform.Rotate(m_RightSteeringVector);
            }
            else
            {
                //���Ƀn���h����؂�(��]������)
                transform.Rotate(m_LeftSteeringVector);
            }
        }

        //���̂ɗ͂�������
        m_rigidbody.AddForce(transform.forward * m_kMaxSpeed - m_rigidbody.velocity);

        //���ɖڎw���E�F�C�|�C���g�̈ʒu���o��
        Debug.Log(GetComponent<WayPointChecker>().GetNextWayPoint());
    }

}
