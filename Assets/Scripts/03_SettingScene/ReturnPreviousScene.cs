using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///�ݒ�V�[������O�̃V�[��(���[�h�I���V�[��)�ɖ߂�N���X
/// </summary>
public class ReturnPreviousScene : MonoBehaviour
{
    //����V�X�e��
    Operation m_operation = null;
    //�g�O���̒l
    ToggleOnOff m_blindToggle = null;
    //���[�U�[���ݒ肵�������i�[���Ēu���ۊǏꏊ
    UserSettingData m_userSettingData = null;

    void Start()
    {
        //����V�X�e���̃Q�[���I�u�W�F�N�g���������Q�[���R���|�[�l���g���擾����
        m_operation = GameObject.Find("OperationSystem").GetComponent<Operation>();
    }

    void Update()
    {
        //��ʂ����������ꂽ��A
        if (m_operation.GetIsLongTouch)
        {
            //�O�̃V�[���ɖ߂�
            GoPreviousScene();
        }
    }

    //�O�̃V�[���ɖ߂�֐�
    void GoPreviousScene()
    {
        //����̔����������������
        m_operation.TachDataInit();

        //�u���C���h�؂�ւ��̃g�O���̃Q�[���I�u�W�F�N�g���������Q�[���R���|�[�l���g���擾����
        m_blindToggle = GameObject.Find("BlindToggle").GetComponent<ToggleOnOff>();

        //���[�U�[�ݒ�f�[�^�̃Q�[���I�u�W�F�N�g���������A
        //�Q�[���R���|�[�l���g���擾����
        m_userSettingData = GameObject.Find("UserSettingDataStorageSystem").GetComponent<UserSettingData>();
        //�u���C���h���[�h���ǂ����̃f�[�^��ۑ�
        m_userSettingData.GetSetBlindMode = m_blindToggle.GetToggleValue;

        //���[�h�I���V�[���ɑJ��
        SceneManager.LoadScene("02_ModeSelectScene");
    }
}
