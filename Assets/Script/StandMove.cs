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
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    void Update()
    {
        //base
        if((Input.GetKey(KeyCode.Q)) || (bW == true))
            baseUp.Rotate(0f, 0f, vel1 * 45f *  Time.deltaTime);

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
