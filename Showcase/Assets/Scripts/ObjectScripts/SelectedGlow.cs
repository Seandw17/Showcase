using UnityEngine;

public class SelectedGlow : MonoBehaviour
{
    GameObject m_glow;

    // Start is called before the first frame update
    void Start()
    {
        // instaniate gameobject and set to false
        m_glow = Instantiate(Resources.Load<GameObject>("Prefabs/Halo"));
        m_glow.transform.parent = gameObject.transform;
        m_glow.transform.localPosition = Vector3.zero;
        m_glow.SetActive(false);
    }

    /// <summary>
    /// Turn on / off the glow on the object
    /// </summary>
    /// <param name="_on"> is the glow on or off </param>
    public void Glow(bool _on) { m_glow.SetActive(_on); }   
}
