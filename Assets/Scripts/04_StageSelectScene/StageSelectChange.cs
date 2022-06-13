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
    [SerializeField] string[] m_stageName = null;
    //�X�e�[�W�����x��
    [SerializeField] Text m_stageNameLabel = null;

    //��Փx�X�v���C�g
    [SerializeField] Sprite[] m_difficlutySprite = null;
    //��Փx�C���[�W
    [SerializeField] Image m_difficlutyImage = null;
    //�X�e�[�W���Ƃ̓�Փx(0,�ȒP�B1,���ʁB2,���)
    [SerializeField]int[] m_stageDifficluty = { 0, 1, 2 };

    //�X�e�[�W������
    [SerializeField] string[] m_stageExplanationSentence = null;
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
    Operation m_operation = null;

    //�I���ړ������Ă��邩
    bool m_selectMove = false;

    //�ړ����ԃJ�E���^�[
    int m_selectMoveCount = 0;

    CircleCenterRotateAround m_circleCenterRotateAround = null;

    void Start()
    {
        //����V�X�e���̃Q�[���I�u�W�F�N�g���������X�N���v�g���g�p����
        m_operation = GameObject.Find("OperationSystem").GetComponent<Operation>();
        //�~�̒��S��d�Ԃ���]����@�\�t���̃Q�[���I�u�W�F�N�g���������X�N���v�g���g�p����
        m_circleCenterRotateAround = GameObject.Find("Train").GetComponent<CircleCenterRotateAround>();
    }

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        if (!m_selectMove)
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

        //�d�Ԃ̈ړ��ɍ��킹�đI�����Ă���f�[�^�����킹��J�E���^�[
        Count();

        //�X�e�[�W�I���V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V
        StageSceneDataUpdate();
    }

    //���̃X�e�[�W�ɑI�����ړ�����֐�
    void GoNextStage()
    {
        //�I���ړ���Ԃɂ���
        m_selectMove = true;
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
        //�I���ړ���Ԃɂ���
        m_selectMove = true;
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

    //�d�Ԃ̈ړ��ɍ��킹�đI�����Ă���f�[�^�����킹��J�E���^�[
    void Count()
    {
        //�I���ړ���Ԃ���Ȃ��Ƃ��͏��������Ȃ��B
        if (!m_selectMove) return;

        //�J�E���g�v��
        m_selectMoveCount++;

        //�J�E���g���w�肵�����l���傫���Ȃ�����A
        if (m_selectMoveCount > m_circleCenterRotateAround.GetCountTime())
        {
            //�I���ړ����Ă��Ȃ���Ԃɖ߂�
            m_selectMove = false;
            //�J�E���g�̏�����
            m_selectMoveCount = 0;
        }
    }
}