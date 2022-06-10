using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//���[�h�I����ʃN���X
public class ModeChange : MonoBehaviour
{
    //���[�h��
    string[] m_modeName = { "�I�����C����������","CPU��������","�^�C���A�^�b�N","�����Ă�" };
    //���[�h���x��
    [SerializeField] Text m_modeLabel = null;

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
    [SerializeField] Text m_modeExplanationLabel = null;

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
            //���̃��[�h�ɑI�����ړ�
            GoNextMode();
        }
        //��ʂ����t���b�N���ꂽ��A
        if (m_operation.GetNowOperation() == "left")
        {
            //�O�̃��[�h�ɑI�����ړ�
            GoBackMode();
        }

        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch())
        {
            //���̃V�[���ɑJ�ڂ�����
            GoNextScene();
        }

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
    //�O�̃��[�h�ɑI�����ړ�����֐�
    void GoBackMode()
    {
        //�I������Ă��郂�[�h��O�̃��[�h�ɂ���
        m_nowSelectMode--;
        if (m_nowSelectMode < EnModeType.enOnlineMode)
        {
            m_nowSelectMode = EnModeType.enMaxModeNum-1;
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
        //����̔����������������
        m_operation.TachDataInit();

        //�I������Ă��郂�[�h�ɂ���ĕ���
        switch (m_nowSelectMode)
        {
            //�I�����C���ΐ탂�[�h
            case EnModeType.enOnlineMode:
                //�ݒ胂�[�h�V�[���ɑJ��
                SceneManager.LoadScene("04_CharaSelectScene");
                break;
            //CPU�ΐ탂�[�h
            case EnModeType.enCpuMode:
                //�ݒ胂�[�h�V�[���ɑJ��
                SceneManager.LoadScene("04_CharaSelectScene");
                break;
            //�^�C���A�^�b�N���[�h
            case EnModeType.enTimeAttackMode:
                //�ݒ胂�[�h�V�[���ɑJ��
                SceneManager.LoadScene("04_CharaSelectScene");
                break;
            //�ݒ胂�[�h
            case EnModeType.enSettingMode:
                //�ݒ胂�[�h�V�[���ɑJ��
                SceneManager.LoadScene("03_SettingScene");
                break;
        }
    }
}