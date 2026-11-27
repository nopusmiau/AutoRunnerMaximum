using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public GameObject pausePanel;
    public AudioSource bgMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        bgMusic.Pause();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        bgMusic.UnPause();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
