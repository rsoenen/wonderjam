using UnityEngine;
using System.Collections;

public class PreviewArena : MonoBehaviour {


	private GameObject mainCam, gC, HUD;

	// Use this for initialization
	void Start () {
        
		mainCam = GameObject.Find ("Main Camera");
		mainCam.SetActive (false);
		//mainCam.GetComponent<Camera>().enabled = false;

		gC = GameObject.Find ("GameController");
		gC.SetActive (false);

		HUD = GameObject.Find ("Ingame HUD");
		HUD.SetActive (false);

        
	}

	void endPreview() {
		mainCam.SetActive(true);
		//mainCam.GetComponent<Camera>().enabled = true;
		gameObject.SetActive (false);
		HUD.SetActive (true);
		gC.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

        if (InputManager.Instance.GetController(0).pause || InputManager.Instance.GetController(0).Turbo)
        {
            endPreview();
        }
	}
}
