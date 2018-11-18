using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
