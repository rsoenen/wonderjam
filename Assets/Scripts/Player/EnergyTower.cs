using UnityEngine;
using System.Collections;

public class EnergyTower : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance().towers.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
