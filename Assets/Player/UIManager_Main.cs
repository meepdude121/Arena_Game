using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager_Main : MonoBehaviour
{
    private float SmoothDampRefValue;
    private float EnergyPercent;

    [SerializeField] private Slider EnergyDisplayBar;
    [SerializeField] private TextMeshProUGUI EnergyDisplay;

    [SerializeField] private float EnergyDisplay_SmoothTime = 0.1f;

    public bool AcceptInput = true;

    public void OnEnergyChange(float Energy, float EnergyLimit)
    {
        EnergyDisplay.text = $"{Mathf.Clamp(Mathf.Round(Energy/EnergyLimit * 100f), 0f, 100f)}%";
        EnergyPercent = Energy / EnergyLimit;
    }

    public void OnAmmoTypeChange()
    {
        
    }

    void Update()
    {
        if (!EnergyDisplayBar.value.IsWithinBoundsOf(EnergyPercent, 0.005f))
        {
            EnergyDisplayBar.value = Mathf.SmoothDamp(EnergyDisplayBar.value, EnergyPercent, ref SmoothDampRefValue, EnergyDisplay_SmoothTime);
        }
    }
}
