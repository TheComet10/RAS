using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public enum UIChange
{
    Simu,
    Menu,
    Set
}

public class SetManager : MonoBehaviour
{
    [SerializeField] int FPS = 30;
    [SerializeField] TMP_Dropdown fpsDropDown;

    public GameObject SideBar;
    public GameObject SetMenu;
    public GameObject PauseMenu;
    public Transform SimuCam;
    public Transform MenuCam;
    public GameObject Camera;

    public StandMove sM;

    private UIChange _uC;
    internal UIChange uC
    {
        get => _uC;
        set
        {
            _uC = value;
            switch (_uC)
            {
                case UIChange.Simu:
                    SetMenu.SetActive(false);
                    PauseMenu.SetActive(false);
                    SideBar.SetActive(true);
                    Camera.transform.position = SimuCam.position;
                    Camera.transform.rotation = SimuCam.rotation;
                    break;
                case UIChange.Set:
                    SetMenu.SetActive(true);
                    SideBar.SetActive(false);
                    PauseMenu.SetActive(false);
                    Camera.transform.position = MenuCam.position;
                    Camera.transform.rotation = MenuCam.rotation;
                    break;
                case UIChange.Menu:
                    SetMenu.SetActive(false);
                    PauseMenu.SetActive(true);
                    SideBar.SetActive(false);
                    Camera.transform.position = MenuCam.position;
                    Camera.transform.rotation = MenuCam.rotation;
                    break;
            }
        }
    }

    bool _isGamePaused = false;
    int prevFPSDD, unsavedFPS;

    private void Start()
    {
        uC = UIChange.Simu;
        Application.targetFrameRate = FPS;
        prevFPSDD = fpsDropDown.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isGamePaused)
            {
                _isGamePaused = true;
                uC = UIChange.Menu;
                Time.timeScale = 0f;
            }
            else
            {
                _isGamePaused = false;
                Time.timeScale = 1f;
                CancelOptions();
                uC = UIChange.Simu;
            }
        }
    }

    public void GetDropdownFPSValue()
    {
        switch(fpsDropDown.value)
        {
            case 0:
                unsavedFPS = 15;
                break;
            case 1:
                unsavedFPS = 30;
                break;
            case 2:
                unsavedFPS = 60;
                break;
            case 3:
                unsavedFPS = 90;
                break;
            case 4:
                unsavedFPS = 120;
                break;
            case 5:
                unsavedFPS = 240;
                break;
        }
    }

    public void SaveOptions()
    {
        FPS = unsavedFPS;
        Application.targetFrameRate = FPS;
        prevFPSDD = fpsDropDown.value;
        uC = UIChange.Menu;
    }

    public void CancelOptions()
    {
        unsavedFPS = FPS;
        fpsDropDown.SetValueWithoutNotify(prevFPSDD);
        uC = UIChange.Menu;
    }

    public void Resume()
    {
        uC = UIChange.Simu;
        Time.timeScale = 1;
    }

    public void Settings()
    {
        uC = UIChange.Set;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
