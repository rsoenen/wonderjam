using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowingBehavior : MonoBehaviour {

    [SerializeField]
    private Transform m_anchor;
    private GameObject m_grabbedObject;

    private AudioSource m_AudioSource;

    [SerializeField]
    private AudioClip m_ThrowSound;

    [SerializeField]
    private float m_speed = 5.0f;

    public bool CurrentlyFull
    {
        get
        {
            return m_grabbedObject != null;
        }
    }

    private bool m_shouldCarrySomething = false;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_grabbedObject == null && m_shouldCarrySomething)
            FailSafe();

        if (m_grabbedObject == null)
            return;
        
        if(Vector3.Distance(m_anchor.position, m_grabbedObject.transform.position) >= 0.1f)
        {
            m_grabbedObject.transform.position += Mathf.Min(Vector3.Distance(m_anchor.position, m_grabbedObject.transform.position), m_speed * Time.deltaTime) * (m_anchor.position - m_grabbedObject.transform.position).normalized;
        }
    }

    private void FailSafe()
    {
        GetComponent<RobotController>().Encumbered = false;
    }

    public void GrabObject(GameObject _obj)
    {
        UnGrabObject();

        m_grabbedObject = _obj;
        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        m_grabbedObject.transform.parent = m_anchor;
        m_grabbedObject.GetComponentInChildren<ThrowableObject>().Grabbed = true;
        m_shouldCarrySomething = true;

        GetComponent<RobotController>().Encumbered = true;

    }

    public void UnGrabObject()
    {
        if (m_grabbedObject == null)
            return;

        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_grabbedObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        m_grabbedObject.transform.parent = null;
        m_grabbedObject.GetComponentInChildren<ThrowableObject>().Grabbed = false;
        m_shouldCarrySomething = false;

        GetComponent<RobotController>().Encumbered = false;

        m_grabbedObject = null;
    }

    public void ThrowObject(Vector3 _direction)
    {
        if (m_grabbedObject == null)
            return;

        m_grabbedObject.transform.parent = null;
        m_grabbedObject.GetComponentInChildren<ThrowableObject>().Ignore(gameObject);
        m_grabbedObject.GetComponentInChildren<ThrowableObject>().Grabbed = false;

        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_grabbedObject.GetComponent<Rigidbody>().AddForce(_direction.normalized * 20.0f + Vector3.up * 2.0f, ForceMode.Impulse);

        m_AudioSource.clip = m_ThrowSound;
        m_AudioSource.Play();

        m_shouldCarrySomething = false;

        GetComponent<RobotController>().Encumbered = false;

        m_grabbedObject = null;
    }

	
}
