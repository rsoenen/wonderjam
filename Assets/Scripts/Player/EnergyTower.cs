﻿using UnityEngine;
using System.Collections.Generic;

public class EnergyTower : MonoBehaviour {

    List<GameObject> bolts = new List<GameObject>();
   
    private GameObject ThrowerLengthReducted;
    private float timerLengthReducte;

    public Transform receiverTransform;
    public Transform spawnerPrefab;
    public int spawnerCount;
	// Use this for initialization

    void Awake()
    {
       
    }

	public void SetupSpawns()
	{ 

		for (int i = 0; i < spawnerCount; i++)
        {
            Transform instance = Transform.Instantiate<Transform>(spawnerPrefab);
            instance.parent = transform;
            instance.localRotation = Quaternion.Euler(new Vector3(0, 0, i * 360 / spawnerCount));
            instance.localPosition = Vector3.zero;
        }
		
	}

	void Start () {
        GameManager.Instance().towers.Add(this);
        timerLengthReducte = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        
        foreach(GameObject obj in bolts)
        {
            LightningBolt bolt = obj.GetComponent<LightningBolt>();
            if(bolt.owner.GetComponent<RobotController>().lightningEnabled)
            {
                Debug.DrawLine(bolt.initPos + Vector3.up, bolt.destPos + Vector3.up);
                foreach (var hitTotem in Physics.RaycastAll(bolt.initPos, bolt.destPos - bolt.initPos, LayerMask.GetMask("Robots")))
                {
                    if (hitTotem.collider.GetComponent<RobotController>() == null)
                        continue;

                    if (hitTotem.collider.gameObject != bolt.owner.gameObject)
                    {
                        print(bolt.owner);
                        hitTotem.collider.GetComponent<RobotController>().SetLastHit(bolt.owner.GetComponent<RobotController>());
                        hitTotem.collider.GetComponent<RobotController>().Die();
                    }

                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        RobotController robot = collider.GetComponent<RobotController>();
        if (robot != null && robot.lightningEnabled)
        {
            if (robot != null)
            {
                Transform instance = Transform.Instantiate<Transform>(receiverTransform);
                instance.parent = robot.lightningRod.transform;
                EnergyLossBehaviour behaviour = robot.GetComponent<EnergyLossBehaviour>();
                if (behaviour != null)
                {
                    Destroy(behaviour);
                }
                instance.transform.localPosition = new Vector3(0, 0, 0);
                LightningBolt bolt = instance.GetComponent<LightningBolt>();
                bolt.Init(transform, robot, GetComponent<SphereCollider>().radius);
                bolts.Add(bolt.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        RobotController robot = collider.GetComponent<RobotController>();

        if (robot != null)
        {
            LightningBolt[] rods = robot.lightningRod.GetComponentsInChildren<LightningBolt>();
            for (int i = rods.Length - 1; i >= 0; i--)
            {
                if (rods[i].GetComponent<LightningBolt>().emitter == transform)
                {
                    bolts.Remove(rods[i].gameObject);
                    Destroy(rods[i].gameObject);
                }
            }
        }
        
    }

    public void reduceArc(GameObject thrower)
    {
        this.ThrowerLengthReducted = thrower;
        this.gameObject.GetComponent<SphereCollider>().radius = 2;
    }
}
