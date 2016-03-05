using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RobotController))]
public class StuntBehaviour : MonoBehaviour
{
    private RobotController robot;
    private Rigidbody body;
    public Vector3 dir;
    private float time;

    GameManager game;

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
        body.AddForce(-dir * game.stuntStrength / Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        body.AddForce(-body.velocity * game.stuntDeceleration);
        if (body.velocity.sqrMagnitude < game.stuntControlSpeed * game.stuntControlSpeed)
        {
            robot.enabled = true;
            Destroy(this);
        }
    }
}
