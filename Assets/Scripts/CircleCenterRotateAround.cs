using UnityEngine;

/// <summary>
/// Transform.RotateAround��p�����~�^��
/// </summary>
public class CircleCenterRotateAround : MonoBehaviour
{
    // ���S�_
    [SerializeField]GameObject m_center = null;

    // ��]��(Y��)
    Vector3 m_axis = Vector3.up;

    // �~�^������
    public float period = 0.0f;

    //����邩�ǂ���
    bool aroundMoveOn = false;

    //���鎞�ԃJ�E���^�[
    int aroundCount = 0;

    //���v��肩�����v��肩
    int m_reverse = 1;

    //����V�X�e��
    OperationNew m_operation = null;

    void Start()
    {
        //����V�X�e���̃Q�[���I�u�W�F�N�g���������X�N���v�g���g�p����
        m_operation = GameObject.Find("OperationSystem").GetComponent<OperationNew>();
    }

    void Update()
    {
        if (!aroundMoveOn)
        {
            //��ʂ��E�t���b�N���ꂽ��A
            if (m_operation.GetNowOperation() == "right")
            {
                //������Ԃɂ���
                aroundMoveOn = true;
                //���v���
                m_reverse = 1;
            }
            //��ʂ����t���b�N���ꂽ��A
            if (m_operation.GetNowOperation() == "left")
            {
                //������Ԃɂ���
                aroundMoveOn = true;
                //���v���
                m_reverse = -1;
            }
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
            m_center.transform.position,                     //���S�_
            m_axis,                                          //��
            360 / period * Time.deltaTime * m_reverse      //����
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