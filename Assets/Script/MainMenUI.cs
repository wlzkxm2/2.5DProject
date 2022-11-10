using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenUI : MonoBehaviour
{
    public void StartBtn_Clicked()
    {
        SceneManager.LoadScene("Stage00");
    }

    public void OptionBtn_Clicked()
    {
        Debug.Log("옵션 버튼 눌림");
    }

    public void ExitBtn_clicked()
    {
        Debug.Log("Exit 버튼 눌림");
    }
}
