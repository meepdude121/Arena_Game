using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager_Main : MonoBehaviour
{
    private float SmoothDampRefValue;
    private float EnergyPercent;

    private Slider EnergyDisplayBar;
    private TextMeshProUGUI EnergyDisplay;

    [SerializeField] private float EnergyDisplay_SmoothTime = 0.05f;

    private void OnEnergyChange(float Energy, float EnergyLimit)
    {
        EnergyDisplay.text = $"{Mathf.Clamp(Mathf.Round(Energy/EnergyLimit) * 100f, 0f, 100f)}%";
        EnergyPercent = Energy / EnergyLimit;
    }

    void Update()
    {
        if (EnergyDisplayBar.value.IsWithinBoundsOf(EnergyPercent, 0.05f))
        {
            EnergyDisplayBar.value = Mathf.SmoothDamp(EnergyDisplayBar.value, EnergyPercent, ref SmoothDampRefValue, EnergyDisplay_SmoothTime);
        }
    }
}
