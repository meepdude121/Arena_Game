using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TimeManager Instance;

    private void Awake() {
        Instance = this;
    }
}
