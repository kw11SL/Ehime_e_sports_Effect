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
            SceneManager.LoadScene("DemoMatchingScene");
            Debug.Log("To Standby Scene");
        }
    }
}
