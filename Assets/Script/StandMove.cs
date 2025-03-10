using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StandMove : MonoBehaviour
{
    [SerializeField] Transform baseUp;
    [SerializeField] Transform b1;
    [SerializeField] Transform b2;
    [SerializeField] Transform r1;
    [SerializeField] int FPS = 30;

    private bool bW, bC, b1W, b1C, b2W, b2C, r1W, r1C;

    public float vel1 = 1f;
    public float vel2 = 1f;
    public float vel3 = 1f;
    public float vel4 = 1f;

    private int defStep, clock;                                  //clock is step per second
    private bool dir = false, en = false, ms1, ms2;

    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    void Update()
    {
        //base
        if((Input.GetKey(KeyCode.Q)) || (bW == true))
            baseUp.Rotate(0f, 0f, vel1 * 45f *  Time.deltaTime); //rotate 45 degrees per second

        if ((Input.GetKey(KeyCode.A)) || (bC == true))
            baseUp.Rotate(0f, 0f, vel1 * -45f * Time.deltaTime);

        //b1 (205/-19)
        if (((Input.GetKey(KeyCode.W)) || b1W == true) && ((b1.localRotation.eulerAngles.y <= 205f) || (b1.localRotation.eulerAngles.y > 339f)))
            b1.Rotate(0f, vel2 * 45f * Time.deltaTime, 0f);

        if (((Input.GetKey(KeyCode.S)) || b1C == true) && ((b1.localRotation.eulerAngles.y < 206f) || (b1.localRotation.eulerAngles.y >= 340f)))
            b1.Rotate(0f, vel2 * -45f * Time.deltaTime, 0f);
        //b2
        if ((Input.GetKey(KeyCode.E)) || b2W == true)
            b2.Rotate(0f, vel3 * 45f * Time.deltaTime, 0f);

        if ((Input.GetKey(KeyCode.D)) || b2C == true)
            b2.Rotate(0f, vel3 * -45f * Time.deltaTime, 0f);
        //r1
        if ((Input.GetKey(KeyCode.R)) || r1W == true)
            r1.Rotate(vel4 * 45f * Time.deltaTime, 0f, 0f);

        if ((Input.GetKey(KeyCode.F)) || r1C == true)
            r1.Rotate(vel4 * -45f * Time.deltaTime, 0f, 0f);


        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    float curStepToDeg(int step, bool ms1, bool ms2)
    {
        float curStep;

        if(ms1 == true)
        {
            if(ms2 == true)
            {
                curStep = step * 16;
            }
            else
            {
                curStep = step * 2;
            }
        }
        else
        {
            if (ms2 == true)
            {
                curStep = step * 4;
            }
            else
            {
                curStep = step * 8;
            }
        }

        return (360/curStep); //returns degrees per steps
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
    public void R1CwD()
    {
        r1W = true;
    }
    public void R1CwU()
    {
        r1W = false;
    }

    public void R1CcD()
    {
        r1C = true;
    }
    public void R1CcU()
    {
        r1C = false;
    }

    public void V1A()
    {
        if (vel1 < 2)
            vel1 = vel1 + 0.1f;
    }
    public void V1S()
    {
        if (vel1 > 0)
            vel1 = vel1 - 0.1f;
    }
    public void V2A()
    {
        if (vel2 < 2)
            vel2 = vel2 + 0.1f;
    }
    public void V2S()
    {
        if (vel2 > 0)
            vel2 = vel2 - 0.1f;
    }
    public void V3A()
    {
        if (vel3 < 2)
            vel3 = vel3 + 0.1f;
    }
    public void V3S()
    {
        if (vel3 > 0)
            vel3 = vel3 - 0.1f;
    }
    public void V4A()
    {
        if (vel4 < 2)
            vel4 = vel4 + 0.1f;
    }
    public void V4S()
    {
        if (vel4 > 0)
            vel4 = vel4 - 0.1f;
    }
}
