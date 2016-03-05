using UnityEngine;
using System.Collections.Generic;

public class GroundDetector : MonoBehaviour
{
    public List<Collider> groundColliders = new List<Collider>();
    public float activationTime;
    private float time;

    void Update()
    {
        time += Time.deltaTime;
    }

    public void ResetDetector()
    {
        time = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GroundBox" && !groundColliders.Contains(other))
            groundColliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "GroundBox" && groundColliders.Contains(other))
        {
            groundColliders.Remove(other);
        }
    }

    public bool isOnGround { get{ return time < activationTime || groundColliders.Count > 0; } }
}
