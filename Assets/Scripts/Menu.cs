using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private GameObject mainPage;

    private void Start()
    {
        // Procura automaticamente o canvas chamado "MainPage" na hierarquia
        mainPage = GameObject.Find("Menu");


    }

    public void OnPlayButton()
    {
        if (mainPage != null)
        {
           SceneManager.LoadScene("MainScene");
        }

    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
