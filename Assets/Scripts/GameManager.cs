using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1f;
    }

    public void GameWon()
    {
        SceneManager.LoadScene("GameWon");
        Time.timeScale = 1f;
    }

}
