using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ThrowableObject : MonoBehaviour
{
    private bool m_grabbed = false;
    public bool Grabbed { get { return m_grabbed; } set { if (m_grabbed != value) { m_grabbed = value; GrabbedChanged(); } } }

    [SerializeField]
    private float m_HurtingSpeed = 10.0f;

    [SerializeField]
    Rigidbody m_rigidbody;
    int m_RobotLayer;

    GameObject m_ignored;

    public void Ignore(GameObject _ignore)
    {
        m_ignored = _ignore;
    }

    void GrabbedChanged()
    {
        TimerAndDeletion tad = GetComponentInParent<TimerAndDeletion>();
        if(tad != null)
        {
            if (Grabbed)
                tad.Enable(false);
            else
                tad.Enable(true);
        }

        BeepAndDestroy bad = GetComponentInParent<BeepAndDestroy>();
        if(bad != null)
        {
            if (Grabbed)
            {
                Destroy(bad);
                var toto = transform.parent.gameObject.AddComponent<TimerAndDeletion>();
                toto.Init(8);
                toto.Enable(false);
            }
        }
    }

    void Start()
    {
        m_RobotLayer = LayerMask.NameToLayer("Robots");
        Grabbed = false;
    }

    void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject != m_ignored && _collider.gameObject.layer == m_RobotLayer && m_rigidbody.velocity.magnitude > m_HurtingSpeed)
        {
            Vector3 direction = m_rigidbody.velocity.normalized;
            direction.y = 0;
            _collider.gameObject.AddComponent<StuntBehaviour>().Init(-direction.normalized);
            Destroy(gameObject);
        }
    }

    
}
