using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

/// <summary>
/// Put this script into the shooter seat
/// </summary>
public class ShooterController : CameraControlBase {

  /// <summary>
  /// Roll sensivity
  /// </summary>
  [Range(1.0f,10.0f)]
  public float SensitivityX = 5.0f;

  [Range(1.0f,10.0f)]
  public float SensitivityY = 5.0f;

  public SpaceRocketController RocketController;

  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public CanonController RightCanon;

  /// <summary>
  /// from the shooter point of view
  /// </summary>
  public CanonController LeftCanon;


  /// <summary>
  /// Shooter seat
  /// </summary>
  public Transform Seat;

  [Range(5.0f, 100.0f)]
  public float aimDistance = 100.0f;


  [Range(-180f,180f)]
  public float seatVerticalMin = -10.0f, seatVerticalMax = 80.0f;
  private float seatEulerAngleY;

  private bool previousFiring = false;
  public MusicController musicController;

  void Start () {
    Assert.IsNotNull(RocketController);
    Assert.IsNotNull(RightCanon);
    Assert.IsNotNull(LeftCanon);
    Assert.IsNotNull(Seat);

    initialize();

    targetX = transform.localRotation.eulerAngles.y;
    targetY = transform.localRotation.eulerAngles.x;

    seatEulerAngleY = Seat.transform.localRotation.eulerAngles.y;

  }

  void Update () {

    bool fireRight = InputManager.Instance.shooter.FireRightCanon;
    bool fireLeft = InputManager.Instance.shooter.FireLeftCanon;

    if (previousFiring && fireRight && fireLeft)
    {
      musicController.fire(false);
    }
    else if (!previousFiring && (fireRight || fireLeft))
    {
      musicController.fire(true);
    }

    previousFiring = false;

    if (fireRight)
    {
      RightCanon.fire();
      previousFiring = true;
    }

    if (fireLeft)
    {
      LeftCanon.fire();
      previousFiring = true;
    }

    targetX += SensitivityX * InputManager.Instance.shooter.AimX;
    targetY += SensitivityY * InputManager.Instance.shooter.AimY;

    float deltaX = 0;
    if (targetX > xMax) {
      deltaX = targetX - xMax;
      RocketController.doRoll (-deltaX);
      targetX = xMax;
    } else if (targetX < xMin) {
      deltaX = targetX - xMin;
      RocketController.doRoll (-deltaX);
      targetX = xMin;
    } else {
      RocketController.doRoll (0);
    }

    float deltaY = 0;
    if (targetY > yMax) {
      deltaY = targetY - yMax;
      if (seatEulerAngleY + deltaY > seatVerticalMax) {
        deltaY = 0;
      }
      targetY = yMax;
    }
    if (targetY < yMin) {
      deltaY = targetY - yMin;
      if (seatEulerAngleY + deltaY < seatVerticalMin) {
        deltaY = 0;
      }
      targetY = yMin;
    }
    seatEulerAngleY += deltaY;

    transform.localRotation = Quaternion.Euler(targetY, targetX, 0);
    Seat.transform.Rotate (deltaY, 0, 0);

    Vector3 aimPoint = new Vector3(0.0f, 0.0f, aimDistance);
    aimPoint = transform.rotation * aimPoint + transform.position;

    LeftCanon.transform.LookAt(aimPoint);
    RightCanon.transform.LookAt(aimPoint);
  }
}
