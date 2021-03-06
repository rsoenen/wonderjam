﻿using UnityEngine;
using System.Collections;

public class RodPlacementBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject m_RodPrefab;

    [SerializeField]
    private float m_cooldown = 1;
    private bool m_onCooldown = false;

    [SerializeField]
    private float m_despawn = 5;

    private float m_cooldown_timer = 0;
    private float m_despawn_timer = 0;

	public void Activate(Vector3 _position, Vector3 _direction)
    {
        if (m_onCooldown)
            return;

        GameObject rod = Instantiate<GameObject>(m_RodPrefab);

        rod.transform.position = _position;
        rod.transform.forward = _direction;

        m_onCooldown = true;
        m_cooldown_timer = 0;
    }

    void Update()
    {
        if(m_onCooldown)
        {
            m_cooldown_timer += Time.deltaTime;
            if(m_cooldown_timer > m_cooldown)
            {
                m_onCooldown = false;
            }
        }
    }
}
