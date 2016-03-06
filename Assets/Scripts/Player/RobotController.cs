using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(AudioSource))]
public class RobotController : MonoBehaviour
{
    GameManager game;
    public int playerId;
    public PlayerInputs input;

   
    private int nombreKill;
    public GameObject lastAttack;

   

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

    private bool m_encumbered;
    public bool Encumbered
    {
        get { return m_encumbered; }
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
        AntennaRenderer = headChild.Find("Antenna").FindChild("Receiver").GetComponent<MeshRenderer>();
    headTransform = headChild;

        rotaringPlatformTransform = transform.Find("RotaringPlateform");
  }

    void FixedUpdate()
    {
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

        if(input.Y)
        {
            GetComponent<RobotGestionPoint>().ActivatePowerup();
        }

        {
            RaycastHit hit;
            if (Physics.SphereCast(rotaringPlatformTransform.position, 1.0f, -rotaringPlatformTransform.up, out hit, 2.0f, LayerMask.GetMask("ThrowableObjects")))
            {
                if (hit.transform.GetComponent<ThrowableObject>().Grabbed)
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
            if (lastAttack != null)
            {
                lastAttack.gameObject.GetComponent<RobotController>().addOneKill();
            }
            gameObject.AddComponent<FallingBehaviour>().Init(rigidBody.velocity);
        }
    }

    private int m_connectionCount = -1;

    public void OnBoltConnected()
    {
        if (AntennaRenderer.GetComponentInChildren<LightningBolt>() != null)
            ChangeAntennaEmission(true);
    }

    public void OnBoltDisconnected()
    {
        if (AntennaRenderer.GetComponentInChildren<LightningBolt>() == null)
            ChangeAntennaEmission(false);
    }

    public void Die()
    {
        if(immuneTime < 0 && GetComponent<DeathBehaviour>() == null)
        {
			GetComponent<AudioSource>().PlayOneShot(dieVoices[Random.Range(0,dieVoices.Length)]);
            //Debug.Log("You are dead.");
            this.gameObject.GetComponent<RobotGestionPoint>().reducePoint(20);
            if (lastAttack != null)
            {
                lastAttack.gameObject.GetComponent<RobotController>().addOneKill();
            }
            gameObject.AddComponent<DeathBehaviour>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Caisse")
        {
            collision.gameObject.GetComponent<scriptCaisse>().Consume(this);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Caisse")
        {

        }
    }

    public void addOneKill()
    {
        this.nombreKill++;
        int idHud=playerId+1;
        GameObject.Find("KillPlayer" + idHud).GetComponent<Text>().text = ""+nombreKill ;
    }
    public int getNumberKill()
    {
        return this.nombreKill;
    }
}
