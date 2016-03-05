using UnityEngine;
using System.Collections;

public class DestroyWhenNoParticles : MonoBehaviour {

    [SerializeField]
    private float m_SafeTime = 0.5f;

    private float m_safeTimer;

    private ParticleSystem m_ParticleSystem;

	// Use this for initialization
	void Start () {
        m_ParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        m_safeTimer += Time.deltaTime;
        if(m_safeTimer > m_SafeTime)
        {
            if (m_ParticleSystem.particleCount == 0)
                Destroy(gameObject);
        }
	}
}
