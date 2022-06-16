using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����̃v���C���[�̂��ߋL�^�ꏊ�ŁA�ʐM����͎Ւf�����N���X
public class ParamManage : MonoBehaviour
{
    private int m_orangePeelNum = 0;                    //�I�����C���ł̃Q�[�������I�����W�̔炪�u���ꂽ��
    private int m_playerID = 0;                         //�v���C���[��ID�i�������������̏��j
    private bool m_isOfflineMode = false;               //�I�t���C�����[�h�Ńv���C���邩

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���ړ��Ŕj�����Ȃ�
        DontDestroyOnLoad(this);
    }

    //�����̃v���C���[ID���L�^
    public void SetPlayerID(int id)
	{
        m_playerID = id;
	}

    //�����̃v���C���[ID���擾
    public int GetPlayerID()
	{
        return m_playerID;
	}

    //�I�t���C�����[�h�ŊJ�n����悤��
    public void SetOfflineMode()
	{
        m_isOfflineMode = true;
	}

    //�I�t���C�����[�h��
    public bool GetIsOfflineMode()
	{
        return m_isOfflineMode;
	}

    //�X�e�[�W��ɒu���ꂽ�I�����W�̔���W�v
    public void AddOrangePeelNum()
	{
        m_orangePeelNum++;
	}

    //�Q�[�����ɒu���ꂽ�I�����W�̔�̑���
    public int GetOrangePeelNumOnField()
	{
        return m_orangePeelNum;
	}
}
