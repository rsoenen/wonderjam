﻿using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    GameManager game;
    public int playerId;
    public PlayerInputs input;

    public float rodPlacementDistance = 0.5f;

    private Rigidbody rigidBody;

    private Vector3 lastLookDirection = new Vector3(1, 0, 0);

    public Vector3 lookDirection
    {
        get
        {
            return lastLookDirection;
        }
    }
    

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
        SetupRobotForPlayer(playerId);
	}


  public void SetupRobotForPlayer(int player)
  {
    input = InputManager.Instance.GetController(player);
    Color color = InputManager.GetColorFromPlayer(player);
    Light[] lights = GetComponentsInChildren<Light>();
    foreach (Light l in lights)
    {
      l.color = color;
    }
    Transform headChild = transform.Find("Neck").Find("Head");
    headChild.Find("LeftEye").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
    headChild.FindChild("RightEye").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
    headChild.Find("Antenna").FindChild("Receiver").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
  }

	

    void FixedUpdate()
    {
        if (input == null)
            input = InputManager.Instance.GetController(playerId);

        if (input == null)
            return;

        Vector3 inputDir = new Vector3(input.Yaw, 0, -input.Pitch);
        if (inputDir.sqrMagnitude > 0.01)
            lastLookDirection = inputDir;
        rigidBody.AddForce(game.playerAcceleration * inputDir);
        rigidBody.AddForce(-rigidBody.velocity * game.playerDeceleration);
    }

    // Update is called once per frame
    void Update ()
    {
        if (input == null)
            return;

        if (rigidBody.velocity.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
        if(input.Turbo)
        {
            gameObject.AddComponent<DashBehaviour>().Init(lastLookDirection);
        }
        if(input.X)
        {
            RaycastHit hit;
            if(Physics.Raycast(lastLookDirection.normalized * rodPlacementDistance + transform.position, Vector3.down, out hit))
            {
                GetComponent<RodPlacementBehavior>().Activate(hit.point, lastLookDirection.normalized);
            }
        }
        
    }
}
