using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�f���V�[���̃I�����C�����I�t���C�����ŕ��򂷂�V�[��
public class GoMatching : MonoBehaviour
{
    GameObject operation = null;

    //�X�^�[�g�֐�
    void Start()
    {
        operation = GameObject.Find("OperationManager");
    }

    //�X�V�֐�
    void Update()
    {
        //�������Ȃ��
        if (operation.GetComponent<OperationOld>().GetIsLongTouch())
        {
            //�}�b�`���O�V�[����
            SceneManager.LoadScene("DemoMatchingScene");
        }

        //�E�t���b�N�Ȃ��
        if (operation.GetComponent<OperationOld>().GetDirection() == "right")
        {
            //�C���Q�[���֒��s�i�V���O���v���C�j
            SceneManager.LoadScene("DemoInGame");
            //�V���O���v���C���[�h�ɐݒ肷��
            GameObject pm = GameObject.Find("ParamManager");
            pm.GetComponent<ParamManage>().SetOfflineMode();
        }
    }
}
