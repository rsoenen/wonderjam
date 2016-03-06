using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager s_Instance = null;
    public static GameManager Instance() { return s_Instance; }

    public GameObject prefabRobot;
    public HudController prefabHUD;
    public GameObject[] prefabCaisses;
    public GameObject[] prefabThrowables;
    private Transform[] totemsTransform;
    private int playerCount;
    private float timeSpawnItem;
    private float timeGlobal;


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
    public float laserShakeFactor;
    private GameObject[] myRobots;
    private RobotController[] robots;

    private GameObject[] spawns;

    public List<EnergyTower> towers = new List<EnergyTower>();

    public float slowDownDuration, slowDownDeathRatio;

    public float spawnBlinkFreq, spawnBlinkDuration;

    public Transform playerHUDPrefab;

    public PlayerUIPopulator UIPopulator;

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
        timeGlobal = 0f;
        timeSpawnItem = 0f;
        playerCount = InputManager.Instance.controllers.Count;
        myRobots = new GameObject[playerCount];
        robots = new RobotController[playerCount];

        GameObject[] totems =  GameObject.FindGameObjectsWithTag("Totem");
        totemsTransform = new Transform[totems.Length];
        for (int i = 0; i < totems.Length; i++)
        {
            totemsTransform[i] = totems[i].transform;
			totemsTransform[i].GetComponent<EnergyTower>().spawnerCount = playerCount;
			totemsTransform[i].GetComponent<EnergyTower>().SetupSpawns();

        }

        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        for (int i = 0; i < playerCount; i++)
        {
            myRobots[i] = (GameObject)Instantiate(prefabRobot, spawns[i].transform.position, Quaternion.identity);
            robots[i] = myRobots[i].GetComponent<RobotController>();
            SetupRobot(myRobots[i], i);
        }

        GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<PlayerUIPopulator>().Populate(robots);
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
        timeGlobal += Time.deltaTime;
        #region Gestion fin de la partie
        
        if (Time.timeScale != 0 && timeGlobal > 180)
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


            int[] ladder = new int[playerCount];
            ladder[0] = 0;
            int id = 0;
            for (int j = 0; j < playerCount; j++)
            {
                pointPlayer[j] = myRobots[j].GetComponent<RobotGestionPoint>().getPoint();
                if (pointPlayer[j]>pointPlayer[ladder[0]]){
                    ladder[0]=j;
                    id=j;
                }
            }
               
            pointPlayer[id] = -1;
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
        #endregion

        if (timeSpawnItem < 10)
        {
            timeSpawnItem += Time.deltaTime;
        }
        else
        {
            float r = Random.Range(2f, 10f);
            float teta = Random.Range(0, 360);
            GameObject prefab = null;
            prefab = Random.value < 0.5f ? prefabCaisses[Random.Range(0, prefabCaisses.Length)] : prefabThrowables[Random.Range(0, prefabThrowables.Length)];

            Instantiate(prefab, new Vector3(r * Mathf.Cos(teta), 10.0f, r * Mathf.Sin(teta)), Quaternion.identity);
            timeSpawnItem = 0;
        }

        
        int tempsRestant = 2 * 60 - (int)timeGlobal;
        GameObject.Find("TextTimeRemaining").GetComponent<Text>().text = "TEMPS RESTANT : " + tempsRestant;

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
        timeGlobal = 0;
        Time.timeScale = 1;
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
