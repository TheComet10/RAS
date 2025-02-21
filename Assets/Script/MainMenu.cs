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

    private void Update()
    {

        if ((mainMenu.activeSelf == true) && (Input.GetKeyDown(KeyCode.Escape)) && (selMenu.activeSelf == false))
        {
            Application.Quit();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            
            selMenu.SetActive(false);
        }
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
