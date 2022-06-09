using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CPU�̂悳�I����ʃN���X
/// </summary>
public class CpuPowerSettingChange : MonoBehaviour
{
    //CPU�̋�����
    string[] m_strengthName = { "��킢", "�ӂ�", "�悢" };
    //CPU�̋������x��
    [SerializeField] Text m_strengthLabel;

    enum EnCpuPowerType
    {
        enWeak,             //�ア
        enNormal,           //����
        enStrong,           //����
        enMaxCpuPowserNum   //�ő�CPU������
    }
    //���ݑI������Ă��鋭��
    EnCpuPowerType m_nowSelectStrength = EnCpuPowerType.enWeak;

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1"))
        {
            //����CPU�̋����ɑI�����ړ�
            GoNextCpuPower();
        }

        //CPU�̋������x�����X�V
        m_strengthLabel.text = m_strengthName[(int)m_nowSelectStrength];
    }

    //����CPU�̋����ɑI�����ړ�����֐�
    void GoNextCpuPower()
    {
        //�I������Ă���CPU�̋��������̋����ɂ���
        m_nowSelectStrength++;
        if (m_nowSelectStrength >= EnCpuPowerType.enMaxCpuPowserNum)
        {
            m_nowSelectStrength = EnCpuPowerType.enWeak;
        }
    }
}
