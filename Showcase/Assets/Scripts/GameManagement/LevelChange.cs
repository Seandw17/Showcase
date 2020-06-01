using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelChange : MonoBehaviour
{
    [SerializeField] string forcedLevelChange;

    static LevelChange instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(!forcedLevelChange.Equals(""),
            "Enter a level to change to");

        ChangeLevel(forcedLevelChange);
    }

    /// <summary>
    /// Change the level to the string name of the scene
    /// </summary>
    /// <param name="_sceneName"> name of the scene </param>
    public static void ChangeLevel(string _sceneName)
    {
        Debug.Log("Changing to scene: " + _sceneName);
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        instance.StartCoroutine(instance.LoadLevel(_sceneName));
    }

    /// <summary>
    /// Couroutine to load a scene
    /// </summary>
    /// <param name="_sceneName">name of scene</param>
    /// <returns>yield command</returns>
    public IEnumerator LoadLevel(string _sceneName)
    {
        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.
            LoadSceneAsync(_sceneName, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Loading");
        Debug.Log("Loading of Scene: " + _sceneName + " is complete");
    }
}
