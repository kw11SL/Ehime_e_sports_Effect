using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[����ʂ̌��݂̃��b�v���̕\�����X�V����N���X
/// </summary>
public class LapChange : MonoBehaviour
{
    //���b�v�����x��
    [SerializeField]Text m_lapLabel = null;
    //���b�v��
    [SerializeField]int m_lapNo = 1;

    public void SetLapNum(int num)
	{
        m_lapNo = num;
	}

    public int GetLapNum()
	{
        return m_lapNo;
	}

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //���݂̃��b�v���ɂ���ă��b�v�����x����ύX
        m_lapLabel.text = m_lapNo + "/3";
    }
}
