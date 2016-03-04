using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class DashBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public float duration, speed;
    private float time;

    public void Init(float duration, float speed)
    {
        this.duration = duration;
        this.speed = speed;
    }

	void Start ()
    {
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.velocity = body.velocity.normalized* speed;
	}
	
	void FixedUpdate ()
    {
        time += Time.fixedDeltaTime;
        if (time > duration)
        {
            robot.enabled = true;
            Destroy(this);
        }
	}
}
