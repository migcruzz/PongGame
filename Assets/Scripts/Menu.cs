using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private GameObject mainPage;

    private void Start()
    {
        // Procura automaticamente o canvas chamado "MainPage" na hierarquia
        mainPage = GameObject.Find("MainPage");


    }

    public void OnPlayButton()
    {
        if (mainPage != null)
        {
            GameConfig.Instance.SetBallSpeed(1f);
            mainPage.SetActive(false); // Oculta o menu
        }

    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
