using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject m_ownPlayer = null;        //�ǐ��Ώۂ̃Q�[���I�u�W�F�N�g�i�v���C���[�j
    private bool m_isGetOwnPlayer = false;        //�v���C���[�C���X�^���X���m�ۂł�����

    public float BEHIND_RATE_FROM_PLAYER = 8.0f; //�J�����̈ʒu���ǂ̂��炢�v���C���[�̌��ɂ��邩
    public float UPPER_RATE_FROM_PLAYER = 5.0f;   //�J�����̈ʒu���ǂ̂��炢�v���C���[�̏�ɂ��邩
    
    // Update is called once per frame
    void Update()
    {
        //�ʐM�̊֌W��A�����ŃC���X�^���X���m�ۂł���܂ŒT��
        if(!m_isGetOwnPlayer)
		{
            m_ownPlayer = GameObject.Find("OwnPlayer");
            if(m_ownPlayer != null)
			{
                m_isGetOwnPlayer = true;
            }
        }
      
        //�J�����̈ʒu�̓v���C���[�̏������̈ʒu��
        Vector3 cameraPos = m_ownPlayer.transform.position + (m_ownPlayer.transform.forward * -1.0f) * BEHIND_RATE_FROM_PLAYER;
        //���������ݒ肷��B
        cameraPos.y += UPPER_RATE_FROM_PLAYER;

        //���C���J�������擾
        Camera camera = Camera.main;
        //�ʒu��ݒ肵
        camera.gameObject.transform.position = cameraPos;
        //���ڑΏۂ̓v���C���[�ɂ���
        camera.gameObject.transform.LookAt(m_ownPlayer.transform);
    }
}
