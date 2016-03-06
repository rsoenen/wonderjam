/*
	This script is placed in public domain. The author takes no responsibility for any possible harm.
	Contributed by Jonathan Czeck
*/
using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour
{
	public Transform emitter;
	public int zigs = 200;
	public float speed = 1f;
	public float amplitude = 1f;
    public float energy = 1f;
	public Light endLight;
    public float maxDistance = 20.0f;

	Perlin noise;
	float oneOverZigs;
	
	private Particle[] particles;

    public void Init(Transform emitter, RobotController owner, float maxDistance)
    {
        this.emitter = emitter;
        this.owner = owner;
        this.maxDistance = maxDistance;
        endLight.color = InputManager.GetColorFromPlayer(owner.playerId);
    }

    public Vector3 initPos { get { return emitter.position; } }
    public Vector3 destPos { get { return transform.position; } }
    public RobotController owner;
	
	void Start()
	{
        amplitude = GameManager.Instance().laserShakeFactor;
        oneOverZigs = 1f / (float)zigs;
		GetComponent<ParticleEmitter>().emit = false;

		GetComponent<ParticleEmitter>().Emit(zigs);
		particles = GetComponent<ParticleEmitter>().particles;

        endLight = GetComponentInChildren<Light>();
	}

    bool connected = false;

    void Update ()
	{
    float distance = Vector3.Distance(transform.position, emitter.position);
     
    if (distance < maxDistance)
    {
            if(!connected)
            {
                owner.OnBoltConnected();
                connected = true;
            }

      GetComponent<MeshRenderer>().enabled = true;
      GetComponent<ParticleRenderer>().enabled = true;
      endLight.enabled = true;

      if (noise == null)
        noise = new Perlin();

      float timex = Time.time * speed * 0.1365143f;
      float timey = Time.time * speed * 1.21688f;
      float timez = Time.time * speed * 2.5564f;
      float _amplitude = amplitude * (0.5f + 0.5f*distance/maxDistance) ;
      for (int i = 0; i < particles.Length; i++)
      {

        Vector3 position = Vector3.Lerp(emitter.position, transform.position, oneOverZigs * (float)i);
        float pdistance = Vector3.Distance(position, emitter.position);
        
        Vector3 offset = new Vector3(noise.Noise(timex-pdistance /*+ position.x*/, timex-pdistance /*+ position.y*/, timex-pdistance /*+ position.z*/),
                      noise.Noise(timey-pdistance /*+ position.x*/, timey-pdistance /*+ position.y*/, timey-pdistance /*+ position.z*/),
                      noise.Noise(timez-pdistance /*+ position.x*/, timez-pdistance /*+ position.y*/, timez-pdistance/*+ position.z*/));
        position += (offset * _amplitude * pdistance/maxDistance);

        particles[i].position = position;
        particles[i].color = endLight.color;
        particles[i].energy = energy * (1 - distance / maxDistance);
        particles[i].size = 0.2f + .5f * (1 - distance / maxDistance);
      }

      GetComponent<ParticleEmitter>().particles = particles;

      if (GetComponent<ParticleEmitter>().particleCount >= 2)
      {
        endLight.transform.position = particles[GetComponent<ParticleEmitter>().particleCount-1].position;
        endLight.intensity = 0.1f + 0.2f * (1 - distance / maxDistance);
        endLight.bounceIntensity =  (1 - distance / maxDistance);
       
      }
    }
    else
    {
      GetComponent<MeshRenderer>().enabled = false;
      GetComponent<ParticleRenderer>().enabled = false;
      endLight.enabled = false;

            if (connected)
            {
                owner.OnBoltDisconnected();
                connected = false;
            }
        }
	}	
}