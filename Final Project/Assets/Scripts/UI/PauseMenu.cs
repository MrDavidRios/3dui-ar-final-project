using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool gamePaused = false;

    [SerializeField] private GameObject pauseMenu;

    private void OnEnable()
    {
        PauseFunctionality.GamePaused += OnPause;
        PauseFunctionality.GameResumed += OnResume;
    }

    private void OnDisable()
    {
        PauseFunctionality.GamePaused -= OnPause;
        PauseFunctionality.GameResumed -= OnResume;
    }

    private void Update()
    {
        OVRInput.Update();

        // On menu button press, toggle pause state
        if (!OVRInput.Get(OVRInput.Button.Start)) return;

        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            PauseFunctionality.GameResumed?.Invoke();
        }
        else
        {
            pauseMenu.SetActive(true);
            PauseFunctionality.GamePaused?.Invoke();
        }
    }

    private void OnPause()
    {
        gamePaused = true;
    }

    private void OnResume()
    {
        gamePaused = false;
    }
}
