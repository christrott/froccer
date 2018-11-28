using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    public AudioSource audioSource;

    public void OpenScene(string sceneName)
    {
        audioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void Restart()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
