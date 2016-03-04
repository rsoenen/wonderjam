using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PilotController : SpaceRocketController
{
  [Header("Human feedback")]
  public MusicController musicContoller;


  void Start()
  {
    initialize();
  }


  void Update()
  {
    //Only graphical 
    UpdateFeedbacks();
  }

  // Update is called once per frame
	void FixedUpdate () {

    //Music feedback

    bool turbo = InputManager.Instance.pilot.Turbo;
    if (useTurbo.Value && turbo)
    {
      musicContoller.turbo(false);
    }
    else if (!useTurbo.Value && !turbo)
    {
      musicContoller.turbo(true);
    }

    Vector3 velocity = body.velocity;
    Vector3 angularVelocity = body.angularVelocity;
    Vector3 localangularvelocity = transform.InverseTransformDirection(angularVelocity).normalized * angularVelocity.magnitude;
    Vector3 localvelocity = transform.InverseTransformDirection(velocity);

    float speed = velocity.magnitude;
    doPitch(InputManager.Instance.pilot.Pitch);
    doYaw(InputManager.Instance.pilot.Yaw);
    doForward(InputManager.Instance.pilot.Forward);
    doBackward(InputManager.Instance.pilot.Backward);
    doTurbo(turbo);
    doBowX(0);
    doBowY(0);
    //doRoll(Input.GetAxisRaw("RollSpaceRocket"));
    Debug.Log("velocity: " + localvelocity + " speed: " + speed +  " angular velocity: " + localangularvelocity + " angular speed: " + localangularvelocity.magnitude + " turbo: " + useTurbo.Value);
    clampMaxVelocity(speed);
    doAngularStabilizer();
    doLinearStabilizer();

	}
}
