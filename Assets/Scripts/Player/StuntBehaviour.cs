using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class StuntBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public float duration, strength;
    public Vector3 dir;
    private float time;

    public void Init(Vector3 dir, float strength)
    {
        this.strength = strength;
        this.dir = dir;
    }

    void Start()
    {
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.AddForce(dir * strength / Time.fixedDeltaTime);
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
