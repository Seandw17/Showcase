using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class QuestionUIDIsplay: EditorWindow
{
    public void Display(Dictionary<e_rating, string> _val)
    {
        for (int index = 0; index < _val.Count; index++)
        {
            _val[(e_rating)index] =
            EditorGUILayout.TextField((e_rating)index + ":",
            _val[(e_rating)index]);
        }
    }
}
