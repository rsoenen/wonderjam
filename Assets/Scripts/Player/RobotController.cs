using UnityEngine;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    GameManager game;
    public int playerId;
    public PlayerInputs input;

    private int m_layerBot = 0;
    public float rodPlacementDistance = 0.5f;

    private Rigidbody rigidBody;

    private GameObject gameController;
    private Vector3 posTotem;
    private Vector3 lastLookDirection = new Vector3(1, 0, 0);
    private Transform headTransform;

    public Vector3 lookDirection
    {
        get
        {
            return lastLookDirection;
        }
    }
    

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
        rigidBody = GetComponent<Rigidbody>();
        m_layerBot = LayerMask.NameToLayer("Robots");
        posTotem = GameObject.FindGameObjectWithTag("Totem").transform.position;
        game = GameObject.FindGameObjectWithTag("Constants").GetComponent<GameManager>();
        SetupRobotForPlayer(playerId);

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
    headTransform = headChild;
  }

    void FixedUpdate()
    {
        if (input == null)
            input = InputManager.Instance.GetController(playerId);
        int controleInverse = 1;
        if (gameController.GetComponent<GameManager>().invertedControl)
        {
            controleInverse = -1;
        }
        Vector3 inputDir = new Vector3(input.Yaw, 0, -input.Pitch);
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
        RaycastHit hitTotem;
        Vector3 posRobot = this.transform.position + (posTotem-transform.position).normalized*1.0f;
        //Debug.DrawLine(posRobot, posTotem);

        if (Physics.Raycast(posTotem, posRobot, out hitTotem))
        {
            if (hitTotem.collider.gameObject != this.gameObject && hitTotem.collider.gameObject.layer == m_layerBot)
            {
                //METTRE L'ACTION QUAND UN ENEMI PASSE DANS NOTRE LIEN
            }
            
           
        }
        
        if (input == null)
            return;


        
        if(input.Turbo)
        {
            gameObject.AddComponent<DashBehaviour>().Init(lastLookDirection);
        }

        if(input.X)
        {
            print("X");
            RaycastHit hit;
            if(Physics.Raycast(lastLookDirection.normalized * rodPlacementDistance + headTransform.position, Vector3.down, out hit))
            {
                GetComponent<RodPlacementBehavior>().Activate(hit.point, lastLookDirection.normalized);
            }
        }
        
        /* TEST */
        if(Input.GetButtonDown("Test"))
        {
            Die();
        }
    }

    void Die()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawn");
        Totem closestTotem = null;
        for(int i=0; i<spawners.Length; i++)
        {
            Totem totem = spawners[i].GetComponent<Totem>();
            if (!totem.occupied && (closestTotem == null || Vector3.Distance(totem.transform.position, transform.position) < Vector3.Distance(closestTotem.transform.position, transform.position)))
            {
                closestTotem = totem;
            }
        }
        if (closestTotem != null)
        {
            gameObject.AddComponent<SpawnBehaviour>().Init(closestTotem);
        }
    }
}
