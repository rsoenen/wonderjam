using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushingRodScript : MonoBehaviour {

    private List<Rigidbody> m_rigidbodies = new List<Rigidbody>();

    [SerializeField]
    private float m_force = 10;
    private int m_layerBot = 0;    

    void Start()
    {
        m_layerBot = LayerMask.NameToLayer("Robots");
    }

    void FixedUpdate()
    {
        foreach(var rigidbody in m_rigidbodies)
        {
            var forceDirection = rigidbody.transform.position - transform.position;
            forceDirection.y = 0;
            rigidbody.AddForce(forceDirection.normalized * m_force);
            rigidbody.GetComponent<RobotController>().SetLastHit(GetComponent<RobotController>());
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.layer == m_layerBot)
        {
            Rigidbody rigidbody = _collider.attachedRigidbody;
            if(rigidbody != null) m_rigidbodies.Add(rigidbody);
        }
    }

    void OnTriggerExit(Collider _collider)
    {
        if (_collider.gameObject.layer == m_layerBot)
        {
            Rigidbody rigidbody = _collider.attachedRigidbody;
            if(rigidbody != null) m_rigidbodies.Remove(rigidbody);
        }
    }
}
