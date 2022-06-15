using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ݒ��ʂ̃X���C�_�[�̌��݂̐��l���e�L�X�g�\������N���X
/// </summary>
public class GetSliderValue : MonoBehaviour
{
    [SerializeField]Slider m_slider = null;
    [SerializeField] Text m_nowSliderValueText = null;

    //�X���C�_�[�̒l���ύX���ꂽ����s�����֐�
    public void ChangeValue()
    {
        //�n���h���̏�̐��l���X�V
        m_nowSliderValueText.text = "" + (int)(100 - m_slider.value);
    }
}