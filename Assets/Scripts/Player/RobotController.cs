using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
    public float maxSpeed, acceleration, deceleration, inputTrigger;
    public int playerId;
    public PlayerInputs input;

    private Vector2 speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(input == null)
        {
            input = InputManager.Instance.pilot[playerId];
        }
        Vector2 inputDir = new Vector2(input.Yaw, -input.Pitch);
        speed += acceleration * inputDir * Time.deltaTime;
        if (inputDir.magnitude < inputTrigger)
            speed *= Mathf.Pow(deceleration, Time.deltaTime);
        if (speed.magnitude > maxSpeed)
            speed *= maxSpeed / speed.magnitude;
        transform.position += new Vector3(speed.x, 0, speed.y) * Time.deltaTime;
    }
}
