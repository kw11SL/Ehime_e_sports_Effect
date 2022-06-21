using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchData : MonoBehaviour
{
    //���[�U�[���ݒ肵�������i�[���Ēu���ۊǏꏊ
    UserSettingData m_userSettingData = null;
    //�u���C���_�[�Q�[���I�u�W�F�N�g
    GameObject m_blinderPanel = null;

    void Start()
    {
        //���[�U�[�ݒ�f�[�^�̃Q�[���I�u�W�F�N�g���������A
        //�Q�[���R���|�[�l���g���擾����
        m_userSettingData = GameObject.Find("UserSettingDataStorageSystem").GetComponent<UserSettingData>();
        //���܂ŕۑ����ꂽ�f�[�^���R���\�[���Ƀf�o�b�N�\��
        m_userSettingData.IndicateDebugLog();

        //�u���C���h���[�h��OFF�̂Ƃ��A
        if(!m_userSettingData.GetSetBlindMode)
        {
            //�u���C���_�[�̃Q�[���I�u�W�F�N�g���擾
            m_blinderPanel = GameObject.Find("BlinderPanel");
            //�^���Â�������ʂ����ɖ߂�
            m_blinderPanel.SetActive(false);
        }
    }
}
