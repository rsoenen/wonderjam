using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class DashBehaviour : MonoBehaviour
{
    GameController game;
    private RobotController robot;
    private Rigidbody body;
    public Vector3 dir;
    private float time;

    public void Init(Vector3 dir)
    {
        this.dir = dir;
    }

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameController>();
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.velocity = dir.normalized * game.dashSpeed;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > game.dashDuration)
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
            other.gameObject.AddComponent<StuntBehaviour>().Init(collision.contacts[0].normal);
        }
    }
}
