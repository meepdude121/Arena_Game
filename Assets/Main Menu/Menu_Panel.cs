using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Animator))]
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
        // Every Menu_Panel must contain an Animator with the valid logic.
        animator = GetComponent<Animator>();
        animator.SetBool("Active", StartActive);
        animator.SetBool("StartActive", StartActive);

        // If this Menu_Panel is set to LoadingPanel, find these components, otherwise don't bother
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
            // start initializing the world
            if (!InitializingWorld && StartInitializingWorld)
			{
                // starts loading the scene asyncronously.
                loadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                InitializingWorld = true;
            }
            // set progress bar and percentage text.
            if (InitializingWorld)
			{
                LoadingSlider.value = loadOperation.progress;
                LoadingText.text = Mathf.RoundToInt(loadOperation.progress * 100f).ToString();

                // when world is loaded, unload the main menu.
                if (loadOperation.isDone)
                {
                    Debug.Log("Unloading menu scene");
                    SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.None);
                }
            }
		}
    }
    /// <summary>
    /// Toggles the panel's visibility.
    /// </summary>
    public void ToggleMenu() => animator.SetBool("Active", !animator.GetBool("Active"));

    /// <summary>
    /// Starts loading the lobby scene.
    /// Will only do anything if the panel is set to the loading panel to prevent loading the scene multiple times.
    /// </summary>
    public void StartGame() => StartInitializingWorld = true;
}
