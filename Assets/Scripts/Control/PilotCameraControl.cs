using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class PilotCameraControl : CameraControlBase {

  public Camera exteriorCamera;

  // Use this for initialization
	void Start () {
    initialize();
    Assert.IsNotNull(exteriorCamera);

    x = transform.localRotation.eulerAngles.y;
    y = transform.localRotation.eulerAngles.x;
		targetX = transform.localRotation.eulerAngles.y;
		targetY = transform.localRotation.eulerAngles.x;
   }
	
	// Update is called once per frame
	void Update () {

    if (InputManager.Instance.pilot.SwitchCamera < 0 )
    {
      gameObject.SetActive(false);
      exteriorCamera.gameObject.SetActive(true);
    }

    targetX += InputManager.Instance.pilot.CameraX; 
    targetY += InputManager.Instance.pilot.CameraY; 

    targetX = ClampAngle(targetX,xMin,xMax);
		targetY = ClampAngle(targetY,yMin,yMax);

    x = Mathf.LerpAngle(x, targetX, damping * Time.deltaTime * 60);
    y = Mathf.LerpAngle(y, targetY, damping * Time.deltaTime * 60);


		Quaternion rotation = Quaternion.Euler(y, x, 0);
		transform.localRotation = rotation;
  }
}
