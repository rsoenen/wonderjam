using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    public float maxSpeed, acceleration, deceleration, inputTrigger, dashSpeed, dashDuration, dashStrength;
    public float rotSpeed;
    public int playerId;
    public PlayerInputs input;

    private Rigidbody rigidBody;

    private Vector3 lastLookDirection = new Vector3(1, 0, 0);

    public float controlSpeed;

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
        SetupRobotForPlayer(playerId);
	}


  public void SetupRobotForPlayer(int player)
  {
    input = InputManager.Instance.controllers[player];
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
            input = InputManager.Instance.controllers[playerId];
        Vector3 inputDir = new Vector3(input.Yaw, 0, -input.Pitch);
        if (inputDir.sqrMagnitude > 0.01)
            lastLookDirection = inputDir;
        rigidBody.AddForce(acceleration * inputDir);
        rigidBody.AddForce(-rigidBody.velocity * deceleration);
    }

    // Update is called once per frame
    void Update ()
    {
        if (rigidBody.velocity.magnitude != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rigidBody.velocity, Vector3.up), Time.deltaTime * rotSpeed);
        if(input.Turbo)
        {
            gameObject.AddComponent<DashBehaviour>().Init(lastLookDirection, dashDuration, dashSpeed, dashStrength);
            
        }
    }

    public bool hasControl { get { return rigidBody.velocity.sqrMagnitude < controlSpeed * controlSpeed; } }
}
