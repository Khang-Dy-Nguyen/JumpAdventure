using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject heartPanel;
    public GameObject settingsButton;
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public Driver playerScript;
    public GameObject player;
    public GameObject invaders;
    public Invaders invadersScript;

    private bool isPaused = false;

    void Start()
    {
        // Bắt đầu game ở trạng thái dừng, hiển thị Main Menu
        Time.timeScale = 0f;
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        settingsButton.SetActive(false);
        heartPanel.SetActive(false);
        gameOverMenu.SetActive(false);

        player.SetActive(false);
        invaders.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);
        settingsButton.SetActive(!isPaused);
        heartPanel.SetActive(!isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ContinueGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        heartPanel.SetActive(true);
        settingsButton.SetActive(true);
        Time.timeScale = 1f;

        // bắt đầu animation bay lên
        player.SetActive(true); // bật máy bay
        invaders.SetActive(true);

        if (AudioManager.Instance !=null)
        {
            AudioManager.Instance.PlayMusic("BackgroundAudio");
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        heartPanel.SetActive(false);
        settingsButton.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}