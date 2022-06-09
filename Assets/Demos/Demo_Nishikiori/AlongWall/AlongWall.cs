using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǂɐڐG�����ہA�ǉ����Ɉړ�����x�N�g�����v�Z�����̂ɓK������N���X�B
/// </summary>
public class AlongWall
{
    //�ǉ����Ɉړ����鑬�x�B
    public float m_alongWallSpeed { get; set; } = 5.0f;

    //�Փ˔���Ƃ���Q�[���I�u�W�F�N�g�̃^�O
    public string m_collideTag { get; set; } = "Wall";

    /// <summary>
    /// �I�u�W�F�N�g���ǂɐڐG�����u�ԁA�ǉ����Ɉړ�����x�N�g�����v�Z�����̂ɓK������B
    /// </summary>
    /// <param name="collision">�I�u�W�F�N�g���ڐG�����R���W����</param>
    /// <param name="rigidbody">�I�u�W�F�N�g�̍���</param>
    /// <param name="moveDirection">(�Q��)�I�u�W�F�N�g�̈ړ�����</param>
    public void CollisionEnter(Collision collision, Rigidbody rigidbody, ref Vector3 moveDirection)
    {
        //�R���W���������Q�[���I�u�W�F�N�g���Փ˔�����s���^�O�������Ă��Ȃ��ꍇ�͉������Ȃ��B
        if (collision.gameObject.CompareTag(m_collideTag) == false)
        {
            return;
        }

        //���̂̈ړ��������擾
        Vector3 velocity = rigidbody.velocity;
        velocity.Normalize();

        //�ڐG���������蔻��̖@�����擾
        Vector3 normal = collision.contacts[0].normal;

        //�ǂɉ����ē����x�N�g�����v�Z���A�ړ������Ƃ��Ċi�[
        moveDirection = velocity - (Vector3.Dot(velocity, normal) * normal);

        //�����������̌��������Z�b�g���A�X�s�[�h���Z�b�g
        rigidbody.velocity = m_alongWallSpeed * moveDirection;
    }
}