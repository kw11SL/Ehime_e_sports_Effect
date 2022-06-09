using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �g�O��
/// </summary>
public class BlindToggle : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private RectTransform handle;
    [SerializeField] private bool onAwake;

    /// <summary>
    /// �g�O���̒l
    /// </summary>
    [NonSerialized] public bool Value;

    private float handlePosX;
    private Sequence sequence;

    private static readonly Color OFF_BG_COLOR = new Color(0.92f, 0.92f, 0.92f);
    private static readonly Color ON_BG_COLOR = new Color(0.2f, 0.84f, 0.3f);

    private const float SWITCH_DURATION = 0.36f;

    private void Start()
    {
        handlePosX = Mathf.Abs(handle.anchoredPosition.x);
        Value = onAwake;
        UpdateToggle(0);
    }

    /// <summary>
    /// �g�O���̃{�^���A�N�V�����ɐݒ肵�Ă���
    /// </summary>
    public void SwitchToggle()
    {
        Value = !Value;
        UpdateToggle(SWITCH_DURATION);
    }

    /// <summary>
    /// ��Ԃ𔽉f������
    /// </summary>
    private void UpdateToggle(float duration)
    {
        var bgColor = Value ? ON_BG_COLOR : OFF_BG_COLOR;
        var handleDestX = Value ? handlePosX : -handlePosX;

        sequence?.Complete();
        sequence = DOTween.Sequence();
        sequence.Append(backgroundImage.DOColor(bgColor, duration))
            .Join(handle.DOAnchorPosX(handleDestX, duration / 2));
    }
}