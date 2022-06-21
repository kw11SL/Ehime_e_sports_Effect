using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[����ʂ̌��݂̏��ʂ̕\�����X�V����N���X
/// </summary>
public class NowRankingChange : MonoBehaviour
{
    //���݂̏��ʃ��x��
    [SerializeField] Text m_nowRankingLabel = null;
    //���ʂ̃����N���x��
    [SerializeField] Text m_rankLabel = null;
    //���݂̏���
    [SerializeField] int m_ranking = 1;
    //�����N(fir"st",seco"nd",thi"rd",for"th")
    string[] m_rankStr = { "st","nd","rd","th"};
    //���ʂ��Ƃ̐F
    Vector4[] m_rankColor =
    {
        new Vector4(1.0f,1.0f,0.0f,1.0f),           //���F
        new Vector4(0.745f,0.745f,0.745f,1.0f),     //��F
        new Vector4(0.588f,0.274f,0.196f,1.0f),     //���F
        new Vector4(0.0f,0.0f,0.0f,1.0f)            //���F
    };

    //�A�b�v�f�[�g�֐�
    void Update()
    {
        //���݂̏��ʂ̒l��F�Ȃǂ̃f�[�^���X�V
        RankingDataUpdate();
    }

    //���݂̏��ʂ̒l��F�Ȃǂ̃f�[�^���X�V������֐�
    void RankingDataUpdate()
    {
        //���݂̏��ʂɂ���Đ��l��ω�
        m_nowRankingLabel.text = m_ranking + "";
        m_rankLabel.text = m_rankStr[m_ranking - 1];
        //���݂̏��ʂɂ���ĐF��ω�
        m_nowRankingLabel.color = m_rankColor[m_ranking - 1];
        m_rankLabel.color = m_rankColor[m_ranking - 1];
    }
}
