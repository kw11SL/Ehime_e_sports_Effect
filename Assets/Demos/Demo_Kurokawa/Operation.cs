using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation : MonoBehaviour
{
    private Vector3 touchStartPos = Vector3.zero;               //�^�b�`���J�n�����ʒu
    private Vector3 touchEndPos = Vector3.zero;                 //�^�b�`���I�����ʒu�A�������͌��݃^�b�`���Ă���ʒu�i���[�h�ɂ���Ĉ������Ⴄ�j
    private string direction = "";                              //�t���b�N���Ă������
    private float touchTime = 0.0f;                             //���������p�����Ă��鎞��
    private bool isTouching = false;                            //���݃^�b�`���Ă��邩
    private bool isLongTouch = false;                           //���������ǂ���
    private bool isDecideDirWhenLongTouch = false;              //��莞�Ԓ��������Ă��鎞�A���̎��_�ł̕������m�F������
    private GameObject rotateObject = null;                     //��]������Q�[���I�u�W�F�N�g

    public bool isWorkEveryFrame = false;                       //���t���[���^�b�`�̈ړ������𒲂ׂ邩�B�Q�[���V�[���Ő؂�ւ���

    void Start()
    {
        //��]�̑ΏۂƂȂ�Q�[���I�u�W�F�N�g�𖼑O�Ō�������
        rotateObject = GameObject.Find("Cube");
    }

    //�ǂ̕����Ƀt���b�N���������f����֐�
    void DecideDirection()
	{
        //�^�b�`�𗣂������̉�ʏ�̈ʒu���擾
        touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //X,Y�����̈ړ��ʂ��v�Z
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //�E�����Ƀt���b�N
                direction = "right";
            }
            else if (-30 > directionX)
            {
                //�������Ƀt���b�N
                direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (30 < directionY)
            {
                //������Ƀt���b�N
                direction = "up";
            }
            else if (-30 > directionY)
            {
                //�������̃t���b�N
                direction = "down";
            }
        }
        else
        {
            //�^�b�`�����o
            direction = "touch";
        }
    }

    //���������ǂ������擾����Q�b�^�[
    public bool GetIsLongTouch()
	{
        return isLongTouch;
	}

    //�X�V�֐�
    void Update()
    {
        //�^�b�v���ꂽ���i���N���b�N�j
        //�����O...���P...�E...�R...��
        if (Input.GetMouseButtonDown(0))
        {
            //��ʂɃ^�b�`���Ă���
            isTouching = true;
            //�^�b�`���Ă����ʏ�̍��W���擾
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        if (rotateObject != null)
        {


            //���t���[���^�b�`�̈ړ��ʂ��擾���郂�[�h�Ȃ��
            if (isWorkEveryFrame)
            {
                //�^�b�`���Ă���Ƃ��A��ԏ��߂̃^�b�v�ꂩ�猻�݂̃^�b�v�ʒu�Ńt���b�N���������肷��
                if (isTouching)
                {
                    //�O�t���[���̃^�b�`�ʒu�ƌ��݂̃t���[���̃^�b�`�ʒu���ꏏ��������I�u�W�F�N�g����]�����Ȃ�
                    // if (touchEndPos.magnitude != new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z).magnitude)
                    //{
                    //�ŏI�n�_�i���݂̃^�b�v�ʒu�j���X�V���ăt���b�N���������肷��
                    DecideDirection();
                    //��]�Ώۂ̃I�u�W�F�N�g�Ƀt���b�N������^����
                    rotateObject.GetComponent<ObjectRotation>().Rotate(direction);
                    // }
                }
            }
            //�^�b�v�J�n�ʒu����^�b�v�𗣂����Ƃ���܂ł��t���b�N����Ƃ݂Ȃ����[�h�Ȃ��
            else
            {
                //�^�b�v�������ꂽ��
                if (Input.GetMouseButtonUp(0))
                {
                    //�^�b�`�̃t���O�␔�l������������
                    TachDataInit();
                    //�ǂ̕����Ƀt���b�N���������f����
                    DecideDirection();
                    //�I�u�W�F�N�g���t���b�N���������ɉ�]������
                    rotateObject.GetComponent<ObjectRotation>().Rotate(direction);
                }
            }

        }

        //�^�b�v�������ꂽ�Ƃ��A
        if (Input.GetMouseButtonUp(0))
        {
            //�^�b�`�̃t���O�␔�l������������
            TachDataInit();
        }

        if (isTouching)
		{
            //�^�b�`���Ă��鎞�Ԃ��Q�[���^�C���Ōv��
            touchTime += Time.deltaTime;

            //������x������������X���C�h���Ă��Ȃ����m���߂�
            if (touchTime >= 1.2f && !isDecideDirWhenLongTouch)
            {
                //���������̃t���b�N�����m�F����
                isDecideDirWhenLongTouch = true;
                //�ǂ̕����Ƀt���b�N���������f����
                DecideDirection();
                //�^�b�`���Ă�����
                if (direction == "touch")
                {
                    //���������ԓǂݏグ�J�n
                    isLongTouch = true;
                }
            }

            if(isLongTouch)
			{
                //���������̃f�o�b�N�\�L
                //Debug.Log("counting! " + touchTime);
            }
        }
    }

    //�^�b�`�̃t���O�␔�l������������֐�
    void TachDataInit()
    {
        //�^�b�`���Ă��Ȃ�
        isTouching = false;
        //���������Ă��Ȃ�
        isLongTouch = false;
        //��莞�Ԉȏ㒷�������Ă��Ȃ�
        isDecideDirWhenLongTouch = false;
        //�^�b�`���Ă��鎞�Ԃ����Z�b�g
        touchTime = 0.0f;
    }
}