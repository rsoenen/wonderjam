using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class DashBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public float duration, speed, stuntStrength, stuntDeceleration;
    public Vector3 dir;
    private float time;

    public void Init(Vector3 dir, float duration, float speed, float strength)
    {
        this.duration = duration;
        this.speed = speed;
        this.dir = dir;
        this.stuntStrength = strength;
    }

    void Start()
    {
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.velocity = dir.normalized * speed;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > duration)
        {
            robot.enabled = true;
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        RobotController other = collision.collider.GetComponent<RobotController>();
        if (other != null && other.enabled)
        {
            other.gameObject.AddComponent<StuntBehaviour>().Init(collision.contacts[0].normal, stuntStrength, stuntDeceleration);
        }
    }
}
