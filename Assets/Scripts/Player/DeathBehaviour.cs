using UnityEngine;
using System.Collections;

public class DeathBehaviour : MonoBehaviour {
    private float time;
    private GameManager game;

	// Use this for initialization
	void Start () {
        time = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<RobotController>().enabled = false;
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        print("Mort ="+time);
        time += Time.deltaTime;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 90 * time / game.deathDuration);
        if(time > game.deathDuration)
        {
            Totem closestTotem = game.GetClosestAvailableTotem(transform.position);
            if (closestTotem != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<RobotController>().enabled = true;
                gameObject.AddComponent<SpawnBehaviour>().Init(closestTotem);
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
                Destroy(this);
            }
        }
	}
    
}
