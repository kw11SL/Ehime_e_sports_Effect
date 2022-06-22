using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


//�E�F�C�|�C���g�o�ߏ����Ǝ��̃E�F�C�|�C���g��񎦂���N���X
public class WayPointChecker : MonoBehaviour
{
    private Vector3 m_currentWayPointPos = Vector3.zero;        //���ݒʉߍς݂̃E�F�C�|�C���g�̍��W
    private Vector3 m_nextWayPointPos = Vector3.zero;           //���̃E�F�C�|�C���g�̍��W
    private int m_nextWayPointNumber = 0;                       //���̃E�F�C�|�C���g�̔ԍ�

    // Start is called before the first frame update
    void Start()
    {
        //���̃N���X�������Ă���̂������̃v���C���[��������
        if(this.gameObject.tag == "OwnPlayer")
		{
            //�J������]�}���̓s����ŏ��̓E�F�C�|�C���g1�ڎw���B
            GameObject nextWayPoint = GameObject.Find("WayPoint1");
            //���̃E�F�C�|�C���g�̍��W���擾
            m_nextWayPointPos = nextWayPoint.transform.position;
        }
    }

    //���̃N���X�i��Ƀv���C���[�j���璼�ڌ��ݒ��߂Œʉ߂����E�F�C�|�C���g�̍��W�Ɣԍ���ݒ�
    public void SetCurrentWayPointDirectly(Vector3 pos, int wayPointNumber)
	{
        //���֐��Ŏ��̃E�F�C�|�C���g���X�V���邽��1�O�̃|�C���g�ŏ�����
        m_nextWayPointNumber = wayPointNumber;
        //���̖ړI�n���X�V����B
        SetNextWayPoint(pos, wayPointNumber);
    }

    //���̃E�F�C�|�C���g�Ɍ��������߁A�ϐ����X�V����
    public void SetNextWayPoint(Vector3 currentPos, int throughNumber)
    {
        //���ɒʉߍς݂̃|�C���g�ƍēx�ڐG���ĕs�v�ȍX�V���s���Ȃ��悤�ɂ���B
        if (m_nextWayPointNumber != throughNumber)
        {
            return;
        }

        //�ʉߍς݃|�C���g�̍��W��ۑ�
        m_currentWayPointPos = currentPos;
        //���̃|�C���g�փC���N�������g
        m_nextWayPointNumber++;
        //���̃|�C���g�̖��O���`
        string nextWayPointName = "WayPoint" + m_nextWayPointNumber;
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

        //���̃E�F�C�|�C���g�̔ԍ������[���v���p�e�B�ɕۑ�
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        //�v���C���[���{WayPointNumber�Ƃ������O���쐬 ex.)Player2WayPointNumber
        string name = PhotonNetwork.NickName + "WayPointNumber";
        //�E�F�C�|�C���g�ԍ���ݒ�
        hashtable[name] = m_nextWayPointNumber;
        //���[���v���p�e�B�̍X�V
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);



        //���̃C���X�^���X�̎����傪��ł����
        if (gameObject.name == "Snapper")
        { 
            //�����̎��̃E�F�C�|�C���g�͂ǂ����X�V������悤�ɓ`����
            this.GetComponent<SnapperController>().SetCheckNextWayPoint();
        }
    }

    //���̃E�F�C�|�C���g�̍��W��Ԃ�
    public Vector3 GetNextWayPoint()
	{
        return m_nextWayPointPos;
	}

    //���߂Œʉ߂����E�F�C�|�C���g�̍��W��Ԃ�
    public Vector3 GetCurrentWayPoint()
    {
        return m_currentWayPointPos;
    }

    //���߂Œʉ߂����E�F�C�|�C���g�ԍ���Ԃ�
    public int GetCurrentWayPointNumber()
    {
        return m_nextWayPointNumber;
    }
}
