using UnityEngine;
using System;

/// <summary>
/// Incomplete class to manage interviewer face changes
/// </summary>
public class InterviewerFace : MonoBehaviour
{
    MeshRenderer m_mesh;

    /// <summary>
    /// Faces of the interviewer
    /// </summary>
    [SerializeField] Material m_inquistiveFace;
    [SerializeField] Material m_neutralFace;
    [SerializeField] Material m_smilingFace;
    [SerializeField] Material m_unsureFace;

    // Start is called before the first frame update
    void Awake()
    {
        m_mesh = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Function to change the expression
    /// </summary>
    /// <param name="_rating">the rating of the answer just given</param>
    public void Expression(e_rating _rating)
    {
        switch ((int) _rating)
        {
            case 4:
            case 3:
                m_mesh.material = m_smilingFace;
                Debug.Log("SMILING REACTION");
                break;
            case 2:
                m_mesh.material = m_inquistiveFace;
                Debug.Log("INQUISITIVE REACTION");
                break;
            case 1:
                m_mesh.material = m_smilingFace;
                Debug.Log("NEUTRAL REACTION");
                break;
            case 0:
                m_mesh.material = m_unsureFace;
                Debug.Log("UNSURE REACTION");
                break;
            default:
                throw new Exception("invalid enum passed");
        }
    }
}
