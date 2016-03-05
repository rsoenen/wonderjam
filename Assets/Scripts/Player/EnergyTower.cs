using UnityEngine;
using System.Collections.Generic;

public class EnergyTower : MonoBehaviour {
    List<GameObject> bolts = new List<GameObject>();
    public Transform receiverTransform;
	// Use this for initialization

	void Start () {
        GameManager.Instance().towers.Add(this);
    }
	
	// Update is called once per frame
	void Update () {
        foreach(GameObject obj in bolts)
        {
            LightningBolt bolt = obj.GetComponent<LightningBolt>();
            RaycastHit hitTotem;
            if (Physics.Raycast(bolt.initPos, bolt.destPos - bolt.initPos, out hitTotem))
            {
                if (hitTotem.collider.gameObject != bolt.owner.gameObject && hitTotem.collider.gameObject.layer == LayerMask.NameToLayer("Robots"))
                {
                    Debug.DrawLine(bolt.initPos, bolt.destPos, Color.blue, 5);
                    Debug.DrawLine(bolt.owner.gameObject.transform.position, bolt.destPos, Color.yellow, 5);
                    print(hitTotem.collider.gameObject + " " + bolt.owner.gameObject);
                    hitTotem.collider.GetComponent<RobotController>().Die();
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        RobotController robot = collider.GetComponent<RobotController>();
        if (robot != null)
        {
            Transform instance = Transform.Instantiate<Transform>(receiverTransform);
            instance.parent = robot.lightningRod.transform;
            instance.transform.localPosition = new Vector3(0, 0, 0);
            LightningBolt bolt = instance.GetComponent<LightningBolt>();
            bolt.Init(transform, robot);
            bolts.Add(bolt.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        RobotController robot = collider.GetComponent<RobotController>();
        LightningBolt[] rods = robot.lightningRod.GetComponentsInChildren<LightningBolt>();
        for (int i= rods.Length-1; i>=0; i--)
        {
            if (rods[i].GetComponent<LightningBolt>().emitter == transform)
            {
                bolts.Remove(rods[i].gameObject);
                Destroy(rods[i].gameObject);
                
            }
        }
    }
}
