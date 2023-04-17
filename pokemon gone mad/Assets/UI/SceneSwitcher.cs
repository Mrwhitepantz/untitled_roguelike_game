using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string name;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        name = sceneName;
    }
}
