using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �X�e�[�W�I����ʃN���X
/// </summary>
public class StageSelectChange : MonoBehaviour
{
    //�X�e�[�W��
    string[] m_stageName = { "�X�e�[�W1", "�X�e�[�W2", "�X�e�[�W3" };
    //�X�e�[�W�����x��
    [SerializeField] Text m_stageNameLabel = null;

    //��Փx�X�v���C�g
    [SerializeField] Sprite[] m_difficlutySprite = null;
    //��Փx�C���[�W
    [SerializeField] Image m_difficlutyImage = null;
    //�X�e�[�W���Ƃ̓�Փx(0,�ȒP�B1,���ʁB2,���)
    int[] m_stageDifficluty = { 0, 1, 2 };

    //�X�e�[�W������
    string[] m_stageExplanationSentence =
    {
        //�X�e�[�W1
        "�����̓X�e�[�W1�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB",
        //�X�e�[�W2
        "�����̓X�e�[�W2�B���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB",
        //�X�e�[�W3
        "�����̓X�e�[�W3���ււł��ււȂ��ււł���B���ււł��ւցB���ււււււցB"
    };
    //�X�e�[�W���������x��
    [SerializeField] Text m_stageExplanationLabel = null;

    enum EnStageType
    {
        enStage1,       //�X�e�[�W1
        enStage2,       //�X�e�[�W2
        enStage3,       //�X�e�[�W3
        enMaxStageNum   //�ő�X�e�[�W��
    }
    //���ݑI������Ă���X�e�[�W
    EnStageType m_nowSelectStage = EnStageType.enStage1;

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
            //���̃X�e�[�W�ɑI�����ړ�
            GoNextStage();
        }
        //��ʂ����t���b�N���ꂽ��A
        if (m_operation.GetNowOperation() == "left")
        {
            //�O�̃X�e�[�W�ɑI�����ړ�
            GoBackStage();
        }

        //�I������Ă���X�e�[�W�ɂ���ĕ���
        switch (m_nowSelectStage)
        {
            //�X�e�[�W1
            case EnStageType.enStage1:
                break;
            //�X�e�[�W2
            case EnStageType.enStage2:
                break;
            //�X�e�[�W3
            case EnStageType.enStage3:
                break;
        }

        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch())
        {
            //���̃V�[���ɑJ�ڂ�����
            GoNextScene();
        }

        //�X�e�[�W�I���V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V
        StageSceneDataUpdate();
    }

    //���̃X�e�[�W�ɑI�����ړ�����֐�
    void GoNextStage()
    {
        //�I������Ă���X�e�[�W�����̃X�e�[�W�ɂ���
        m_nowSelectStage++;
        if (m_nowSelectStage >= EnStageType.enMaxStageNum)
        {
            m_nowSelectStage = EnStageType.enStage1;
        }
    }
    //�O�̃X�e�[�W�ɑI�����ړ�����֐�
    void GoBackStage()
    {
        //�I������Ă���X�e�[�W��O�̃X�e�[�W�ɂ���
        m_nowSelectStage--;
        if (m_nowSelectStage < EnStageType.enStage1)
        {
            m_nowSelectStage = EnStageType.enMaxStageNum - 1;
        }
    }

    //�X�e�[�W�I���V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V������֐�
    void StageSceneDataUpdate()
    {
        //��Փx�摜���X�V
        m_difficlutyImage.sprite = m_difficlutySprite[m_stageDifficluty[(int)m_nowSelectStage]];
        //�X�e�[�W�����x�����X�V
        m_stageNameLabel.text = m_stageName[(int)m_nowSelectStage];
        //�X�e�[�W���������X�V
        m_stageExplanationLabel.text = m_stageExplanationSentence[(int)m_nowSelectStage];
    }

    //���̃V�[���ɑJ�ڂ�����֐�
    void GoNextScene()
    {
        //����̔����������������
        m_operation.TachDataInit();

        //CPU�����ݒ�I���V�[���ɑJ��
        SceneManager.LoadScene("06_CpuPowerSettingScene");
    }
}