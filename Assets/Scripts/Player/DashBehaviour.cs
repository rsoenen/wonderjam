using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class DashBehaviour : MonoBehaviour
{
    GameManager game;
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
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.velocity = dir.normalized * game.dashSpeed;
        robot.SetImmune(game.dashDuration);
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
        print(GetComponent<RobotController>());
        RobotController other = collision.collider.GetComponent<RobotController>();
        if (other != null && other.enabled)
        {
            print(GetComponent<RobotController>());
            other.GetComponent<RobotController>().SetLastHit(GetComponent<RobotController>());
            Vector3 normalSpeed = Vector3.Project(body.velocity, collision.contacts[0].normal);
            Vector3 tangentSpeed = body.velocity - normalSpeed;
            other.gameObject.AddComponent<StuntBehaviour>().Init(collision.contacts[0].normal);
            body.velocity = tangentSpeed + normalSpeed * game.dashContactSlow;
        }
    }
}
