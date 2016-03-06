using UnityEngine;
using System.Collections;

public class EnergyLossBehaviour : MonoBehaviour {
    public float duration, deathRatio;
    private float time;

	// Use this for initialization
	void Start () {
        duration = GameManager.Instance().slowDownDuration;
        deathRatio = GameManager.Instance().slowDownDeathRatio;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GetComponentInChildren<LightningBolt>() != null)
            Destroy(this);
        time += Time.fixedDeltaTime;
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(-time/duration * body.velocity / Time.fixedDeltaTime);
        if (time / duration > deathRatio)
        {
            GetComponent<RobotController>().Die();
            Destroy(this);
        }
	}
}
