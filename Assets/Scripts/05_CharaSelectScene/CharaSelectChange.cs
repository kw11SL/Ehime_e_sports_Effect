using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �L�����N�^�[�I����ʃN���X
/// </summary>
public class CharaSelectChange : MonoBehaviour
{
    //�L�����N�^�[��
    string[] m_charaName = { "�݂����", "�q�݂����", "�_�[�N�݂����" };
    //�L�����N�^�[�����x��
    [SerializeField] Text m_charaNameLabel;

    //�L�����N�^�[�X�e�[�^�X
    string[] m_charaStatus = { "S\nS\nS\nS", "A\nA\nA\nA", "B\nB\nB\nB" };
    //�L�����X�e�[�^�X���x��
    [SerializeField] Text m_charaStatusLabel;

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
    [SerializeField] Text m_charaExplanationLabel;

    enum EnCharaType
    {
        enMikyan,        //�݂����
        enKomikyan,      //�q�݂����
        enDarkmikyan,    //�_�[�N�݂����
        enMaxCharaNum    //�ő�L�����N�^�[��
    }
    //���ݑI������Ă���L�����N�^�[
    EnCharaType m_nowSelectChara = EnCharaType.enMikyan;

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1"))
        {
            //���̃L�����N�^�[�ɑI�����ړ�
            GoNextChara();
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
}
