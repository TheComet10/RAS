using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI velText1 = null;
    [SerializeField] private TextMeshProUGUI velText2 = null;
    [SerializeField] private TextMeshProUGUI velText3 = null;
    [SerializeField] private TextMeshProUGUI velText4 = null;

    public StandMove sM;

    private void Update()
    {
        velText1.text = sM.vel1.ToString("x0.0");
        velText2.text = sM.vel2.ToString("x0.0");
        velText3.text = sM.vel3.ToString("x0.0");
        velText4.text = sM.vel4.ToString("x0.0");
    }
}
