using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class CanonController : MonoBehaviour {

  public Transform firePoint;
  public Transform projectilePrefab;

  public Rigidbody vehicle;

  public List<AudioClip> fireSounds;

  public float projectileForce = 50.0f;
  public float fireDelay = 0.3f;

  private float lastFireTime = 0.0f;

	// Use this for initialization
	void Start () {
    Assert.IsNotNull(firePoint);
    Assert.IsNotNull(projectilePrefab);
    Assert.IsNotNull(vehicle);
	}

  public void fire()
  {
    if (lastFireTime > fireDelay)
    {
      Transform p = (Transform)Object.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
      Rigidbody b = p.GetComponent<Rigidbody>();
      b.velocity = vehicle.velocity;
      b.AddRelativeForce(new Vector3(0.0f, 0.0f, projectileForce), ForceMode.Impulse);
      lastFireTime = 0.0f;
      GetComponent<AudioSource>().PlayOneShot(fireSounds[Random.Range(0, fireSounds.Count-1)]);
    }
  }

  void Update()
  {
    lastFireTime += Time.deltaTime;
  }

}
