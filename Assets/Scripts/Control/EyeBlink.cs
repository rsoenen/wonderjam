using UnityEngine;
using System.Collections;

public class EyeBlink : MonoBehaviour {

	private int minDelay = 0;
	public float blinkTime = 0.5f;
	private float acc = 0.0f;
	private bool blinking = false; 
	private Vector3 initScale;

	// Use this for initialization
	void Start () {
		initScale = transform.localScale;

	
	}
	
	// Update is called once per frame
	void Update () {
		acc += Time.deltaTime;
		if (acc > blinkTime)
		{
			if (blinking)
			{
				minDelay = 10;
			}
			minDelay--;
			if (minDelay > 0)
			{
				blinking = false;
			}
			else
			{
				blinking = Random.value > 0.8;
			}
			acc=0.0f;
		}
		if (blinking)
		{
			transform.localScale = new Vector3(initScale.x,initScale.y * Mathf.Abs(Mathf.Cos(acc*Mathf.PI/blinkTime)),initScale.z* 1.0f);
		}
	}
}
