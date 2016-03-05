using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager s_Instance = null;
    public static GameManager Instance() { return s_Instance; }

    public GameObject prefabRobot;
    public HudController prefabHUD;
    public GameObject[] prefabCaisses;
    private Transform[] totemsTransform;
    private int playerCount;
    private float timeSpawnItem;


    public bool invertedControl;
    public GameObject ThrowerInvertedControl;
    public float timerInverted;

    public float stuntDeceleration, stuntDuration, stuntStrength, stuntControlSpeed, floorY;
    public float dashDuration, dashSpeed, dashContactSlow;
    public float playerMaxSpeed, playerAcceleration, playerDeceleration;
    public float playerRotSpeed;
    public float deathDuration;
    public float gravity;
    public float deathHeight;
    public float spawnImmuneTime;
    private GameObject[] myRobots;

    private GameObject[] spawns;

    public List<EnergyTower> towers = new List<EnergyTower>();

    [Header("Dead Body Parts Prefabs")]
    public GameObject headPrefab;
    public GameObject rightArmPrefab;
    public GameObject leftArmPrefab;
    public GameObject bodyPrefab;
    public GameObject rotaringPlateformPrefab;

    // Use this for initialization
    void Start () {
        ThrowerInvertedControl = null;
        timerInverted = 0f;
        invertedControl = false;

        timeSpawnItem = 0f;
        playerCount = InputManager.Instance.controllers.Count;
        myRobots = new GameObject[playerCount];
        
        GameObject[] totems =  GameObject.FindGameObjectsWithTag("Totem");
        totemsTransform = new Transform[totems.Length];
        for (int i = 0; i < totems.Length; i++)
        {
            totemsTransform[i] = totems[i].transform;
        }

        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        for (int i = 0; i < playerCount; i++)
        {
            myRobots[i] = (GameObject)Instantiate(prefabRobot, spawns[i].transform.position, Quaternion.identity);
            SetupRobot(myRobots[i], i);
        }

        HudController hud = Instantiate<HudController>(prefabHUD);
        for (int i = 0; i < playerCount; i++)
        {
            hud.m_HudJoueur[i].Init(myRobots[i].GetComponent<RobotGestionPoint>());
        }

        if (playerCount < 4)
        {
            hud.m_HudJoueur[3].gameObject.SetActive(false);
        }
        if (playerCount < 3)
        {
            hud.m_HudJoueur[2].gameObject.SetActive(false);
        }
        if (playerCount < 2)
        {
            hud.m_HudJoueur[1].gameObject.SetActive(false);
        }
    }

    private void SetupRobot(GameObject _bot, int _id)
    {
        _bot.GetComponent<RobotController>().playerId = _id;
        _bot.GetComponent<SpawnBehaviour>().Init(GetClosestAvailableTotem(transform.position));
        foreach(LightningBolt bolt in _bot.GetComponentsInChildren<LightningBolt>())
            bolt.emitter = totemsTransform[0];
        Camera.main.GetComponent<CameraController>().pois.Add(myRobots[_id].transform);
    }

    void Awake()
    {
        Debug.Log("GameManager singleton instance set.");
        s_Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        #region Gestion fin de la partie
        for (int i = 0; i < playerCount; i++)
        {
            if (myRobots[i].GetComponent<RobotGestionPoint>().getPoint() >= 100 && Time.timeScale != 0)
            {
                Time.timeScale = 0;
                GameObject.Find("UI").GetComponent<ShowPanels>().ShowWinPanel();

                if (playerCount < 4)
                {
                    GameObject.Find("TextForth").SetActive(false);
                }
                if (playerCount < 3)
                {
                    GameObject.Find("TextThird").SetActive(false);
                }
                int[] pointPlayer = new int[playerCount];

                for (int j = 0; j < playerCount; j++)
                {
                    pointPlayer[j] = myRobots[j].GetComponent<RobotGestionPoint>().getPoint();
                }

                int[] ladder = new int[playerCount];
                ladder[0] = i;
                pointPlayer[i] = -1;
                ladder[1] = 0;
                GameObject.Find("TextFirst").GetComponent<Text>().text += ladder[0] + 1;


                //On cherche le deuxième
                for (int j = 0; j < playerCount; j++)
                {
                    if (pointPlayer[j] > pointPlayer[ladder[1]])
                    {
                        ladder[1] = j;
                    }
                }
                pointPlayer[ladder[1]] = -1;
                GameObject.Find("TextSecond").GetComponent<Text>().text += ladder[1] + 1;
                //On cherche le troisième
                if (playerCount > 2)
                {
                    ladder[2] = 0;
                    for (int j = 0; j < playerCount; j++)
                    {
                        if (pointPlayer[j] > pointPlayer[ladder[2]])
                        {
                            ladder[2] = j;
                        }
                    }
                    pointPlayer[ladder[2]] = -1;
                    GameObject.Find("TextThird").GetComponent<Text>().text += ladder[2] + 1;
                }

                //On cherche le quatrième
                if (playerCount > 3)
                {

                    ladder[3] = 0;
                    for (int j = 0; j < playerCount; j++)
                    {
                        if (pointPlayer[j] > pointPlayer[ladder[3]])
                        {
                            ladder[3] = j;
                        }
                    }
                    pointPlayer[ladder[3]] = -1;
                    GameObject.Find("TextForth").GetComponent<Text>().text += ladder[3] + 1;
                }

            }
        }
        #endregion

        if (timeSpawnItem < 10)
        {
            timeSpawnItem += Time.deltaTime;
        }
        else
        {
            
            float r = Random.Range(2f, 10f);
            float teta = Random.Range(0, 360);
            Instantiate(prefabCaisses[Random.Range(0, prefabCaisses.Length)], new Vector3(r * Mathf.Cos(teta), 0.5f, r * Mathf.Sin(teta)), Quaternion.identity);
            timeSpawnItem = 0;
        }



        if (timerInverted > 10 && invertedControl)
        {
            timerInverted = 0;
            invertedControl = false;
            ThrowerInvertedControl = null;
        }
        if (invertedControl)
        {
            timerInverted += Time.deltaTime;
        }
    }

    public void resetGameController()
    {
        myRobots = null;
        Time.timeScale = 1;
    }

    public float lengthTotemRobot(GameObject myRobot)
    {
        return Vector3.Distance(totemsTransform[0].position, myRobot.transform.position);
    }

    public void setplayerCount(int _playerCount){
        this.playerCount = _playerCount;
    }

    public Totem GetClosestAvailableTotem(Vector3 position)
    {
        Totem closestTotem = null;
        for (int i = 0; i < spawns.Length; i++)
        {
            Totem totem = spawns[i].GetComponent<Totem>();
            if (!totem.occupied && (closestTotem == null || Vector3.Distance(totem.transform.position, position) < Vector3.Distance(closestTotem.transform.position, position)))
            {
                closestTotem = totem;
            }
        }
        return closestTotem;
    }
}
