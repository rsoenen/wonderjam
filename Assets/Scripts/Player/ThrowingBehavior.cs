using UnityEngine;
using System.Collections;

public class ThrowingBehavior : MonoBehaviour {

    [SerializeField]
    private Transform m_anchor;
    private GameObject m_grabbedObject;

    [SerializeField]
    private float m_speed = 5.0f;

    public bool CurrentlyFull
    {
        get
        {
            return m_grabbedObject != null;
        }
    }

    void Update()
    {
        if (m_grabbedObject == null)
            return;

        if(Vector3.Distance(m_anchor.position, m_grabbedObject.transform.position) >= 0.1f)
        {
            m_grabbedObject.transform.position += Mathf.Min(Vector3.Distance(m_anchor.position, m_grabbedObject.transform.position), m_speed * Time.deltaTime) * (m_anchor.position - m_grabbedObject.transform.position).normalized;
        }
    }

    public void GrabObject(GameObject _obj)
    {
        UnGrabObject();

        m_grabbedObject = _obj;
        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        m_grabbedObject.transform.parent = m_anchor;

        GetComponent<RobotController>().Encumbered = true;

    }

    public void UnGrabObject()
    {
        if (m_grabbedObject == null)
            return;

        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_grabbedObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        m_grabbedObject.transform.parent = null;

        GetComponent<RobotController>().Encumbered = false;

        m_grabbedObject = null;
    }

    public void ThrowObject()
    {
        if (m_grabbedObject == null)
            return;

        m_grabbedObject.transform.parent = null;
        m_grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_grabbedObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10.0f + Vector3.up * 2.0f, ForceMode.Impulse);

        GetComponent<RobotController>().Encumbered = false;

        m_grabbedObject = null;
    }

	
}
