using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwapper : MonoBehaviour
{
    public void SwapScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
