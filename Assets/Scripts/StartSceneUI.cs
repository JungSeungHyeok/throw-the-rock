using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage1-1"); // 여기서 "GameScene"은 게임 씬의 이름입니다.
    }

    public void OnExitButtonClicked()
    {
        Application.Quit(); // 게임 종료
    }
}
