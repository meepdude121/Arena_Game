using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Menu_Panel : MonoBehaviour
{
    [SerializeField] private bool StartActive;
    [SerializeField] private bool LoadingMenu;
    private bool InitializingWorld;
    private bool StartInitializingWorld;
    AsyncOperation loadOperation;
    Animator animator;
    Slider LoadingSlider;
    TextMeshProUGUI LoadingText;
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Active", StartActive);
        if (LoadingMenu)
		{
            LoadingSlider = GameObject.Find("Loading Bar").GetComponent<Slider>();
            LoadingText = GameObject.Find("Loading Text").GetComponent<TextMeshProUGUI>();
		}
    }
    void Update()
    {
        if (LoadingMenu)
		{
            if (!InitializingWorld && StartInitializingWorld)
			{
                InitializingWorld = true;
                loadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
			}
            if (InitializingWorld)
			{
                LoadingSlider.value = loadOperation.progress;
                LoadingText.text = Mathf.RoundToInt(loadOperation.progress * 100f).ToString();
			}
            if (loadOperation.isDone)
			{
                Debug.Log("Unloading menu scene");
                SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.None);
			}
		}
    }
    public void ToggleMenu() => animator.SetBool("Active", !animator.GetBool("Active"));
    public void StartGame() => StartInitializingWorld = true;
}
