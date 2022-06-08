using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�e�[�W�I����ʃN���X
/// </summary>
public class StageSelectChange : MonoBehaviour
{
    //�X�e�[�W��
    string[] m_stageName = { "�X�e�[�W1", "�X�e�[�W2", "�X�e�[�W3" };
    //�X�e�[�W�����x��
    [SerializeField] Text m_stageNameLabel;

    //��Փx�X�v���C�g
    [SerializeField] Sprite[] m_difficlutySprite;
    //��Փx�C���[�W
    [SerializeField] Image m_difficlutyImage;
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
    [SerializeField] Text m_stageExplanationLabel;

    enum EnStageType
    {
        enStage1,       //�X�e�[�W1
        enStage2,       //�X�e�[�W2
        enStage3,       //�X�e�[�W3
        enMaxStageNum   //�ő�X�e�[�W��
    }
    //���ݑI������Ă���X�e�[�W
    EnStageType m_nowSelectStage = EnStageType.enStage1;

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1"))
        {
            //���̃X�e�[�W�ɑI�����ړ�
            GoNextStage();
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
}