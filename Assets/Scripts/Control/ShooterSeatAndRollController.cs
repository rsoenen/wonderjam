using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

/// <summary>
/// Put this script into the shooter seat
/// </summary>
public class ShooterSeatAndRollController : MonoBehaviour {

  /// <summary>
  /// Roll sensivity
  /// </summary>
  [Range(0.1f,1.0f)]
  public float SensitivityX = 0.5f;

  [Range(0.1f,1.0f)]
  public float SensitivityY = 1.0f;
/*
  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public Transform RightCanon;
 
  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public Transform LeftCanon;
  */
  /// <summary>
  /// The space rocket controller for roll
  /// </summary>
  public SpaceRocketController RocketController;


  /*[Range(-180f, 180f)]
  public float verticalMin = -10.0f, verticalMax = 120.0f;*/
  [Range(-180f,180f)]
  public float seatVerticalMin = -10.0f, seatVerticalMax = 80.0f;

  private Vector3 seatEulerAngles;
/*  private Vector3 canonREulerAngles;
  private Vector3 canonLEulerAngles;
  private float canonRYDelta;
  private float canonLYDelta;*/


	void Start () {
   /* Assert.IsNotNull(RightCanon);
    Assert.IsNotNull(LeftCanon);*/
    Assert.IsNotNull(RocketController);

    seatEulerAngles = this.transform.localRotation.eulerAngles;
   /* canonREulerAngles = RightCanon.localRotation.eulerAngles;
    canonLEulerAngles = LeftCanon.localRotation.eulerAngles;

    canonRYDelta = canonREulerAngles.y - seatEulerAngles.y;
    canonLYDelta = canonLEulerAngles.y - seatEulerAngles.y;
    */
	}

  public float ClampAngle(float y, float min, float max)
  {
    if (y < -360) y += 360;
    if (y > 360) y -= 360;
    return Mathf.Clamp(y, min, max);
  }


  void Update () {

    RocketController.doRoll(Input.GetAxisRaw("ShooterAimX")*-1.0f*SensitivityX);
    seatEulerAngles.y += Input.GetAxisRaw("ShooterAimY")*SensitivityY;
    /*  seatEulerAngles.y = ClampAngle(seatEulerAngles.y, verticalMin, verticalMax);

      canonREulerAngles.y = seatEulerAngles.y + canonRYDelta;
      canonLEulerAngles.y = seatEulerAngles.y + canonLYDelta;

      LeftCanon.localRotation = Quaternion.Euler(canonLEulerAngles);
      RightCanon.localRotation = Quaternion.Euler(canonREulerAngles);*/

    seatEulerAngles.y = ClampAngle(seatEulerAngles.y, seatVerticalMin, seatVerticalMax);
    this.transform.localRotation = Quaternion.Euler(seatEulerAngles);


	}
}
