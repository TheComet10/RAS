using UnityEngine;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;

    public void SliderChange(float value)
    {
        sliderText.text = value.ToString("x0.0");
    }
}
