using UnityEngine;

// Class to allow for a collision box to be fired on a mouse down
public class CollisionFire : MonoBehaviour
{
    /// <summary>
    /// Collider to turn on / off on mouse click
    /// </summary>
    [SerializeField] BoxCollider m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_collider.enabled = false;
    }

    private void Update()
    {
        // enable collider if mouse is down
        m_collider.enabled = Input.GetMouseButtonDown(0);
    }
}
