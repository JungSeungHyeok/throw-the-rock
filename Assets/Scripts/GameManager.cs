using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver }
    public GameState currentState = GameState.Playing;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) // �̱���
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // ���� ����Ǿ �ı�x
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // �����
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentState = GameState.Playing;
    }

    public void EndGame()
    {
        currentState = GameState.GameOver;
        // ���� ȭ�� ǥ�� ���� ���߿�
    }
}
