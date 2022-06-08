using UnityEngine;

/// <summary>
/// Transform.RotateAround��p�����~�^��
/// </summary>
public class CircleCenterRotateAround : MonoBehaviour
{
    // ���S�_
    [SerializeField]GameObject center;

    // ��]��(Y��)
    private Vector3 axis = Vector3.up;

    // �~�^������
    public float period;

    //����邩�ǂ���
    bool aroundMoveOn = false;

    //���鎞�ԃJ�E���^�[
    int aroundCount = 0;

    void Update()
    {
        //��ʂ��^�b�v���ꂽ��A
        if (Input.GetButtonDown("Fire1")&&!aroundMoveOn)
        {
            //������Ԃɂ���
            aroundMoveOn = true;
        }

        //���鏈�������s
        GoAround();
    }

    //���鏈���֐�
    void GoAround()
    {
        //���Ȃ���Ԃ̂Ƃ��͏��������Ȃ��B
        if (!aroundMoveOn) return;

        // ���S�_center�̎�����A��axis�ŁAperiod�����ŉ~�^��
        transform.RotateAround(
            center.transform.position,          //���S�_
            axis,                               //��
            360 / period * Time.deltaTime       //����
        );

        //�J�E���g�v��
        aroundCount++;
        //�J�E���g���w�肵�����l���傫���Ȃ�����A
        if (aroundCount > 100)
        {
            //����Ȃ���Ԃɖ߂�
            aroundMoveOn = false;
            //�J�E���g�̏�����
            aroundCount = 0;
        }
    }
}