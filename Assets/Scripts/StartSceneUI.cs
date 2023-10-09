using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage1-1"); // ���⼭ "GameScene"�� ���� ���� �̸��Դϴ�.
    }

    public void OnExitButtonClicked()
    {
        Application.Quit(); // ���� ����
    }
}
