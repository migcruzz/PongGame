using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPage;
    public GameObject optionsPage;

    [Header("Texts (opcional se quiser mostrar valores)")]
    public Text titleText;
    public Text gameModeText;
    public Text racketSpeedText;
    public Text ballSpeedText;

    [Header("UI Components")]
    public Toggle multiplayerToggle;
    public Slider ballSpeedSlider;
    public Slider racketSlider;
    public Button confirmButton;
    public Button cancelButton;

    // ✅ Valores padrão
    private const float DefaultBallSpeed = 5f;
    private const float DefaultRacketSpeed = 4f;
    private const bool DefaultIsMultiplayer = false;

    private void Start()
    {
        mainPage.SetActive(true);
        optionsPage.SetActive(false);

        // Verifica se o GameConfig ainda não foi configurado e aplica os valores padrão
        if (GameConfig.Instance != null)
        {
            if (!GameConfig.Instance.HasBeenConfigured)
            {
                GameConfig.Instance.SetBallSpeed(DefaultBallSpeed);
                GameConfig.Instance.SetRacketSpeed(DefaultRacketSpeed);
                GameConfig.Instance.SetIsMultiplayer(DefaultIsMultiplayer);
            }

            // Preenche os campos visuais com os dados salvos (ou padrão se for a primeira vez)
            multiplayerToggle.isOn = GameConfig.Instance.GetIsMultiplayer();
            ballSpeedSlider.value = GameConfig.Instance.GetBallSpeed();
            racketSlider.value = GameConfig.Instance.GetRacketSpeed();
        }

        // Eventos
        ballSpeedSlider.onValueChanged.AddListener(OnBallSpeedSliderChanged);
        racketSlider.onValueChanged.AddListener(OnRacketSliderChanged);

        UpdateSliderTexts();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnOptionsButton()
    {
        optionsPage.SetActive(true);
        mainPage.SetActive(false);
    }

    public void OnCancelButton()
    {
        mainPage.SetActive(true);
        optionsPage.SetActive(false);
    }

    public void OnConfirmButton()
    {
        if (GameConfig.Instance != null)
        {
            GameConfig.Instance.SetIsMultiplayer(multiplayerToggle.isOn);
            GameConfig.Instance.SetBallSpeed(ballSpeedSlider.value);
            GameConfig.Instance.SetRacketSpeed(racketSlider.value);
            GameConfig.Instance.HasBeenConfigured = true;
        }

        optionsPage.SetActive(false);
        mainPage.SetActive(true);
    }

    private void OnBallSpeedSliderChanged(float value)
    {
        if (ballSpeedText != null)
            ballSpeedText.text = $"Ball Speed: {value:F1}";
    }

    private void OnRacketSliderChanged(float value)
    {
        if (racketSpeedText != null)
            racketSpeedText.text = $"Racket Speed: {value:F1}";
    }

    private void UpdateSliderTexts()
    {
        OnBallSpeedSliderChanged(ballSpeedSlider.value);
        OnRacketSliderChanged(racketSlider.value);
    }
}
