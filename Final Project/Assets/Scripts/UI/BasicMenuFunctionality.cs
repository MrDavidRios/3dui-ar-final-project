using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMenuFunctionality : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx + 1);
    }
}
