using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeepAndDestroy : MonoBehaviour {

    [SerializeField]
    private float m_beepDuration;

    [SerializeField]
    private float m_beepFrequency;

    private bool m_beeping = false;

	public bool beep = true;

    [SerializeField]
    private float m_DestructionETA;

    private float m_destructionTimer = 0;
    private float m_beepTimer = 0;

    private Renderer[] m_renderers;

	// Use this for initialization
	void Start () {
        m_renderers = GetComponentsInChildren<Renderer>();
        if (m_renderers.Length == 0)
            Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
        m_destructionTimer += Time.deltaTime;
        m_beepTimer += Time.deltaTime;

        if (m_destructionTimer > m_DestructionETA)
            Destroy(gameObject);

		if (beep)
		{

        if(!m_beeping)
        {
            if(m_beepTimer > 1.0f / m_beepFrequency)
            {
                m_beepTimer = 0;
                foreach(var renderer in m_renderers) renderer.enabled = false;
                m_beeping = true;
            }
        }
        else
        {
            if(m_beepTimer > m_beepDuration)
            {
                m_beepTimer = 0;
                foreach (var renderer in m_renderers) renderer.enabled = true;
                m_beeping = false;
            }
        }
		}
	}
}
