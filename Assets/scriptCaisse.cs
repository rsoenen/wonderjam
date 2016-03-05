using UnityEngine;
using System.Collections;

public class scriptCaisse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, Time.deltaTime * 100, 0, Space.World);
	}
}
