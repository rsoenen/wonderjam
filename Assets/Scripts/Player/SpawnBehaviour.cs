using UnityEngine;
using System.Collections;

public class SpawnBehaviour : MonoBehaviour {

    GameManager game;
    private RobotController robot;
    private Rigidbody body;
    private Totem totem;
    private float initY;
    private Vector3 speed;
    
    public void Init(Totem totem)
    {
        this.totem = totem;
        transform.position = totem.transform.position;
        totem.occupied = true;
    }

    void Start () {
        this.game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
        this.robot = GetComponent<RobotController>();
        this.body = GetComponent<Rigidbody>();
        speed = totem.initSpeed.x * totem.transform.forward + totem.initSpeed.y * Vector3.up;
        initY = transform.position.y;
        this.body.isKinematic = true;
        robot.SetImmune(game.spawnImmuneTime);
    }
	
	void Update () {
        if (transform.position.y < game.floorY)
        {
            transform.position = new Vector3(transform.position.x, game.floorY, transform.position.z);
            robot.SetImmune(game.spawnImmuneTime);
            body.isKinematic = false;
            totem.occupied = false;
            robot.gameObject.AddComponent<SpawnBlinkBehaviour>();
            Destroy(this);
        }
	}

    void FixedUpdate()
    {
        speed += Vector3.down * totem.gravity * Time.fixedDeltaTime;
        transform.position += speed * Time.fixedDeltaTime;
    }
}
