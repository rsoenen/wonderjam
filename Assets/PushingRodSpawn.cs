using UnityEngine;
using System.Collections;

public class PushingRodSpawn : MonoBehaviour {

    [SerializeField]
    private PushingRodScript m_pushingRod;

    [SerializeField]
    private float m_DestroyETA = 10.0f;
    private float m_destructionTimer = 0;


    private float m_DeployementETA = 0.5f;
    private bool m_deployed = false;
    private float m_deployementSpeed;
    private float m_deployementTimer = 0;

    private Vector3 m_targetPosition;

    private static float ROD_HEIGHT = 1.0f;

    private bool m_undeploy = false;

    void Start()
    {
        m_pushingRod.enabled = false;

        m_targetPosition = transform.position;

        transform.position = m_targetPosition + Vector3.down * ROD_HEIGHT;
        m_deployementSpeed = Vector3.Distance(m_targetPosition, transform.position) / m_DeployementETA;

        GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update()
    {
        if(m_undeploy)
        {
            UnDeploy();
        }
        else
        {
            if (m_deployed)
            {
                m_destructionTimer += Time.deltaTime;
                if (m_destructionTimer > m_DestroyETA)
                {
                    m_targetPosition = m_targetPosition + Vector3.down * ROD_HEIGHT;
                    m_deployementTimer = 0;
                    GetComponentInChildren<ParticleSystem>().Stop();
                    m_undeploy = true;
                }
            }
            else
            {
                Deploy();
            }
        }
    }

    void UnDeploy()
    {
        m_deployementTimer += Time.deltaTime;
        if(m_deployementTimer < m_DeployementETA)
        {
            transform.position += Vector3.down * m_deployementSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Deploy()
    {
        m_deployementTimer += Time.deltaTime;
        if (m_deployementTimer < m_DeployementETA)
        {
            transform.position += Vector3.up * m_deployementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = m_targetPosition;
            GetComponentInChildren<ParticleSystem>().Play();
            m_pushingRod.enabled = true;
            m_deployed = true;
        }
    }
}
