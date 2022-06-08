using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTap : MonoBehaviour
{
    //��Ԃ��O�i�ł��邩�ǂ���
    bool canAdvance = false;
    //��Ԃ̈ړ����x
    [SerializeField] float advanceSpeed;
    //��Ԃ̈ړ��͈�
    [SerializeField] float trainMoveRange;

    void Update()
    {
        //��ʂ��^�b�v���ꂽ���𔻒�
        TapScreen();

        //��Ԃ��O�i�ł���Ƃ�
        if (canAdvance)
        {
            //��Ԃ�O�i������
            this.transform.position += new Vector3(0.0f,0.0f,advanceSpeed);
        }

        //�J�����Ɉ��̋����܂ŋ߂Â�����A
        if (this.transform.position.z < trainMoveRange)
        {
            //���[�h�I���V�[���ɑJ��
            SceneManager.LoadScene("02_ModeSelectScene");

        }
    }

    //��ʃ^�b�v����֐�
    void TapScreen()
    {
        //��ʃ^�b�v���P�x�����ł��Ȃ��悤�ɂ���
        if (canAdvance)
        {
            return;
        }

        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1"))
        {
            //��Ԃ��O�i�ł��锻��ɂ���
            canAdvance = true;
        }
    }
}
