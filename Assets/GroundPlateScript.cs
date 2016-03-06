using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundPlateScript : MonoBehaviour {

    RobotController m_owner = null;
    int m_BotLayer;

    List<RobotController> m_triggered = new List<RobotController>();

    [SerializeField]
    Renderer m_ownershipIndicator;

    private float m_timer;

    void Start()
    {
        m_BotLayer = LayerMask.NameToLayer("Robots");
    }

    void Update()
    {

        if(m_owner != null)
        {
            m_timer += Time.deltaTime;
            if(m_timer > GameManager.Instance().groundPlateGainRate)
            {
                m_owner.GetComponent<RobotGestionPoint>().addPoint((int)GameManager.Instance().groundPlatePointGain);
                m_timer = 0;
            }
        }
    }

    void OnOwnershipChanged(Color _color)
    {
        m_ownershipIndicator.material.color = _color;
    }

	void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.layer == m_BotLayer)
        {
            RobotController controller = _collider.GetComponent<RobotController>();
            m_triggered.Add(controller);

            if(m_owner == null || !m_triggered.Contains(m_owner))
            {
                m_owner = controller;
                OnOwnershipChanged(controller.Color);
            }
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        if (_collider.gameObject.layer == m_BotLayer)
        {
            RobotController controller = _collider.GetComponent<RobotController>();
            m_triggered.Remove(controller);
        }
    }
}
