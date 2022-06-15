using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�g�p���m�F�̃e�X�g�I�u�W�F�N�g�p
public class ObjectRotation : MonoBehaviour
{
    Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void Rotate(string dir)
    {
        //�ǂ���Ƀt���b�N���ꂽ���ŃI�u�W�F�N�g�����̕����ɉ�
        switch (dir)
        {
            case "right":
                rb.angularVelocity = new Vector3(0.0f, -1.0f, 0.0f);
                break;
            case "left":
                rb.angularVelocity = new Vector3(0.0f, 1.0f, 0.0f);
                break;
            //�^�b�`�Ŏ~�߂�
            case "touch":
                rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            default:
                break;
        }
    }
}