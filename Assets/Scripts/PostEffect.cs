using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �g�����W�V�����N���X
/// </summary>
public class PostEffect : MonoBehaviour
{
    //�|�X�g�G�t�F�N�g�V�F�[�_�[����̃}�e���A��
    public Material m_postEffectMat;

    //��ʐ؂�ւ�
    const float m_kFadeMax = 128.0f;
    float m_fadeCount = 1.0f;
    [SerializeField]bool m_fadeFlag;

    //�X�V�֐�
    void Update()
    {
        //��ʐ؂�ւ�
        //(�������F���{�^���N���b�N��������؂�ւ�)
        if(Input.GetMouseButtonDown(0))
        {
            //�t�F�[�h��؂�ւ���
            m_fadeFlag = !m_fadeFlag;
        }

        m_fadeCount = Mathf.Clamp(m_fadeCount + (m_fadeFlag ? 1.0f : -1.0f) / m_kFadeMax, 0.0f, 1.0f);

        m_postEffectMat.SetFloat("_FadeCount", m_fadeCount);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, m_postEffectMat);
    }
}