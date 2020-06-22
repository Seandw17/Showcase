using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UIResponseVariations : EditorWindow
{
    static public void Display(List<Questionresponse> _val)
    {
        for (int index = 0; index < _val.Count; index++)
        {
            // reponse
            _val[index].response = EditorGUILayout.TextField("Response: ",
                _val[index].response);
            // rating
            _val[index].rating = (e_rating)EditorGUILayout.
                EnumPopup("Rating: ", _val[index].rating);
            // Unlock Criteria
            _val[index].unlockCriteria =
                (e_unlockFlag)EditorGUILayout.EnumPopup
                ("Unlock Flag: ", _val[index].unlockCriteria);

            if (GUILayout.Button("Delete Response"))
            {
                DeleteResponse(index);
            }
        }
    }

    static void DeleteResponse(int _toDelete)
    {
        // TODO delete response
    }
}
