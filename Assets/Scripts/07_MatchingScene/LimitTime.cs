using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitTime : MonoBehaviour
{
    //�������ԃ��x��
    [SerializeField] Text limitTimeLabel = null;

    //�������Ԃ̔w�i�摜
    [SerializeField] Image limitTimeBackImage1 = null;
    [SerializeField] Image limitTimeBackImage2 = null;

    //�������Ԓl(�ω������Ă���)
    [SerializeField] float limitTimeValue = 0.0f;
    //�ő吧�����Ԓl
    float maxLimitTimeValue = 0.0f;


    void Start()
    {
        maxLimitTimeValue = limitTimeValue;
    }

    void Update()
    {
        //�������Ԃ�0�ɂȂ�܂Ő������ԃJ�E���g���s
        if (limitTimeValue >= 0)
        {
            limitTimeValue -= Time.deltaTime;
        }

        //�������Ԃ̔w�i�摜�̃Q�[�W�����������Ă���
        limitTimeBackImage2.fillAmount += Time.deltaTime / maxLimitTimeValue;

        //�������Ԃ��I���킸���ɂȂ�����F��ω�������
        if(limitTimeValue<=6)
        {
            limitTimeBackImage1.color = Color.red;
        }

        //�������ԃ��x�����X�V
        limitTimeLabel.text = "" + (int)limitTimeValue;
    }
}
