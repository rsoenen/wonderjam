using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    public float maxSpeed, acceleration, deceleration, inputTrigger, collisionForce;
    public float rotSpeed;
    public int playerId;
    public PlayerInputs input;

    private Rigidbody rigidBody;
    

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (input == null)
            input = InputManager.Instance.pilot[playerId];
        Vector3 inputDir = new Vector3(input.Yaw, 0, -input.Pitch);
        rigidBody.AddForce(acceleration * inputDir);
        rigidBody.AddForce(-rigidBody.velocity * deceleration);
    }

    // Update is called once per frame
    void Update ()
    {
        if (rigidBody.velocity.magnitude != 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rigidBody.velocity, Vector3.up), Time.deltaTime * rotSpeed);

    }
}
