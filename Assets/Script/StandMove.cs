using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandMove : MonoBehaviour
{
    [SerializeField] Transform baseUp;
    [SerializeField] Transform b1;
    [SerializeField] Transform b2;
    [SerializeField] int FPS = 30;

    private bool bW, bC, b1W, b1C, b2W, b2C;

    public float vel1 = 1f;
    public float vel2 = 1f;
    public float vel3 = 1f;
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    void Update()
    {
        //base
        if((Input.GetKey(KeyCode.Q)) || (bW == true))
            baseUp.Rotate(0f, 0f, vel1 * 45f *  Time.deltaTime);

        if ((Input.GetKey(KeyCode.W)) || (bC == true))
            baseUp.Rotate(0f, 0f, vel1 * -45f * Time.deltaTime);

        //b1 (205/-19)
        if (((Input.GetKey(KeyCode.E)) || b1W == true) && ((b1.localRotation.eulerAngles.y <= 205f) || (b1.localRotation.eulerAngles.y > 339f)))
            b1.Rotate(0f, vel2 * 45f * Time.deltaTime, 0f);

        if (((Input.GetKey(KeyCode.R)) || b1C == true) && ((b1.localRotation.eulerAngles.y < 206f) || (b1.localRotation.eulerAngles.y >= 340f)))
            b1.Rotate(0f, vel2 * -45f * Time.deltaTime, 0f);
        //b2
        if ((Input.GetKey(KeyCode.T)) || b2W == true)
            b2.Rotate(0f, vel3 * 45f * Time.deltaTime, 0f);

        if ((Input.GetKey(KeyCode.Y)) || b2C == true)
            b2.Rotate(0f, vel3 * -45f * Time.deltaTime, 0f);

        if(Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void BaseCwD()
    {
        bW = true;
    }
    public void BaseCwU()
    {
        bW = false;
    }

    public void BaseCcD()
    {
        bC = true;
    }
    public void BaseCcU()
    {
        bC = false;
    }

    public void B1CwD()
    {
        b1W = true;
    }
    public void B1CwU()
    {
        b1W = false;
    }
    public void B1CcD()
    {
        b1C = true;
    }
    public void B1CcU()
    {
        b1C = false;
    }

    public void B2CwD()
    {
        b2W = true;
    }
    public void B2CwU()
    {
        b2W = false;
    }

    public void B2CcD()
    {
        b2C = true;
    }
    public void B2CcU()
    {
        b2C = false;
    }

    public void SliderChange1(float value1)
    {
        vel1 = value1;
    }
    public void SliderChange2(float value2)
    {
        vel2 = value2;
    }
    public void SliderChange3(float value3)
    {
        vel3 = value3;
    }
}
