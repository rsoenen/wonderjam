using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushingRodScript : MonoBehaviour {

    private List<Rigidbody> m_rigidbodies = new List<Rigidbody>();
    private List<Rigidbody> m_to_add = new List<Rigidbody>();
    private List<Rigidbody> m_to_remove = new List<Rigidbody>();

    [SerializeField]
    private float m_force = 10;

    private int m_layerBot = 0;

    void Start()
    {
        m_layerBot = LayerMask.NameToLayer("Robots");
    }

    void FixedUpdate()
    {
        foreach (var rigidbody in m_to_add)
            m_rigidbodies.Add(rigidbody);
        foreach (var rigidbody in m_to_remove)
            m_rigidbodies.Remove(rigidbody);

        m_to_add.Clear();
        m_to_remove.Clear();

        foreach(var rigidbody in m_rigidbodies)
        {
            var forceDirection = rigidbody.transform.position - transform.position;
            forceDirection.y = 0;
            rigidbody.AddForce(forceDirection.normalized * m_force);
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.layer == m_layerBot)
        {
            Rigidbody rigidbody = _collider.attachedRigidbody;
            if(rigidbody != null) m_to_add.Add(rigidbody);
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        if (_collider.gameObject.layer == m_layerBot)
        {
            Rigidbody rigidbody = _collider.attachedRigidbody;
            if (rigidbody != null) m_to_remove.Add(rigidbody);
        }
    }
}
