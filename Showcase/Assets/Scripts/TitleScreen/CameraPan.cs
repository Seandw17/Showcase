using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using static FadeIn;
using System.Linq;

// Author: Alec Lauder

/// <summary>
/// Struct to hold a camera pan rotation / position
/// </summary>
public struct s_cameraPan
{
    public Vector3 position;
    public Vector3 rotation;
}

/// <summary>
/// Class to manage camera panning on title screen
/// </summary>
public class CameraPan : MonoBehaviour
{
    /// <summary>
    /// How fast the camera should move
    /// </summary>
    [SerializeField] float m_cameraMoveSpeed = 0.05f;

    /// <summary>
    /// the cover up sprite for this level
    /// </summary>
    [SerializeField] Image m_coverupSprite;

    int m_activeModel;
    int m_currentGoalpos = 1;

    GameObject[] m_models;

    //List<s_cameraPan>[] m_targetPositions;
    Dictionary<string, List<s_cameraPan>> m_targetPositions;
    List<s_cameraPan> m_currentTargetPositions;

    // Start is called before the first frame update
    void Start()
    {
        //Load all models and set all but first index 
        m_models = Resources.LoadAll("Models/Title", typeof(GameObject))
            .Cast<GameObject>().ToArray();
        foreach(GameObject model in m_models)
        {
            Instantiate(model);
            model.SetActive(false);
        }
        m_models[0].SetActive(true);

        // getting positions
        m_targetPositions = w_CSVLoader.LoadTitlePositions();
        m_currentTargetPositions = m_targetPositions[m_models[0].name];
        transform.position = m_currentTargetPositions[0].position;
        transform.eulerAngles = m_currentTargetPositions[0].rotation;
        SetAlphaToZero(m_coverupSprite);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position ==
            m_currentTargetPositions[m_currentGoalpos].position)
        {
            Debug.Log("changing target");
            m_currentGoalpos++;
            if (m_currentGoalpos >= m_currentTargetPositions.Count)
            {
                Debug.Log("Changing Model");
                ChangeModel();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                m_currentTargetPositions[m_currentGoalpos].position,
                m_cameraMoveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.Euler(m_currentTargetPositions[m_currentGoalpos]
                .rotation),
                (m_cameraMoveSpeed * 5) * Time.deltaTime);
        }
    }

    /// <summary>
    /// Change the currently active model
    /// </summary>
    void ChangeModel()
    {
        StartCoroutine(FadeAsset(m_coverupSprite, 0.1f, true));

        m_models[m_activeModel].SetActive(false);

        m_activeModel++;

        if (m_activeModel >= m_models.Length) { m_activeModel = 0; }

        m_currentTargetPositions =
            m_targetPositions[m_models[m_activeModel].name];
        transform.position = m_currentTargetPositions[0].position;
        transform.eulerAngles = m_currentTargetPositions[0].rotation;
        m_models[m_activeModel].SetActive(true);
        m_currentGoalpos = 1;

        StartCoroutine(FadeAsset(m_coverupSprite, 0.1f, false));
    }
}
