using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapperController : MonoBehaviour
{
    private Vector3 m_targetPos = Vector3.zero;                 //���̖ڕW�n�_
    private bool m_shouldCheckNextWayPoint = false;             //���̃E�F�C�|�C���g���X�V���ׂ����ǂ���
    private float MOVE_POWER = 20.0f;
    private bool m_isChasePlayer = false;                       //�v���C���[�������ĒǐՂ��Ă��邩
    private GameObject m_targetPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̖ڕW�n�_
        //m_targetPos = this.GetComponent<WayPointChecker>().GetNextWayPoint();
    }

    //���̖ڕW�n�_���X�V����悤�ɖ��߂���
    public void SetCheckNextWayPoint()
	{
        m_shouldCheckNextWayPoint = true;
	}

	private void OnCollisionEnter(Collision col)
	{
        if(col.gameObject.tag != "OwnPlayer")
		{
            Debug.Log("Injured Player");
            Destroy(this.gameObject);
        }
	}

	private void OnTriggerEnter(Collider col)
	{
        if(col.gameObject.tag == "Player" && !m_isChasePlayer)
		{
            m_targetPlayer = col.gameObject;
            m_isChasePlayer = true;
        }
	}

    // Update is called once per frame
    void Update()
    {
        //�^�[�Q�b�g�ƂȂ�v���C���[���������Ȃ��
        if(m_isChasePlayer)
		{
            m_targetPos = m_targetPlayer.transform.position;
        }
		else
		{
            //���̖ڕW�n�_���X�V���ׂ��ł����
            if (m_shouldCheckNextWayPoint)
            {
                //���̒n�_���X�V
                m_targetPos = this.GetComponent<WayPointChecker>().GetNextWayPoint();
                m_shouldCheckNextWayPoint = false;
            }
        }

        //�ڕW�n�_�Ɍ������x�N�g�����`
        Vector3 moveDir = m_targetPos - this.transform.position;
        moveDir.Normalize();
        //���W�b�h�{�f�B�ɖڕW�n�_�����ɗ͂�������
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce((moveDir * MOVE_POWER) - rb.velocity);
    }
}
