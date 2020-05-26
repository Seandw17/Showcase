using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        SceneManager.SetActiveScene(_scene);
        Debug.Log("Changing to scene: " + _scene.name);
    }
}
