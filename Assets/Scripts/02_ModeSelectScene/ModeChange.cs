using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���[�h�I����ʃN���X
public class ModeChange : MonoBehaviour
{
    //���[�h��
    string[] m_modeName = { "�I�����C����������","CPU��������","�^�C���A�^�b�N","�����Ă�" };
    //���[�h���x��
    [SerializeField] Text m_modeLabel;

    //���[�h�^�C�v
    enum EnModeType
    {
        enOnlineMode,       //�I�����C���ΐ탂�[�h
        enCpuMode,          //CPU�ΐ탂�[�h
        enTimeAttackMode,   //�^�C���A�^�b�N���[�h
        enSettingMode,      //�ݒ胂�[�h
        enMaxModeNum        //���[�h��
    }
    //���ݑI������Ă��郂�[�h
    EnModeType m_nowSelectMode = EnModeType.enOnlineMode;

    //���[�h������
    string[] m_modeExplanationSentence =
    {
        //�I�����C���������񃂁[�h
        "�I�����C���������񃂁[�h�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB",
        //CUP�ΐ탂�[�h
        "CPU�������񃂁[�h�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB",
        //�^�C���A�^�b�N���[�h
        "�^�C���A�^�b�N���[�h�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB",
        //�����Ă����[�h
        "�����Ă����[�h�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB"
    };
    //���[�h���������x��
    [SerializeField] Text m_modeExplanationLabel;

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1"))
        {
            //���̃��[�h�ɑI�����ړ�
            GoNextMode();
        }

        //���̃V�[���ɑJ�ڂ�����
        GoNextScene();

        //���[�h�V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V
        ModeSceneDataUpdate();
    }

    //���̃��[�h�ɑI�����ړ�����֐�
    void GoNextMode()
    {
        //�I������Ă��郂�[�h�����̃��[�h�ɂ���
        m_nowSelectMode++;
        if (m_nowSelectMode >= EnModeType.enMaxModeNum)
        {
            m_nowSelectMode = EnModeType.enOnlineMode;
        }
    }

    //���[�h�V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V������֐�
    void ModeSceneDataUpdate()
    {
        //���[�h�����X�V
        m_modeLabel.text = m_modeName[(int)m_nowSelectMode];
        //���[�h���������X�V
        m_modeExplanationLabel.text = m_modeExplanationSentence[(int)m_nowSelectMode];
    }

    //���̃V�[���ɑJ�ڂ�����֐�
    void GoNextScene()
    {
        //�I������Ă��郂�[�h�ɂ���ĕ���
        switch (m_nowSelectMode)
        {
            //�I�����C���ΐ탂�[�h
            case EnModeType.enOnlineMode:
                break;
            //CPU�ΐ탂�[�h
            case EnModeType.enCpuMode:
                break;
            //�^�C���A�^�b�N���[�h
            case EnModeType.enTimeAttackMode:
                break;
            //�ݒ胂�[�h
            case EnModeType.enSettingMode:
                break;
        }
    }
}