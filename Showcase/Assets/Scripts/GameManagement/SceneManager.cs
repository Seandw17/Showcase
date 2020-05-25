using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Sets the current active scene to the 
    /// </summary>
    /// <param name="scene"> Scene we are changing to </param>
    /// <param name="mode"> the change most we are using</param>
    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Debug.Assert(_mode == LoadSceneMode.Additive,
            "All scene changes should be done using LoadSceneMode.Additive" +
            " failing to do so will delete the _preload scene");
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(_scene);
    }
}
