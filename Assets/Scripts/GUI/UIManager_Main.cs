using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager_Main : MonoBehaviour
{
    public static UIManager_Main Instance { get; private set; }

    private float EnergySmoothDampRefValue;
    private float EnergyPercent = 1f;

    [Header("Energy Attributes")]
    [SerializeField] private Slider EnergyDisplayBar;
    [SerializeField] private TextMeshProUGUI EnergyDisplay;
    [SerializeField] private float EnergyDisplay_SmoothTime = 0.1f;


    private float AmmoSmoothDampRefValue;
    private float AmmoPercent = 1f;

    [Header("Ammo Attributes")]
    [SerializeField] private Slider AmmoDisplayBar;
    [SerializeField] private float AmmoDisplay_SmoothTime = 0.05f;

    [HideInInspector]
    public bool AcceptInput = true;

    public void OnEnergyChange(float Energy, float EnergyLimit)
    {
        EnergyDisplay.text = $"{Mathf.Clamp(Mathf.Round(Energy / EnergyLimit * 100f), 0f, 100f)}%";
        EnergyPercent = Energy / EnergyLimit;
    }
    public void OnAmmoChange(float Percent)
    {
        // do i need this? Todo: test
        AmmoPercent = Percent;
    }
    public void OnAmmoTypeChange()
    {

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
        if (!AmmoDisplayBar.value.IsWithinBoundsOf(AmmoPercent, 0.005f))
        {
            AmmoDisplayBar.value = Mathf.SmoothDamp(AmmoDisplayBar.value, AmmoPercent, ref AmmoSmoothDampRefValue, AmmoDisplay_SmoothTime);
        }
    }
}
