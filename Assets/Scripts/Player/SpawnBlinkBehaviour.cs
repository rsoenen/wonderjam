using UnityEngine;
using System.Collections;

public class SpawnBlinkBehaviour : MonoBehaviour {

    [SerializeField]
    private float m_beepDuration;

    [SerializeField]
    private float m_beepFrequency;

    private bool m_beeping = false;

    private float m_destructionTimer = 0;
    private float m_beepTimer = 0;

    private Renderer[] m_renderers;

    public void Init(float beepFrequency, float beepDuration)
    {
    }

    // Use this for initialization
    void Start()
    {
        m_beepFrequency = GameManager.Instance().spawnBlinkFreq;
        m_beepDuration = GameManager.Instance().spawnBlinkDuration;
    }

    void OnDestroy()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>()) renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_destructionTimer += Time.deltaTime;
        m_beepTimer += Time.deltaTime;

        if (!m_beeping)
        {
            if (m_beepTimer > 1.0f / m_beepFrequency)
            {
                m_beepTimer = 0;
                foreach (var renderer in GetComponentsInChildren<Renderer>()) renderer.enabled = false;
                m_beeping = true;
            }
        }
        else
        {
            if (m_beepTimer > m_beepDuration)
            {
                m_beepTimer = 0;
                foreach (var renderer in GetComponentsInChildren<Renderer>()) renderer.enabled = true;
                m_beeping = false;
            }
        }
    }
}
