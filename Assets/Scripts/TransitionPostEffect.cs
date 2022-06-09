using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPostEffect : MonoBehaviour
{
    [SerializeField] private Material postEffectMaterial; //�g�����W�V�����̃}�e���A��
    [SerializeField] private float transitionTime = 2f; //�g�����W�V�����̎���
    readonly int _progressId = Shader.PropertyToID("_Progress"); //�V�F�[�_�[�v���p�e�B��Reference��
    bool m_isTransitionIn = true;   //�g�����W�V������In��Out���t���O
    bool m_isPlay = false;  //�g�����W�V�������Đ�����Ă��邩�t���O

    /// <summary>
    /// �J�n���Ɏ��s
    /// </summary>
    void Start()
    {
        if (postEffectMaterial != null)
        {
            //�g�����W�V�������J�n
            StartCoroutine(InTransition());
        }

        //�V�[���J�ڂ��Ă����̃g�����W�V�����̃I�u�W�F�N�g�͍폜���Ȃ�
        DontDestroyOnLoad(this);
    }

    //�g�����W�V�����N���֐�
    public void OnTransition()
    {
       //In�̂Ƃ��A
       if (m_isTransitionIn)
       {
           //�g�����W�V�������Đ����̂Ƃ��A
           if (m_isPlay)
           {
               //Out�g�����W�V��������U�I��������B
               StopCoroutine(OutTransition());
           }
           //In�g�����W�V�������J�n
           StartCoroutine(InTransition());
       }
       //Out�̂Ƃ��A
       else
       {
           //�g�����W�V�������Đ����̂Ƃ��A
           if (m_isPlay)
           {
               //In�g�����W�V��������U�I��������B
               StopCoroutine(InTransition());
           }

           //Out�g�����W�V�������J�n
           StartCoroutine(OutTransition());
       }
    }

    /// <summary>
    ///In�g�����W�V����
    /// </summary>
    IEnumerator InTransition()
    {
        //�g�����W�V����������Out�����s�����悤�ɂ���B
        m_isTransitionIn = !m_isTransitionIn;

        //�g�����W�V�����Đ����ɂ���B
        m_isPlay = true;

        float t = 0f;
        while (t < transitionTime)
        {
            float progress = t / transitionTime;

            // �V�F�[�_�[��_Progress�ɒl��ݒ�
            postEffectMaterial.SetFloat(_progressId, progress);
            yield return null;

            t += Time.deltaTime;
        }

        postEffectMaterial.SetFloat(_progressId, 1f);

        //�g�����W�V������~���ɂ���B
        m_isPlay = false;
    }

    /// <summary>
    ///Out�g�����W�V����
    /// </summary>
    IEnumerator OutTransition()
    {
        //�g�����W�V����������In�����s�����悤�ɂ���B
        m_isTransitionIn = !m_isTransitionIn;

        //�g�����W�V�����Đ����ɂ���B
        m_isPlay = true;

        float t = transitionTime;
        while (t > 0.0f)
        {
            float progress = t / transitionTime;

            // �V�F�[�_�[��_Progress�ɒl��ݒ�
            postEffectMaterial.SetFloat(_progressId, progress);
            yield return null;

            t -= Time.deltaTime;
        }

        postEffectMaterial.SetFloat(_progressId, 1f);

        //�g�����W�V������~���ɂ���B
        m_isPlay = false;
    }
}