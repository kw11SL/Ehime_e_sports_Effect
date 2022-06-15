using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointChecker : MonoBehaviour
{
    private Vector3 m_currentWayPointPos = Vector3.zero;
    private Vector3 m_nextWayPointPos = Vector3.zero;
    private int m_nextWayPointNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "OwnPlayer")
		{
            GameObject nextWayPoint = GameObject.Find("WayPoint1");
            m_nextWayPointPos = nextWayPoint.transform.position;
        }
    }

    public void SetCurrentWayPointDirectly(Vector3 pos, int wayPointNumber)
	{
        Debug.Log("�Z�b�g����ʉߍς݃i���o�[�@:�@" + wayPointNumber);
        m_nextWayPointNumber = wayPointNumber;
        //���̖ړI�n���X�V����B
        SetNextWayPoint(pos, wayPointNumber);
    }

    public void SetNextWayPoint(Vector3 currentPos, int throughNumber)
    {
        //���ɒʉߍς݂̃|�C���g�ƍēx�ڐG���ĕs�v�ȍX�V���s���Ȃ��悤�ɂ���B
        if (m_nextWayPointNumber != throughNumber)
        {
            Debug.Log(this.gameObject.name + " Cant Update Next�@���̃|�C���g��" + m_nextWayPointNumber + " : �ʉ߂����|�C���g��" + throughNumber);
            return;
        }

        //�ʉߍς݃|�C���g�̍��W��ۑ�
        m_currentWayPointPos = currentPos;
        //���̃|�C���g�փC���N�������g
        m_nextWayPointNumber++;
        //���̃|�C���g�̖��O���`
        string nextWayPointName = "WayPoint" + m_nextWayPointNumber;

        Debug.Log(this.gameObject.name + "�̎��̃E�F�C�|�C���g�́@:  " + nextWayPointName);
        //���̃|�C���g�C���X�^���X���擾
        GameObject nextWayPoint = GameObject.Find(nextWayPointName);
        //�Ȃ���΁A0�Ԃɖ߂�
        if (nextWayPoint == null)
        {
            nextWayPoint = GameObject.Find("WayPoint0");
            m_nextWayPointNumber = 0;
        }
        //�V�����|�C���g�̍��W���擾
        m_nextWayPointPos = nextWayPoint.transform.position;

        //���̃C���X�^���X�̎����傪��ł����
        if (gameObject.name == "Snapper")
        { 
            this.GetComponent<SnapperController>().SetCheckNextWayPoint();
        }
    }

    public Vector3 GetNextWayPoint()
	{
        return m_nextWayPointPos;
	}

    public Vector3 GetCurrentWayPoint()
    {
        return m_currentWayPointPos;
    }

    public int GetCurrentWayPointNumber()
    {
        Debug.Log(this.gameObject.name + "CurrentNumber = " + m_nextWayPointNumber);
        return m_nextWayPointNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
