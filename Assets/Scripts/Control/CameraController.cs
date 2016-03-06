using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

	public List<Transform> pois;
	public float tweak = 0.60f;
	private float limit = 3.5f;
	public float minY = 3f;
	[Range(0.11f, 1.0f)] public float damping = 1.0f;

	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pmin = pois[0].position;
		Vector3 pmax = pois[0].position;

		foreach (Transform t in pois)
		{
			Vector3 p = t.position;
			if (pmin.x > p.x) pmin.x = p.x;
			if (pmin.y > p.y) pmin.y = p.y;
			if (pmin.z > p.z) pmin.z = p.z;

			if (pmax.x < p.x) pmax.x = p.x;
			if (pmax.y < p.y) pmax.y = p.y;
			if (pmax.z < p.z) pmax.z = p.z;
		}


		pmax += new Vector3(limit,0,limit);
		pmin -= new Vector3(limit,0,limit);


		Vector3 ppcam = (pmax-pmin)*0.5f + pmin;

		float x = pmax.x - pmin.x;
		float z = pmax.z - pmin.z;

		float r = x;
		if (z > x) 
		{
			r = z;
		}

		float y = r*tweak + pmax.y;
		if (y < minY)
		{
			y = minY;
		}

		targetPosition = new Vector3(ppcam.x, y, ppcam.z - y*Mathf.Sin(Mathf.Deg2Rad*(90-transform.rotation.eulerAngles.x)));
		transform.position = Vector3.Lerp(transform.position,targetPosition,damping * Time.deltaTime * 60);

	}
}
