using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(VolumetricLines.VolumetricLineStripBehavior))]
[RequireComponent(typeof(MeshRenderer))]

public class EnergyReceiver : MonoBehaviour {

  public Transform emitter;
  public float maxDistance;
	public float speed = 1f;
	public float amplitude = 1f;
  private VolumetricLines.VolumetricLineStripBehavior rendering;
  private Light light;
	private Perlin noise;


	// Use this for initialization
	void Start () {
	  rendering = GetComponent<VolumetricLines.VolumetricLineStripBehavior>();
    light = GetComponent<Light>();
    noise = new Perlin();

	}
	
	// Update is called once per frame
	void Update () {
    float distance = Vector3.Distance(transform.position, emitter.position);
    Debug.Log("distance: " + distance);
    if (distance < maxDistance)
    {
      GetComponent<MeshRenderer>().enabled = true;
      light.enabled = true;

      if (noise == null)
        noise = new Perlin();

      float timex = Time.time * speed * 0.1365143f;
      float timey = Time.time * speed * 1.21688f;
      float timez = Time.time * speed * 2.5564f;
      float _amplitude = amplitude * (0.5f + 0.5f * distance / maxDistance);

      Vector3 global_dir = emitter.position - transform.position;
      global_dir.Normalize();
      Vector3 local_dir = Quaternion.Inverse(transform.rotation) * global_dir;

      Vector3[] newPos = new Vector3[rendering.LineVertices.Length];
      {
        Vector3 position = emitter.position;
        float loldistance = Vector3.Distance(transform.position, emitter.position);
        position = local_dir * loldistance;
        newPos[0] = position;
        //Debug.Log("Computed position: " + position + " pdistance: " + pdistance + " local_dir: " + local_dir);
      }
      for (int i = 1; i < newPos.Length - 1 && 1==0; i++)
      {

        Vector3 position = Vector3.Lerp(emitter.position, transform.position, (float)i / (float)rendering.LineVertices.Length);
        float pdistance = Vector3.Distance(position, emitter.position);

        Vector3 offset = new Vector3(noise.Noise(timex - pdistance /*+ position.x*/, timex - pdistance /*+ position.y*/, timex - pdistance /*+ position.z*/),
                      noise.Noise(timey - pdistance /*+ position.x*/, timey - pdistance /*+ position.y*/, timey - pdistance /*+ position.z*/),
                      noise.Noise(timez - pdistance /*+ position.x*/, timez - pdistance /*+ position.y*/, timez - pdistance/*+ position.z*/));
        position = local_dir * pdistance + (offset * _amplitude * pdistance / maxDistance);
        newPos[i] = position;

      

        /*  particles[i].position = position;
          particles[i].color = endLight.color;
          particles[i].energy = energy * (1 - distance / maxDistance);
          particles[i].size = 1.0f + 2.0f * (1 - distance / maxDistance);*/
      }
      {
        newPos[newPos.Length-1] = new Vector3(0.0f,0.0f,0.0f);
      }
      rendering.UpdateLineVertices(newPos);


      if (newPos.Length >= 2)
      {
        //light.transform.position = newPos[newPos.Length-1];
        light.intensity = 0.5f + 1.5f * (1 - distance / maxDistance);
        light.bounceIntensity = (1 - distance / maxDistance);

      }
    }
    else
    {
      GetComponent<MeshRenderer>().enabled = false;
      light.enabled = false;
    }

	
	}
}
