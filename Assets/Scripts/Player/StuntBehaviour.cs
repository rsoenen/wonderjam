using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class StuntBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public float duration, strength, deceleration;
    public Vector3 dir;
    private float time;

    public void Init(Vector3 dir, float strength, float deceleration)
    {
        this.strength = strength;
        this.dir = dir;
        this.deceleration = deceleration;
    }

    void Start()
    {
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.AddForce(dir * strength / Time.fixedDeltaTime);
        body.AddForce(-body.velocity * deceleration);
    }

    void FixedUpdate()
    {
        if(robot.hasControl)
        {
            robot.enabled = true;
            Destroy(this);
        }
    }
}
