﻿using UnityEngine;
using System.Collections.Generic;

public class EnergyTower : MonoBehaviour {

    List<GameObject> bolts = new List<GameObject>();
   
    public bool lengthReducted;
    private GameObject ThrowerLengthReducted;
    private float timerLengthReducte;

    public Transform receiverTransform;
    public Transform spawnerPrefab;
    public int spawnerCount;
	// Use this for initialization

    void Awake()
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
        lengthReducted = false;
        timerLengthReducte = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        
        foreach(GameObject obj in bolts)
        {
            LightningBolt bolt = obj.GetComponent<LightningBolt>();
            if(bolt.owner.GetComponent<RobotController>().lightningEnabled)
            {
                RaycastHit hitTotem;
                if (Physics.Raycast(bolt.initPos, bolt.destPos - bolt.initPos, out hitTotem))
                {

                    if (hitTotem.collider.gameObject != bolt.owner.gameObject && hitTotem.collider.gameObject.layer == LayerMask.NameToLayer("Robots"))
                    {
                        hitTotem.collider.GetComponent<RobotController>().Die();
                    }

                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        RobotController robot = collider.GetComponent<RobotController>();
        if (robot != null && robot.lightningEnabled&&!lengthReducted)
        {
            if (robot != null)
            {
                Transform instance = Transform.Instantiate<Transform>(receiverTransform);
                instance.parent = robot.lightningRod.transform;
                instance.transform.localPosition = new Vector3(0, 0, 0);
                LightningBolt bolt = instance.GetComponent<LightningBolt>();
                bolt.Init(transform, robot, GetComponent<SphereCollider>().radius);
                bolts.Add(bolt.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (!lengthReducted)
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
        
    }

    public void reduceArc(GameObject thrower)
    {
        this.ThrowerLengthReducted = thrower;
        lengthReducted = true;
        this.gameObject.GetComponent<SphereCollider>().radius = 2;
    }
}
