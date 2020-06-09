using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsPages : Page
{
    ResponseDisplay[] m_responseDisplays;

    private void Awake()
    {
        m_responseDisplays = new ResponseDisplay[3];
        Vector3 newPos = Vector3.zero;
        newPos.z -= 1f;
        newPos.y -= 0.3f;

        for (int index = 0; index < m_responseDisplays.Length; index++)
        {
            ResponseDisplay temp = Instantiate(Resources.Load<GameObject>
                ("Prefabs/ResponseDisplay")).GetComponent<ResponseDisplay>();
            m_responseDisplays[index] = temp;
            m_responseDisplays[index].transform.SetParent(transform);

            if (index > 0)
            {
                newPos.y -= 0.3f;
            }

            m_responseDisplays[index].transform.localPosition = newPos;
        }
    }

    protected override void Init() =>
        throw new System.NotImplementedException();

    public void SetValue(string[] _tips)
    {
        Debug.Log(m_responseDisplays.Length);
        Debug.Assert(_tips.Length == m_responseDisplays.Length,
            "Too many text " +
            "values have been passed to this functions");

        for (int index = 0; index < m_responseDisplays.Length; index++)
        {
            m_responseDisplays[index].SetValue(_tips[index]);
        }
    }
}
