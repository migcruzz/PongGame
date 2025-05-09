using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField] private GameObject pauseMenuUI;

    private InputAction pauseAction;
    private bool isPaused = false;


    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void GoToHome()
    {
        Time.timeScale = 1f; // Garante que o jogo não fique pausado após a troca
        SceneManager.LoadScene("Menu");
    }
}
