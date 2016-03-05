using UnityEngine;
using System.Collections;

public class RodPlacementBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject m_RodPrefab;

    private GameObject m_rod;

    [SerializeField]
    private float m_cooldown = 1;
    private bool m_onCooldown = false;

    private float m_timer = 0;

	public void Activate(Vector3 _position, Vector3 _direction)
    {
        if (m_onCooldown)
            return;

        if(m_rod == null)
            m_rod = Instantiate<GameObject>(m_RodPrefab);

        m_rod.transform.position = _position;
        m_rod.transform.forward = _direction;

        m_onCooldown = true;
        m_timer = 0;
    }

    void Update()
    {
        if(m_onCooldown)
        {
            m_timer += Time.deltaTime;
            if(m_timer > m_cooldown)
            {
                m_onCooldown = false;
            }
        }
    }
}
