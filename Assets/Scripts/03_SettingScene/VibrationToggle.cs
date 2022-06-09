using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �g�O��
/// </summary>
public class VibrationToggle : MonoBehaviour
{
    [SerializeField]Image m_backgroundImage;
    [SerializeField]RectTransform m_handle;
    [SerializeField]bool m_onAwake;

    /// <summary>
    /// �g�O���̒l
    /// </summary>
    [NonSerialized] public bool m_value;

    float m_handlePosX;
    Sequence m_sequence;

    static readonly Color m_OFF_BG_COLOR = new Color(0.92f, 0.92f, 0.92f);
    static readonly Color m_ON_BG_COLOR = new Color(0.2f, 0.84f, 0.3f);

    const float m_kSwitchDuration = 0.36f;

    //�X�^�[�g�֐�
    void Start()
    {
        m_handlePosX = Mathf.Abs(m_handle.anchoredPosition.x);
        m_value = m_onAwake;
        UpdateToggle(0);
    }

    /// <summary>
    /// �g�O���̃{�^���A�N�V�����ɐݒ肵�Ă���
    /// </summary>
    public void SwitchToggle()
    {
        m_value = !m_value;
        UpdateToggle(m_kSwitchDuration);
    }

    /// <summary>
    /// ��Ԃ𔽉f������
    /// </summary>
    void UpdateToggle(float duration)
    {
        var bgColor = m_value ? m_ON_BG_COLOR : m_OFF_BG_COLOR;
        var handleDestX = m_value ? m_handlePosX : -m_handlePosX;

        m_sequence?.Complete();
        m_sequence = DOTween.Sequence();
        m_sequence.Append(m_backgroundImage.DOColor(bgColor, duration))
            .Join(m_handle.DOAnchorPosX(handleDestX, duration / 2));
    }
}