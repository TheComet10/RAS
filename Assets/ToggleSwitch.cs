using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    //https://youtu.be/E9AWlbPGi_4?feature=shared
    [Header("Slider Setup")]
    [SerializeField, Range(0, 1f)] private float sliderValue;
    public bool CurVal { get; private set; }

    private Slider _slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationSpeed = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine _animateSliderCoroutine;

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    private ToggleSwitchGroupManager _toggleSwitchGroupManager;
}
