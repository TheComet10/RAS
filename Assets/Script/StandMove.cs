using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.IO.Ports;

public class StandMove : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM4", 115200); 

    [SerializeField] Transform baseUp;
    [SerializeField] Transform b1;
    [SerializeField] Transform b2;
    [SerializeField] Transform r1;

    private bool bW, bC, b1W, b1C, b2W, b2C, r1W, r1C;

    public float vel1 = 1f;
    public float vel2 = 1f;
    public float vel3 = 1f;
    public float vel4 = 1f;

    public int defStep;
    public float[] clock = { 200f, 200f, 200f, 200f };                                  //clock is step per second
    public bool[] dir = { false, false, false, false }, en = new bool[4]; 
    public int[] ms1 = {0,0,0,0}, ms2 = {0,0,0,0};

    private void Start()
    {
        defStep = 200;

        serial.Open();
        serial.ReadTimeout = 100;
    }

    void Update()
    {
        //base
        if (((Input.GetKey(KeyCode.Q)) || (bW)) && (!en[0]))
        {
            baseUp.Rotate(0f, 0f, vel1 * CurStepToDeg(defStep, ms1[0], ms2[0]) * clock[0] * Direction(dir[0]) * Time.deltaTime);
            serial.Write("?M0002000008!");
        }

        if (((Input.GetKey(KeyCode.A)) || bC) && (!en[0]))
        {
            baseUp.Rotate(0f, 0f, vel1 * CurStepToDeg(defStep, ms1[0], ms2[0]) * clock[0] * Direction(!dir[0]) * Time.deltaTime);
            serial.Write("?M1002000008!");
        }

        //b1
        if (((Input.GetKey(KeyCode.W)) || b1W) && (!en[1]))
            b1.Rotate(0f, vel2 * CurStepToDeg(defStep, ms1[1], ms2[1]) * clock[1] * Direction(dir[1]) * Time.deltaTime, 0f);

        if (((Input.GetKey(KeyCode.S)) || b1C) && (!en[1]))
            b1.Rotate(0f, vel2 * CurStepToDeg(defStep, ms1[1], ms2[1]) * clock[1] * Direction(!dir[1]) * Time.deltaTime, 0f);
        //b2
        if (((Input.GetKey(KeyCode.E)) || b2W) && (!en[2]))
            b2.Rotate(0f, vel3 * CurStepToDeg(defStep, ms1[2], ms2[2]) * clock[2] * Direction(dir[2]) * Time.deltaTime, 0f);

        if (((Input.GetKey(KeyCode.D)) || b2C) && (!en[2]))
            b2.Rotate(0f, vel3 * CurStepToDeg(defStep, ms1[2], ms2[2]) * clock[2] * Direction(!dir[2]) * Time.deltaTime, 0f);
        //r1
        if (((Input.GetKey(KeyCode.R)) || r1W) && (!en[3]))
            r1.Rotate(vel4 * CurStepToDeg(defStep, ms1[3], ms2[3]) * clock[3] * Direction(dir[3]) * Time.deltaTime, 0f, 0f);

        if (((Input.GetKey(KeyCode.F)) || r1C) && (!en[3]))
            r1.Rotate(vel4 * CurStepToDeg(defStep, ms1[3], ms2[3]) * clock[3] * Direction(!dir[3]) * Time.deltaTime, 0f, 0f);
    }

    private void OnApplicationQuit()
    {
        serial.Close();
    }

    float CurStepToDeg(int step, int ms1, int ms2)
    {
        float curStep;

        int[,] ms = { { 8, 4 }, { 2, 16 } };

        curStep = step * ms[ms1, ms2];

        return (360/curStep); //returns degrees per steps
    }

    float Direction(bool dir)
    {
        if (dir)
            return 1f;
        else
            return -1f;
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
            vel1 += 0.1f;
    }
    public void V1S()
    {
        if (vel1 > 0)
            vel1 -= 0.1f;
    }
    public void V2A()
    {
        if (vel2 < 2)
            vel2 += 0.1f;
    }
    public void V2S()
    {
        if (vel2 > 0)
            vel2 -= 0.1f;
    }
    public void V3A()
    {
        if (vel3 < 2)
            vel3 += 0.1f;
    }
    public void V3S()
    {
        if (vel3 > 0)
            vel3 -= 0.1f;
    }
    public void V4A()
    {
        if (vel4 < 2)
            vel4 += 0.1f;
    }
    public void V4S()
    {
        if (vel4 > 0)
            vel4 -= 0.1f;
    }
}
