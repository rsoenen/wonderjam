using UnityEngine;
using System.Collections;

public class FallingBehaviour : MonoBehaviour {
    GameManager game;
    Vector3 speed;

    public void Init(Vector3 initSpeed)
    {
        this.speed = initSpeed;
    }

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<RobotController>().enabled = false;
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        speed += Vector3.down * game.gravity * Time.fixedDeltaTime;
        transform.position += speed * Time.fixedDeltaTime;

        if(transform.position.y < game.deathHeight)
        {
            Totem closestTotem = game.GetClosestAvailableTotem(transform.position);
            if (closestTotem != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<RobotController>().enabled = true;
                gameObject.AddComponent<SpawnBehaviour>().Init(closestTotem);
                Destroy(this);
            }
        }
    }
}
