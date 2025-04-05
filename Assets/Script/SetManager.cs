using UnityEngine;

public enum UIChange
{
    Simu,
    Menu,
    Set
}

public class SetManager : MonoBehaviour
{
    [SerializeField] int FPS = 30;

    public GameObject SideBar;
    public GameObject SetMenu;
    public GameObject SetBut;
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
                    SideBar.SetActive(true);
                    SetBut.SetActive(true);
                    Camera.transform.position = SimuCam.position;
                    Camera.transform.rotation = SimuCam.rotation;
                    break;
                case UIChange.Set:
                    SetMenu.SetActive(true);
                    SideBar.SetActive(false);
                    SetBut.SetActive(false);
                    Camera.transform.position = MenuCam.position;
                    Camera.transform.rotation = MenuCam.rotation;
                    break;
                case UIChange.Menu:
                    SetMenu.SetActive(true);
                    SideBar.SetActive(false);
                    SetBut.SetActive(false);
                    Camera.transform.position = MenuCam.position;
                    Camera.transform.rotation = MenuCam.rotation;
                    break;
            }
        }
    }

    bool _isGamePaused = false;

    private void Start()
    {
        uC = UIChange.Simu;
        Application.targetFrameRate = FPS;
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
                uC = UIChange.Simu;
                Time.timeScale = 1f;
            }
        }
    }
}
