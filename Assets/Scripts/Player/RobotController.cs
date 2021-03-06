﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(AudioSource))]
public class RobotController : MonoBehaviour
{
    GameManager game;
    public int playerId;
    public PlayerInputs input;

   
    private int killsCount;
    public RobotController lastHitRobot;
    private float timerLastHit;


    public float rodPlacementDistance = 0.5f;

    private Rigidbody rigidBody;

    private GameObject gameController;
    private Vector3 lastLookDirection = new Vector3(1, 0, 0);
    private Transform headTransform;
    private Transform rotaringPlatformTransform;
    public Transform lightningRod;

    private MeshRenderer AntennaRenderer;

    private GroundDetector groundDetector;

    private float immuneTime;

	public AudioClip[] dieVoices;
	public AudioClip[] dashSounds;
	public AudioClip connectSound;
	public AudioClip disconnectSound;
	public AudioClip outOfBatterySound;
	public AudioClip pickUpCaisseSound;

    private Color m_color;
    public Color Color
    {
        get { return m_color; }
    }


    private bool m_encumbered;
    public bool Encumbered
    {
        get{ return m_encumbered; }
        set
        {
            m_encumbered = value;
            OnEncumberedChanged();
        }
    }

    [SerializeField]
    private GameObject pickupArrow;


    private void OnEncumberedChanged()
    {
        // maybe change speed ?
    }

    public Vector3 lookDirection
    {
        get
        {
            return lastLookDirection;
        }
    }

    private bool m_lightningEnabled = true;
    public bool lightningEnabled
    {
        get
        {
            return m_lightningEnabled;
        }
        set
        {
            m_lightningEnabled = value;
            lightningRod.gameObject.SetActive(value);
        }
    }

    private bool m_AntennaEmission = false;
    private void ChangeAntennaEmission(bool _value)
    {
        if(m_AntennaEmission != _value)
        {
            AntennaRenderer.material.SetColor("_EmissionColor", _value ? AntennaRenderer.material.color : Color.black);
            m_AntennaEmission = _value;
			if (_value)
			{
			GetComponent<AudioSource>().PlayOneShot(connectSound);
			}
			else
			{
			GetComponent<AudioSource>().PlayOneShot(disconnectSound);
			}
        }
    }

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
        rigidBody = GetComponent<Rigidbody>();
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
        SetupRobotForPlayer(playerId);
        groundDetector = GetComponent<GroundDetector>();
        SetImmune(game.spawnImmuneTime);
        timerLastHit = -1;
	}

    public void SetImmune(float seconds)
    {
        immuneTime = seconds;
    }

  public void SetupRobotForPlayer(int player)
  {
    input = InputManager.Instance.GetController(player);
    Color color = InputManager.GetColorFromPlayer(player);
    Light[] lights = GetComponentsInChildren<Light>();
    foreach (Light l in lights)
    {
        l.color = color;
    }
    Transform headChild = transform.Find("Neck").Find("Head");
    headChild.Find("LeftEye").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
    headChild.FindChild("RightEye").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
    headChild.Find("Antenna").FindChild("Receiver").GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
        m_color = color;
        AntennaRenderer = headChild.Find("Antenna").FindChild("Receiver").GetComponent<MeshRenderer>();
    headTransform = headChild;

        rotaringPlatformTransform = transform.Find("RotaringPlateform");
  }

    void FixedUpdate()
    {
        if (timerLastHit < 0)
        {
            lastHitRobot = null;
        }
        timerLastHit -= Time.deltaTime;
        if (input == null)
            input = InputManager.Instance.GetController(playerId);
        int controleInverse = 1;
        if (gameController.GetComponent<GameManager>().invertedControl&&gameController.GetComponent<GameManager>().ThrowerInvertedControl !=this.gameObject)
        {
            controleInverse = -1;
        }
        Vector3 inputDir = new Vector3(input.Yaw, 0, -input.Pitch);
        // print(inputDir);
        if (inputDir.sqrMagnitude > 0.01)
            lastLookDirection = controleInverse*inputDir;

        rigidBody.AddForce(controleInverse*game.playerAcceleration * inputDir);
        rigidBody.AddForce(-rigidBody.velocity * game.playerDeceleration);
        if (rigidBody.velocity.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
        }
        else rigidBody.angularVelocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update ()
    {

        if (AntennaRenderer.GetComponentInChildren<LightningBolt>() == null)
            ChangeAntennaEmission(false);
        else
            ChangeAntennaEmission(true);

        if(immuneTime > 0 && immuneTime < Time.deltaTime)
        {
            Destroy(GetComponent<SpawnBlinkBehaviour>());
        }
        immuneTime -= Time.deltaTime;
        
        if (input == null)
            return;
        
        if(input.Turbo)
        {
            if(!m_encumbered)
            {
                if (this.gameObject.GetComponent<RobotGestionPoint>().getPoint() > 0)
                {
                    this.gameObject.GetComponent<RobotGestionPoint>().reducePoint(5);
                    gameObject.AddComponent<DashBehaviour>().Init(lastLookDirection);
					GetComponent<AudioSource>().PlayOneShot(dashSounds[Random.Range(0,dashSounds.Length)]);
                }
            }
        }

        if(input.X)
        {
            RaycastHit hit;
            if(Physics.Raycast(lastLookDirection.normalized * rodPlacementDistance + headTransform.position, Vector3.down, out hit))
            {
                GetComponent<RodPlacementBehavior>().Activate(hit.point, lastLookDirection.normalized);
            }
        }
        if (input.pause)
        {
            GameObject.Find("UI").GetComponent<Pause>().DoPause();
        }

        if(input.Y)
        {
            GetComponent<RobotGestionPoint>().ActivatePowerup();
        }

        {
            RaycastHit hit;
            if (Physics.SphereCast(rotaringPlatformTransform.position, 1.0f, -rotaringPlatformTransform.up, out hit, 2.0f, LayerMask.GetMask("ThrowableObjects")))
            {
                ThrowableObject thobj = hit.transform.GetComponentInChildren<ThrowableObject>();
                if (thobj != null && !thobj.Grabbed)
                {
                    if (!pickupArrow.activeSelf)
                        pickupArrow.SetActive(true);
                    pickupArrow.transform.position = hit.transform.position + Vector3.up * 1.0f;

                    if (input.B)
                        GetComponent<ThrowingBehavior>().GrabObject(hit.transform.gameObject);
                }

            }
            else
            {
                if (pickupArrow.activeSelf)
                    pickupArrow.SetActive(false);

                if (input.B)
                    GetComponent<ThrowingBehavior>().ThrowObject(headTransform.forward);
            }
        }

        /* TEST */
        if(Input.GetButtonDown("Test"))
        {
            Die();
        }

        if (!groundDetector.isOnGround)
        {
            //Die();
            gameObject.AddComponent<FallingBehaviour>().Init(rigidBody.velocity);
        }
    }

    //private int m_connectionCount = -1;

  /*  public void OnBoltConnected()
    {
        if (AntennaRenderer.GetComponentInChildren<LightningBolt>() != null)
		{
            ChangeAntennaEmission(true);
			GetComponent<AudioSource>().PlayOneShot(connectSound);

		}
    }

    public void OnBoltDisconnected()
    {
        if (AntennaRenderer.GetComponentInChildren<LightningBolt>() == null)
		{
			Debug.Log("goes here");
            ChangeAntennaEmission(false);
			GetComponent<AudioSource>().PlayOneShot(disconnectSound);
		}
    }*/

    public void Die()
    {
        if(immuneTime < 0 && GetComponent<DeathBehaviour>() == null)
        {
			GetComponent<AudioSource>().PlayOneShot(dieVoices[Random.Range(0,dieVoices.Length)]);
            //Debug.Log("You are dead.");
            this.gameObject.GetComponent<RobotGestionPoint>().reducePoint(20);
            if (lastHitRobot != null)
            {
                GiveKill();
            }
            gameObject.AddComponent<DeathBehaviour>();
        }
    }

	public void OutOfBattery()
	{
		if (lastHitRobot != null)
        {
        	GiveKill();
        }

		GetComponent<AudioSource>().PlayOneShot(outOfBatterySound);
		gameObject.AddComponent<NoBatteryBehavior>();
	}

    public void SetLastHit(RobotController robot)
    {
        if (immuneTime < 0 && GetComponent<DeathBehaviour>() == null)
        {
            timerLastHit = 5;
            lastHitRobot = robot;
        }
        
    }

    public void GiveKill()
    {
        if (lastHitRobot != null)
        {
            lastHitRobot.killsCount++;
            //print(lastHitRobot.killsCount);
            lastHitRobot = null;
        }
        else
        {
            killsCount--;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Caisse")
        {
            collision.gameObject.GetComponent<scriptCaisse>().Consume(this);
			GetComponent<AudioSource>().PlayOneShot(pickUpCaisseSound);
            Destroy(collision.gameObject);
        }
    }

    public int getKillsCount()
    {
        return this.killsCount;
    }
}
