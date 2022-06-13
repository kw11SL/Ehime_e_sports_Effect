using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �}�b�`���O����������̃V�[���J�ڃN���X
/// </summary>
public class MatchingSceneChange : MonoBehaviour
{
    //����V�X�e��
    Operation m_operation = null;

    void Start()
    {
        //����V�X�e���̃Q�[���I�u�W�F�N�g���������X�N���v�g���g�p����
        m_operation = GameObject.Find("OperationSystem").GetComponent<Operation>();
    }

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch())
        {
            //���̃V�[���ɑJ�ڂ�����
            GoNextScene();
        }
    }

    //���̃V�[���ɑJ�ڂ�����֐�
    void GoNextScene()
    {
        //����̔����������������
        m_operation.TachDataInit();

        //�Q�[���V�[���ɑJ��
        SceneManager.LoadScene("08_GameScene");
    }
}