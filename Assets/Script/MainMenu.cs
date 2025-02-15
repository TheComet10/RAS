using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        selMenu.SetActive(false);
        Application.targetFrameRate = 30;
    }

    public void StartB1()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitSimu()
    {
        Application.Quit();
    }
}
