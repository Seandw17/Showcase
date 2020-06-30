using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelChange : MonoBehaviour
{
    [SerializeField] string forcedLevelChange;

    static LevelChange instance;

    static LoadingManage m_loadingManager;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(!forcedLevelChange.Equals(""),
            "Enter a level to change to");
        //ChangeLevel("TitleScreen");
        ChangeLevel(forcedLevelChange);
        ConversationStore.Init();
    }

    /// <summary>
    /// Change the level to the string name of the scene
    /// </summary>
    /// <param name="_sceneName"> name of the scene </param>
    public static void ChangeLevel(string _sceneName)
    {
        Debug.Log("Changing to scene: " + _sceneName);
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
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
            Debug.Log("Loading scene " + _sceneName + " " + async.progress +
                "%");
            m_loadingManager.SetLoadingPercentText(async.progress);
            yield return null;
        }

        Debug.Assert(m_loadingManager, "No loading manager was set!");
        m_loadingManager.FadeOut();

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
        Debug.Log("Loading of Scene: " + _sceneName + " is complete");

        if (!SceneManager.GetActiveScene().name.Equals("TItleScreen")
            && !SceneManager.GetActiveScene().name.Equals("PreLoad"))
        {
            GameManagerScript.UIActive(true);
        }
        else
        {
            GameManagerScript.UIActive(false);
        }
    }

    /// <summary>
    /// overwrite the loading manager
    /// </summary>
    /// <param name="_new">the new loading manager</param>
    public static void SetLoadingManager(LoadingManage _new) =>
        m_loadingManager = _new;
}
