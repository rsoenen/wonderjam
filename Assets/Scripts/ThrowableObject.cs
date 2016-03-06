using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ThrowableObject : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_rigidbody;
    int m_RobotLayer;

    void Start()
    {
        m_RobotLayer = LayerMask.NameToLayer("Robots");
    }

    void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.layer == m_RobotLayer)
        {
            Vector3 direction = (_collider.transform.position - transform.position);
            direction.y = 0;
            _collider.gameObject.AddComponent<StuntBehaviour>().Init(direction.normalized);
            Destroy(gameObject);
        }
    }
}
