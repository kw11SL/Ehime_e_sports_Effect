using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (operation.GetComponent<OperationOld>().GetIsLongTouch())
        {
            //�}�b�`���O�V�[����
            SceneManager.LoadScene("DemoMatchingScene");
            Debug.Log("To Standby Scene");
        }

        if (operation.GetComponent<OperationOld>().GetDirection() == "right")
        {
            //�C���Q�[���֒��s�i�V���O���v���C�j
            SceneManager.LoadScene("DemoInGame");
            GameObject pm = GameObject.Find("ParamManager");
            pm.GetComponent<ParamManage>().SetOfflineMode();
            Debug.Log("To Standby Scene");
        }
    }
}
