using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    public float maxSpeed, acceleration, deceleration, inputTrigger, collisionForce;
    public int playerId;
    public PlayerInputs input;

    private Vector2 speed;

    private List<SphereCollider> colliders = new List<SphereCollider>(); 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(input == null)
        {
            input = InputManager.Instance.pilot[playerId];
        }
        Vector2 inputDir = new Vector2(input.Yaw, -input.Pitch);
        speed += acceleration * inputDir * Time.deltaTime;
        if (inputDir.magnitude < inputTrigger)
            speed *= Mathf.Pow(deceleration, Time.deltaTime);
        if (speed.magnitude > maxSpeed)
            speed *= maxSpeed / speed.magnitude;
        transform.position += new Vector3(speed.x, 0, speed.y) * Time.deltaTime;

        foreach(SphereCollider collider in colliders)
        {
            Vector3 forceDir = (transform.position - collider.transform.position);
            float optimalDist = collider.radius + GetComponent<SphereCollider>().radius;
            speed += optimalDist / forceDir.magnitude * collisionForce * Time.deltaTime * new Vector2(forceDir.x, forceDir.z).normalized;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        SphereCollider collider = other.GetComponent<SphereCollider>();
        if (collider != null)
        {
            colliders.Add(collider);
        }
    }

    void OnTriggerExit(Collider other)
    {
        colliders.Remove(other.GetComponent<SphereCollider>());
    }
}
