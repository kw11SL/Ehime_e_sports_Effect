using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ProgressChecker : MonoBehaviour
{
    public int MAX_CHECKPOINT_NUM = 3;                  //�X�e�[�W�ɔz�u�����`�F�b�N�|�C���g�̐�
    public int MAX_RAP_NUM = 1;                         //�������邩

    private List<bool> m_checkPoint = new List<bool>();
    private int m_rapCount = 0;
    private GameObject m_rapCountText = null;


    void Start()
    {
        //�`�F�b�N�|�C���g��������
        for (int i = 0; i < MAX_CHECKPOINT_NUM; i++)
		{
            m_checkPoint.Add(false);
		}

        //���b�v�J�E���g�̃e�L�X�g��ǉ�
        m_rapCountText = GameObject.Find("RapCount");
        //���݂̃��b�v���ƍő僉�b�v����\��
        m_rapCountText.GetComponent<Text>().text = "Rap : " + m_rapCount + " / " + MAX_RAP_NUM;
    }

    //�ǂ̒n�_��ʉ߂������𕶎���Ŋm�F
    public void SetThroughPointName(string name)
	{
        //�`�F�b�N�|�C���g�̐���������
        for(int i = 0; i < MAX_CHECKPOINT_NUM; i++)
		{
            //�`�F�b�N�|�C���g�ƂO�`�̐���g�ݍ��킹��
            string pointName = "CheckPoint" + i;
            //���̕�����ƒʉ݂̘A�������������񂪓����Ȃ��
            if(name == pointName)
			{
                //�ʉ߂������Ƃ��m�F
                m_checkPoint[i] = true;
                //�ȍ~�͂���Ȃ��͂��ł��邩��AFOR������ł�B
                break;
			}
		}
	}

    //�S�[���ł��邩�`�F�b�N����
    public bool CheckCanGoal()
	{
        //�S�Ẵ`�F�b�N�|�C���g��
        foreach(var isThrough in m_checkPoint)
        {
            //�ʂ��Ă��Ȃ����
            if(!isThrough)
			{
                //�S�[���ł��Ȃ�
                return false;
			}
		}
        //�����������b�v���𑝂₷
        m_rapCount++;

        //�t���O�����ɖ߂�
        for(int i = 0; i < MAX_CHECKPOINT_NUM; i++)
		{
            m_checkPoint[i] = false;
		}

        //���b�v���̍X�V
        m_rapCountText.GetComponent<Text>().text = "Rap : " + m_rapCount + " / " + MAX_RAP_NUM;

        //�S�[���ł���
        return true;
    }

    //���[�X���I���邩
    public bool IsFinishRacing()
	{
        //�ő僉�b�v���𒴂��Ă�����
        if(m_rapCount >= MAX_RAP_NUM)
		{
            //�I����
            return true;
		}
        else
		{
            //������
            return false;
		}
	}
}
