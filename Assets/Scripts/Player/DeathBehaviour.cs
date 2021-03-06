﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathBehaviour : MonoBehaviour {
    private float time;
    private GameManager game;


    private Transform head, rightArm, leftArm, rotaringPlateform, body;

	// Use this for initialization
	void Start () {
        time = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<RobotController>().enabled = false;
        game = GameManager.Instance();
        GetComponent<RobotController>().lightningEnabled = false;

        FetchParts();
        EnableRenderers(false);
        EnableCollision(false);
        InstantiateDeadBodyParts();

	}

    void FetchParts()
    {
        body = gameObject.transform.Find("RobotBody");
        rotaringPlateform = gameObject.transform.Find("RotaringPlateform");
        head = gameObject.transform.Find("Neck").Find("Head");
        leftArm = body.transform.Find("LeftArm");
        rightArm = body.transform.Find("RightArm");
    }

    void EnableRenderers(bool _enable)
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
            renderer.enabled = _enable;
    }

    void EnableCollision(bool _enable)
    {
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = _enable;
    }

    void InstantiateDeadBodyParts()
    {
        GameObject deadHead = Instantiate<GameObject>(game.headPrefab);
        deadHead.transform.position = head.transform.position;
        deadHead.transform.rotation = head.transform.rotation;
        deadHead.GetComponent<Rigidbody>().AddForce(5 * Vector3.up + Random.onUnitSphere * 2.0f, ForceMode.Impulse);

        GameObject deadLeftArm = Instantiate<GameObject>(game.leftArmPrefab);
        deadLeftArm.transform.position = leftArm.transform.position;
        deadLeftArm.transform.rotation = leftArm.transform.rotation;
        deadLeftArm.GetComponent<Rigidbody>().AddForce(-deadLeftArm.transform.right * 2.5f, ForceMode.Impulse);

        GameObject deadRightArm = Instantiate<GameObject>(game.rightArmPrefab);
        deadRightArm.transform.position = rightArm.transform.position;
        deadRightArm.transform.rotation = rightArm.transform.rotation;
        deadRightArm.GetComponent<Rigidbody>().AddForce(-deadRightArm.transform.right * 2.5f, ForceMode.Impulse);

        GameObject deadBody = Instantiate<GameObject>(game.bodyPrefab);
        deadBody.transform.position = body.transform.position;
        deadBody.transform.rotation = body.transform.rotation;
        deadBody.GetComponent<Rigidbody>().AddForce(2.5f * deadBody.transform.forward + 2.0f * Vector3.up + Random.onUnitSphere * 1.0f, ForceMode.Impulse);

        GameObject deadRotaringPlateform = Instantiate<GameObject>(game.rotaringPlateformPrefab);
        deadRotaringPlateform.transform.position = rotaringPlateform.transform.position;
        deadRotaringPlateform.transform.rotation = rotaringPlateform.transform.rotation;
        deadRotaringPlateform.GetComponent<Rigidbody>().AddForce(1.5f * Vector3.up, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        //transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 90 * time / game.deathDuration);
        if(time > game.deathDuration)
        {
            Totem closestTotem = game.GetClosestAvailableTotem(transform.position);
            if (closestTotem != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<RobotController>().enabled = true;
                GetComponent<RobotController>().lightningEnabled = true;
                gameObject.AddComponent<SpawnBehaviour>().Init(closestTotem);
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);

                EnableRenderers(true);
                EnableCollision(true);

                Destroy(this);
            }
        }
	}

    
}
