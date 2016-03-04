using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class ExteriorCameraControl : CameraControlBase {

  public Camera cockpitCamera;
  [Range(1.0f,30.0f)] public float distance=10.0f;
  public Transform target;

	// Use this for initialization
	void Start () {
    initialize();
    Assert.IsNotNull(cockpitCamera);
    Assert.IsNotNull(target);
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKeyDown(KeyCode.L)) {
			toggleLock();
		}
		
    if (InputManager.Instance.pilot.SwitchCamera > 0 )
    {
      gameObject.SetActive(false);
      cockpitCamera.gameObject.SetActive(true);
    }

		targetY += InputManager.Instance.pilot.CameraX;
		targetX += InputManager.Instance.pilot.CameraY;

    //targetX = ClampAngle(targetX,xMin,xMax);
		//targetY = ClampAngle(targetY,yMin,yMax);

    x = Mathf.LerpAngle(x, targetX, damping * Time.deltaTime * 60);
    y = Mathf.LerpAngle(y, targetY, damping * Time.deltaTime * 60);

    float rad_x = Mathf.Deg2Rad*x;
    float rad_y = Mathf.Deg2Rad*y;

    Vector3 delta = new Vector3(Mathf.Cos(rad_x) * Mathf.Cos(rad_y), Mathf.Sin(rad_x) * Mathf.Cos(rad_y), Mathf.Sin(rad_y));

    transform.position = target.position + target.rotation * delta * distance;
    transform.LookAt(target, target.rotation * Vector3.up );
	}


}
