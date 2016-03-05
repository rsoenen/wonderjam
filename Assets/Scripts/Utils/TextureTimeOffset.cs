using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class TextureTimeOffset : MonoBehaviour {

	public float xMax;
	public float yMax;
	public float xMin;
	public float yMin;
	public float xSpeed;
	public float ySpeed;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = GetComponent<Renderer>().material.GetTextureOffset("_MainTex"); 
		offset.x += xSpeed * Time.deltaTime;
		offset.y += ySpeed * Time.deltaTime;
		if (offset.x > xMax)
		{
			offset.x = xMin;
		}
		if (offset.y > yMax)
		{
			offset.y = yMin;
		}
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex",offset);
	}
}
