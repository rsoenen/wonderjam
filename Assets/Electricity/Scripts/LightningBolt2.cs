using UnityEngine;
using System.Collections;

public class LightningBolt2 : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{		
    GetComponent<MeshRenderer>().materials[0].SetFloat("_StartSeed",Random.value*1000);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

