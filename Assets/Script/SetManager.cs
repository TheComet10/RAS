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

    //toggles
    public Slider Dir1; public Slider Dir2; public Slider Dir3; public Slider Dir4;
    public Slider En1; public Slider En2; public Slider En3; public Slider En4;
    public Slider Ms11; public Slider Ms12; public Slider Ms13; public Slider Ms14;
    public Slider Ms21; public Slider Ms22; public Slider Ms23; public Slider Ms24;

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

    //motors
    private bool dir1, dir2, dir3, dir4, en1, en2, en3, en4;
    private int ms11, ms12, ms13, ms14, ms21, ms22, ms23, ms24;

    private void Start()
    {
        uC = UIChange.Simu;
        Application.targetFrameRate = FPS;
        prevFPSDD = fpsDropDown.value;


        dir1 = sM.dir[0]; dir2 = sM.dir[1]; dir3 = sM.dir[2]; dir4 = sM.dir[3];
        Dir1.value = dir1 ? 1 : 0; Dir2.value = dir2 ? 1 : 0; Dir3.value = dir3 ? 1 : 0; Dir4.value = dir4 ? 1 : 0;
        en1 = sM.en[0]; en2 = sM.en[1]; en3 = sM.en[2]; en4 = sM.en[3];
        En1.value = en1 ? 1 : 0; En2.value = en2 ? 1 : 0; En3.value = en3 ? 1 : 0; En4.value = en4 ? 1 : 0;
        ms11 = sM.ms1[0]; ms12 = sM.ms1[1]; ms13 = sM.ms1[2]; ms14 = sM.ms1[3];
        Ms11.value = ms11; Ms12.value = ms12; Ms13.value = ms13; Ms14.value = ms14;
        ms21 = sM.ms2[0]; ms22 = sM.ms2[1]; ms23 = sM.ms2[2]; ms24 = sM.ms2[3];
        Ms21.value = ms21; Ms22.value = ms22; Ms23.value = ms23; Ms24.value = ms24;
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

        //motors
        sM.dir[0] = dir1; sM.dir[1] = dir2; sM.dir[2] = dir3; sM.dir[3] = dir4;
        sM.en[0] = en1; sM.en[1] = en2; sM.en[2] = en3; sM.en[3] = en4;
        sM.ms1[0] = ms11; sM.ms1[1] = ms12; sM.ms1[2] = ms13; sM.ms1[3] = ms14;
        sM.ms2[0] = ms21; sM.ms2[1] = ms22; sM.ms2[2] = ms23; sM.ms2[3] = ms24;
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
        Clock2.text = tck2.ToString();
        tck3 = (int)sM.clock[2];
        Clock3.text = tck3.ToString();
        tck4 = (int)sM.clock[3];
        Clock4.text = tck4.ToString();

        //motors
        dir1 = sM.dir[0]; dir2 = sM.dir[1]; dir3 = sM.dir[2]; dir4 = sM.dir[3];
        Dir1.value = dir1 ? 1 : 0; Dir2.value = dir2 ? 1 : 0; Dir3.value = dir3 ? 1 : 0; Dir4.value = dir4 ? 1 : 0;
        en1 = sM.en[0]; en2 = sM.en[1]; en3 = sM.en[2]; en4 = sM.en[3];
        En1.value = en1 ? 1 : 0; En2.value = en2 ? 1 : 0; En3.value = en3 ? 1 : 0; En4.value = en4 ? 1 : 0;
        ms11 = sM.ms1[0]; ms12 = sM.ms1[1]; ms13 = sM.ms1[2]; ms14 = sM.ms1[3];
        Ms11.value = ms11; Ms12.value = ms12; Ms13.value = ms13; Ms14.value = ms14;
        ms21 = sM.ms2[0]; ms22 = sM.ms2[1]; ms23 = sM.ms2[2]; ms24 = sM.ms2[3];
        Ms21.value = ms21; Ms22.value = ms22; Ms23.value = ms23; Ms24.value = ms24;

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

    public void DirOn1()
    {
        dir1 = true;
    }
    public void DirOff1()
    {
        dir1 = false;
    }
    public void DirOn2()
    {
        dir2 = true;
    }
    public void DirOff2()
    {
        dir2 = false;
    }
    public void DirOn3()
    {
        dir3 = true;
    }
    public void DirOff3()
    {
        dir3 = false;
    }
    public void DirOn4()
    {
        dir4 = true;
    }
    public void DirOff4()
    {
        dir4 = false;
    }
    public void EnOn1()
    {
        en1 = true;
    }
    public void EnOff1()
    {
        en1 = false;
    }
    public void EnOn2()
    {
        en2 = true;
    }
    public void EnOff2()
    {
        en2 = false;
    }
    public void EnOn3()
    {
        en3 = true;
    }
    public void EnOff3()
    {
        en3 = false;
    }
    public void EnOn4()
    {
        en4 = true;
    }
    public void EnOff4()
    {
        en4 = false;
    }
    public void Ms1On1()
    {
        ms11 = 1;
    }
    public void Ms1Off1()
    {
        ms11 = 0;
    }
    public void Ms1On2()
    {
        ms12 = 1;
    }
    public void Ms1Off2()
    {
        ms12 = 0;
    }
    public void Ms1On3()
    {
        ms13 = 1;
    }
    public void Ms1Off3()
    {
        ms13 = 0;
    }
    public void Ms1On4()
    {
        ms14 = 1;
    }
    public void Ms1Off4()
    {
        ms14 = 0;
    }
    public void Ms2On1()
    {
        ms21 = 1;
    }
    public void Ms2Off1()
    {
        ms21 = 0;
    }
    public void Ms2On2()
    {
        ms22 = 1;
    }
    public void Ms2Off2()
    {
        ms22 = 0;
    }
    public void Ms2On3()
    {
        ms23 = 1;
    }
    public void Ms2Off3()
    {
        ms23 = 0;
    }
    public void Ms2On4()
    {
        ms24 = 1;
    }
    public void Ms2Off4()
    {
        ms24 = 0;
    }
}
