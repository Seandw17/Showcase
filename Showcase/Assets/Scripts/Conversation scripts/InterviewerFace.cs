using UnityEngine;
using System;

public class InterviewerFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        throw new NotImplementedException("The interviewer does not yet exist");
    }

    public static void Expression(e_rating _rating)
    {
        // TODO actually change the expressions
        switch ((int) _rating)
        {
            case 4:
            case 3:
                Debug.Log("SMILING REACTION");
                break;
            case 2:
                Debug.Log("INQUISITIVE REACTION");
                break;
            case 1:
                Debug.Log("NEUTRAL REACTION");
                break;
            case 0:
                Debug.Log("UNSURE REACTION");
                break;
            default:
                throw new Exception("invalid enum passed");
        }
    }
}
