using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public GameObject prefabRobot;
    public GameObject prefabHUD;
    public GameObject prefabCaisse;
    private Transform totem;
    private int playerCount;
    private float timeSpawnItem;


    public bool invertedControl;
    public float stuntDeceleration, stuntDuration, stuntStrength, stuntControlSpeed, floorY;
    public float dashDuration, dashSpeed, dashContactSlow;
    public float playerMaxSpeed, playerAcceleration, playerDeceleration;
    public float playerRotSpeed;
    public float deathDuration;
    public float gravity;
    public float deathHeight;
    private GameObject[] myRobots;

    // Use this for initialization
    void Start () {
        timeSpawnItem = 0f;
        invertedControl = false;
        playerCount = InputManager.Instance.controllers.Count;
        myRobots = new GameObject[playerCount];
        totem = GameObject.FindGameObjectWithTag("Totem").transform;
        Vector3[] posSpawn = new Vector3[] { new Vector3(1, 0.51f, 1), new Vector3(-1, 0.51f, -1), new Vector3(-1, 0.51f, 1), new Vector3(1, 0.51f, -1) };
        for (int i = 0; i < playerCount; i++)
        {
            myRobots[i] = (GameObject)Instantiate(prefabRobot, posSpawn[i], Quaternion.identity);
            myRobots[i].GetComponent<RobotController>().playerId = i;
            myRobots[i].GetComponent<SpawnBehaviour>().Init(GetClosestAvailableTotem(transform.position));
            myRobots[i].GetComponentInChildren<LightningBolt>().emitter = totem;
            //Camera.main.GetComponent<CameraController>().pois.Add(myRobots[i].transform);
        }
        Instantiate(prefabHUD);
        if (playerCount < 4)
        {
            GameObject.Find("HUDJoueur4").SetActive(false);
        }
        if (playerCount < 3)
        {
            GameObject.Find("HUDJoueur3").SetActive(false);
        }
        if (playerCount < 2)
        {
            GameObject.Find("HUDJoueur2").SetActive(false);
        }
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
                        Debug.Log(pointPlayer[j]);
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
            int signeX = 1, signeZ = 1;
            if (Random.Range(0, 2)==0)
            {
                signeX = -1;
            }
            if (Random.Range(0, 2) == 0)
            {
                signeZ = -1;
            }

            Instantiate(prefabCaisse, new Vector3(signeX * Random.Range(2f, 9.5f), 0.5f, signeZ * Random.Range(2f, 9.5f)), Quaternion.identity);
            timeSpawnItem = 0;
        }
    }
    public void resetGameController()
    {
        myRobots = null;
        Time.timeScale = 1;
    }
    public float lengthTotemRobot(GameObject myRobot)
    {
        return Vector3.Distance(totem.position, myRobot.transform.position);
    }

    public void setplayerCount(int _playerCount){
        this.playerCount = _playerCount;
    }

    public Totem GetClosestAvailableTotem(Vector3 position)
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawn");
        Totem closestTotem = null;
        for (int i = 0; i < spawners.Length; i++)
        {
            Totem totem = spawners[i].GetComponent<Totem>();
            if (!totem.occupied && (closestTotem == null || Vector3.Distance(totem.transform.position, position) < Vector3.Distance(closestTotem.transform.position, position)))
            {
                closestTotem = totem;
            }
        }
        return closestTotem;
    }
}
