using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager_Main : MonoBehaviour
{
    public static UIManager_Main Instance;
    private float EnergySmoothDampRefValue;
    private float EnergyPercent = 1f;

    [Header("Energy Attributes")]
    [SerializeField] private Slider EnergyDisplayBar;
    [SerializeField] private TextMeshProUGUI EnergyDisplay;
    [SerializeField] private float EnergyDisplay_SmoothTime = 0.1f;
    private float XPSmoothDampRefValue;
    private float XPPercent = 0f;

    [Header("XP Attributes")]
    [SerializeField] private Slider XPDisplayBar;
    [SerializeField] private float XPDisplay_SmoothTime = 0.05f;

    [HideInInspector]
    public bool AcceptInput = true;

    public void OnEnergyChange(float Energy, float EnergyLimit)
    {
        EnergyDisplay.text = $"{Mathf.Clamp(Mathf.Round(Energy / EnergyLimit * 100f), 0f, 100f)}%";
        EnergyPercent = Energy / EnergyLimit;
    }
    public void OnXPChange(float Percent)
    {
        XPPercent = Percent;
    }
    private void Awake() => Instance = this;
    
    // TODO: this sucks
    void Update()
    {
        if (!EnergyDisplayBar.value.IsWithinBoundsOf(EnergyPercent, 0.005f))
        {
            EnergyDisplayBar.value = Mathf.SmoothDamp(EnergyDisplayBar.value, EnergyPercent, ref EnergySmoothDampRefValue, EnergyDisplay_SmoothTime);
            EnergyDisplay.text = $"{Mathf.Round(EnergyDisplayBar.value * 100f)}%";
            if (EnergyDisplayBar.value < 0.5f)
			{
                EnergyDisplay.color = Color.yellow;
                if (EnergyDisplayBar.value < 0.33f)
                {
                    EnergyDisplay.color = Color.red;
                }
            } else
			{
                EnergyDisplay.color = Color.white;
            }

        }
        if (!XPDisplayBar.value.IsWithinBoundsOf(XPPercent, 0.005f))
        {
            XPDisplayBar.value = Mathf.SmoothDamp(XPDisplayBar.value, XPPercent, ref XPSmoothDampRefValue, XPDisplay_SmoothTime);
        }
    }
}
