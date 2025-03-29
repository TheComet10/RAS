using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

enum MenuState
{
    MAIN,
    SELECT,
    CREDITS
}

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selMenu;
    public GameObject credMenu;
    public GameObject othPan;

    private MenuState state;

    private void Start()
    {
        state = MenuState.MAIN;

        mainMenu.SetActive(true);
        selMenu.SetActive(false);
        credMenu.SetActive(false);
        othPan.SetActive(true);

        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == MenuState.SELECT)
            {
                Back();
            }
            else if (state == MenuState.CREDITS)
            {
                Back();
            }
            else if(state == MenuState.MAIN)
            {
                Application.Quit();
            }
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

    public void Credits()
    {
        mainMenu.SetActive(false);
        selMenu.SetActive(false);
        credMenu.SetActive(true);
        othPan.SetActive(false);
        state = MenuState.CREDITS;
    }

    public void Select()
    {
        mainMenu.SetActive(false);
        selMenu.SetActive(true);
        othPan.SetActive(true);
        state = MenuState.SELECT;
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        selMenu.SetActive(false);
        credMenu.SetActive(false);
        othPan.SetActive(true);
        state = MenuState.MAIN;
    }
}
