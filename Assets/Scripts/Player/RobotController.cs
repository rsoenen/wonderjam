using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        print(InputManager.Instance.pilot[0].CameraX);


    }
}
