using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class ShooterAimCameraController : CameraControlBase {

  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public Transform RightCanon;

  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public Transform LeftCanon;


  /// <summary>
  /// Shooter seat
  /// </summary>
  public Transform Seat;

  [Range(5.0f, 100.0f)]
  public float aimDistance = 10.0f;


  // Use this for initialization
  void Start()
  {
    Assert.IsNotNull(RightCanon);
    Assert.IsNotNull(LeftCanon);
    Assert.IsNotNull(Seat);

    initialize();


    x = transform.localRotation.eulerAngles.y;
    y = transform.localRotation.eulerAngles.x;
    targetX = transform.localRotation.eulerAngles.y;
    targetY = transform.localRotation.eulerAngles.x;
  }

  // Update is called once per frame
  void Update()
  {

    targetX += Input.GetAxisRaw("ShooterCameraX");
    targetY += Input.GetAxisRaw("ShooterCameraY");

    targetX = ClampAngle(targetX, xMin, xMax);
    targetY = ClampAngle(targetY, yMin, yMax);

    x = Mathf.LerpAngle(x, targetX, damping * Time.deltaTime * 60);
    y = Mathf.LerpAngle(y, targetY, damping * Time.deltaTime * 60);

    transform.localRotation = Quaternion.Euler(y, x, 0);


    Vector3 aimPoint = new Vector3(0.0f, 0.0f, aimDistance);
    aimPoint = transform.rotation * aimPoint  + transform.position;

    LeftCanon.LookAt(aimPoint);
    RightCanon.LookAt(aimPoint);



    /*
    canonREulerAngles.y = y + canonRYDelta + Seat.transform.localEulerAngles.y;
    canonLEulerAngles.y = y + canonLYDelta + Seat.transform.localEulerAngles.y;


    LeftCanon.localRotation = Quaternion.Euler(canonLEulerAngles);
    RightCanon.localRotation = Quaternion.Euler(canonREulerAngles);
    */
     
  }

}
