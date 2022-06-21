using System.Collections;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    [SerializeField] float speed = 0.0f;             //���x
    [SerializeField] float startPosition = 0.0f;     //�J�n�ʒu
    [SerializeField] float endPosition = 0.0f;       //�I���ʒu

    //�X�V�֐�
    void Update()
    {
        //���t���[��x�|�W�V�������������ړ�������
        transform.Translate(speed * Time.deltaTime, 0, 0);

        //�X�N���[�����ڕW�|�C���g�܂œ��B���������`�F�b�N
        if (transform.position.x >= endPosition)
        {
            ScrollEnd();
        }
    }

    void ScrollEnd()
    {
        //�X�N���[�����鋗������߂�
        transform.Translate(-1 * (endPosition - startPosition), 0, 0);

        //�����Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���R���|�[�l���g�Ƀ��b�Z�[�W�𑗂�
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}