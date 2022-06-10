using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �L�����N�^�[�I����ʃN���X
/// </summary>
public class CharaSelectChange : MonoBehaviour
{
    //�L�����N�^�[��
    string[] m_charaName = { "�݂����", "�q�݂����", "�_�[�N�݂����" };
    //�L�����N�^�[�����x��
    [SerializeField] Text m_charaNameLabel = null;

    //�L�����N�^�[�X�e�[�^�X
    string[] m_charaStatus = { "S\nS\nS\nS", "A\nA\nA\nA", "B\nB\nB\nB" };
    //�L�����X�e�[�^�X���x��
    [SerializeField] Text m_charaStatusLabel = null;

    //�L�����N�^�[������
    string[] m_charaExplanationSentence =
    {
        //�݂����
        "���Ђ߂̃}�X�R�b�g�L�����N�^�[�B�݂����B���킢���B���킢���B���킢���B���킢���B",
        //�q�݂����
        "���Ђ߂̃}�X�R�b�g�L�����N�^�[�B�q�݂����B���킢���B���킢���B���킢���B���킢���B",
        //�_�[�N�݂����
        "���Ђ߂̃}�X�R�b�g�L�����N�^�[�B�_�[�N�݂����B���킢���B���킢���B���킢���B���킢���B"
    };
    //�L�����N�^�[�������x��
    [SerializeField] Text m_charaExplanationLabel = null;

    enum EnCharaType
    {
        enMikyan,        //�݂����
        enKomikyan,      //�q�݂����
        enDarkmikyan,    //�_�[�N�݂����
        enMaxCharaNum    //�ő�L�����N�^�[��
    }
    //���ݑI������Ă���L�����N�^�[
    EnCharaType m_nowSelectChara = EnCharaType.enMikyan;

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
            //���̃L�����N�^�[�ɑI�����ړ�
            GoNextChara();
        }
        //��ʂ����t���b�N���ꂽ��A
        if (m_operation.GetNowOperation() == "left")
        {
            //�O�̃L�����N�^�[�ɑI�����ړ�
            GoBackChara();
        }

        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch())
        {
            //���̃V�[���ɑJ�ڂ�����
            GoNextScene();
        }

        //�L�����I���V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V
        CharaSelectSceneDataUpdate();
    }

    //���̃L�����N�^�[�ɑI�����ړ�����֐�
    void GoNextChara()
    {
        //�I������Ă���L�����N�^�[�����̃L�����N�^�[�ɂ���
        m_nowSelectChara++;
        if (m_nowSelectChara >= EnCharaType.enMaxCharaNum)
        {
            m_nowSelectChara = EnCharaType.enMikyan;
        }
    }
    //�O�̃L�����N�^�[�ɑI�����ړ�����֐�
    void GoBackChara()
    {
        //�I������Ă��郂�[�h��O�̃��[�h�ɂ���
        m_nowSelectChara--;
        if (m_nowSelectChara < EnCharaType.enMikyan)
        {
            m_nowSelectChara = EnCharaType.enMaxCharaNum - 1;
        }
    }

    //�L�����I���V�[���̃e�L�X�g�Ȃǂ̃f�[�^���X�V������֐�
    void CharaSelectSceneDataUpdate()
    {
        //�L�����N�^�[�����x�����X�V
        m_charaNameLabel.text = m_charaName[(int)m_nowSelectChara];
        //�L�����N�^�[�X�e�[�^�X���x�����X�V
        m_charaStatusLabel.text = m_charaStatus[(int)m_nowSelectChara];
        //�L�����N�^�[�������x�����X�V
        m_charaExplanationLabel.text = m_charaExplanationSentence[(int)m_nowSelectChara];
    }

    //���̃V�[���ɑJ�ڂ�����֐�
    void GoNextScene()
    {
        //����̔����������������
        m_operation.TachDataInit();

        //�X�e�[�W�I���V�[���ɑJ��
        SceneManager.LoadScene("05_StageSelectScene");
    }
}