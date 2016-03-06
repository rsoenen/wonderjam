using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoBatteryBehavior : MonoBehaviour {
	private float time;
	private GameManager game;



	// Use this for initialization
	void Start () 
	{        
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<RobotController>().enabled = false;
		time = 0;
		game = GameManager.Instance();
		GetComponent<RobotController>().lightningEnabled = false;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().PlayQueued("NoBattery");
		EnableCollision(false);

	}

	void EnableRenderers(bool _enable)
	{
		foreach (var renderer in GetComponentsInChildren<Renderer>())
			renderer.enabled = _enable;
	}

	void EnableCollision(bool _enable)
	{
		foreach (var col in GetComponentsInChildren<Collider>())
			col.enabled = _enable;
	}

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime;
		//transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 90 * time / game.deathDuration);
		if(time > game.deathDuration)
		{
			Totem closestTotem = game.GetClosestAvailableTotem(transform.position);
			if (closestTotem != null)
			{
				GetComponent<RobotController>().enabled = true;
				GetComponent<RobotController>().lightningEnabled = true;
				gameObject.AddComponent<SpawnBehaviour>().Init(closestTotem);
				transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
				GetComponent<Rigidbody>().isKinematic = false;
				GetComponent<RobotController>().enabled = true;
				EnableRenderers(true);
				EnableCollision(true);
				GetComponent<Animation>().Stop();
				GetComponent<Animation>().PlayQueued("idle");

				Destroy(this);
			}
		}
	}

	
}
