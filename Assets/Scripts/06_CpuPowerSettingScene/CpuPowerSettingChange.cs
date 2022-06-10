using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// CPU�̂悳�I����ʃN���X
/// </summary>
public class CpuPowerSettingChange : MonoBehaviour
{
    //CPU�̋�����
    string[] m_strengthName = { "��킢", "�ӂ�", "�悢" };
    //CPU�̋������x��
    [SerializeField] Text m_strengthLabel = null;

    enum EnCpuPowerType
    {
        enWeak,             //�ア
        enNormal,           //����
        enStrong,           //����
        enMaxCpuPowserNum   //�ő�CPU������
    }
    //���ݑI������Ă��鋭��
    EnCpuPowerType m_nowSelectStrength = EnCpuPowerType.enWeak;

    //����V�X�e��
    OperationNew m_operation = null;

    void Start()
    {
        //����V�X�e���̃Q�[���I�u�W�F�N�g���������X�N���v�g���g�p����
        m_operation = GameObject.Find("OperationSystem").GetComponent<OperationNew>();
    }

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ��E�t���b�N���ꂽ��A
        if (m_operation.GetNowOperation() == "right")
        {
            //����CPU�̋����ɑI�����ړ�
            GoNextCpuPower();
        }
        //��ʂ����t���b�N���ꂽ��A
        if (m_operation.GetNowOperation() == "left")
        {
            //�O��CPU�̋����ɑI�����ړ�
            GoBackStage();
        }

        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch())
        {
            //���̃V�[���ɑJ�ڂ�����
            GoNextScene();
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
    //�O��CPU�̋����ɑI�����ړ�����֐�
    void GoBackStage()
    {
        //�I������Ă���CPU�̋�����O��CPU�̋����ɂ���
        m_nowSelectStrength--;
        if (m_nowSelectStrength < EnCpuPowerType.enWeak)
        {
            m_nowSelectStrength = EnCpuPowerType.enMaxCpuPowserNum - 1;
        }
    }

    //���̃V�[���ɑJ�ڂ�����֐�
    void GoNextScene()
    {
        //����̔����������������
        m_operation.TachDataInit();

        //�}�b�`���O�V�[���ɑJ��
        SceneManager.LoadScene("07_MatchingScene");
    }
}
