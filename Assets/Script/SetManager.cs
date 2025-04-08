using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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

    public TMP_InputField Clock1, Clock2, Clock3, Clock4;

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
                    _isGamePaused = false;
                    SetMenu.SetActive(false);
                    PauseMenu.SetActive(false);
                    SideBar.SetActive(true);
                    Camera.transform.position = SimuCam.position;
                    Camera.transform.rotation = SimuCam.rotation;
                    break;
                case UIChange.Set:
                    _isGamePaused = true;
                    SetMenu.SetActive(true);
                    SideBar.SetActive(false);
                    PauseMenu.SetActive(false);
                    Camera.transform.position = MenuCam.position;
                    Camera.transform.rotation = MenuCam.rotation;
                    break;
                case UIChange.Menu:
                    _isGamePaused = true;
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
    int prevFPSDD, unsavedFPS, tck1 = 200, tck2 = 200, tck3 = 200, tck4 = 200;

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
                uC = UIChange.Menu;
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                if (uC == UIChange.Set)
                { 
                    CancelOptions();
                    uC = UIChange.Menu;
                }
                else if (uC == UIChange.Menu)
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
        //fps
        FPS = unsavedFPS;
        Application.targetFrameRate = FPS;
        prevFPSDD = fpsDropDown.value;

        //ck
        sM.clock[0] = tck1;
        sM.clock[1] = tck2;
        sM.clock[2] = tck3;
        sM.clock[3] = tck4;

        //back to menu
        uC = UIChange.Menu;
    }

    public void CancelOptions()
    {
        //FPS
        unsavedFPS = FPS;
        fpsDropDown.SetValueWithoutNotify(prevFPSDD);

        //ck
        tck1 = (int)sM.clock[0];
        Clock1.text = tck1.ToString();
        tck2 = (int)sM.clock[1];
        //Clock2.text = tck2.ToString();
        tck3 = (int)sM.clock[2];
        //Clock3.text = tck3.ToString();
        tck4 = (int)sM.clock[3];
        //Clock4.text = tck4.ToString();

        //back to menu
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

    public void Ck1()
    {
        string input = Clock1.text;

        if (int.TryParse(input, out int num))
        {
            tck1 = num;
        }
    }
    public void Ck2()
    {
        string input = Clock2.text;

        if (int.TryParse(input, out int num))
        {
            tck2 = num;
        }
    }
    public void Ck3()
    {
        string input = Clock3.text;

        if (int.TryParse(input, out int num))
        {
            tck3 = num;
        }
    }
    public void Ck4()
    {
        string input = Clock4.text;

        if (int.TryParse(input, out int num))
        {
            tck4 = num;
        }
    }
}
