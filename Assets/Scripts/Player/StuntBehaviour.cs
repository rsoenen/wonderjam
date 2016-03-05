using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class StuntBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public Vector3 dir;
    private float time;

    GameInstance game;

    public void Init(Vector3 dir)
    {
        this.dir = dir;
    }

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameInstance>();
        robot = GetComponent<RobotController>();
        body = GetComponent<Rigidbody>();
        robot.enabled = false;
        body.AddForce(dir * game.stuntStrength / Time.fixedDeltaTime);
        body.AddForce(-body.velocity * game.stuntDeceleration);
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
