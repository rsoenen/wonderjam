using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public int numberPlayer;
    public GameObject prefabRobot;
    public GameObject prefabHUD;
    private Transform totem;


    public bool invertedControl;
    public float stuntDeceleration, stuntDuration, stuntStrength, stuntControlSpeed, floorY;
    public float dashDuration, dashSpeed, dashContactSlow;
    public float playerMaxSpeed, playerAcceleration, playerDeceleration;
    public float playerRotSpeed;
    private GameObject[] myRobots;

    // Use this for initialization
    void Start () {
        invertedControl = false;
        myRobots = new GameObject[numberPlayer];
        totem = GameObject.FindGameObjectWithTag("Totem").transform;
        Vector3[] posSpawn = new Vector3[] { new Vector3(1, 0.51f, 1), new Vector3(-1, 0.51f, -1), new Vector3(-1, 0.51f, 1), new Vector3(1, 0.51f, -1) };
        for (int i = 0; i < numberPlayer; i++)
        {
            myRobots[i] = (GameObject)Instantiate(prefabRobot, posSpawn[i], Quaternion.identity);
            myRobots[i].GetComponent<RobotController>().playerId = i;
            myRobots[i].GetComponentInChildren<LightningBolt>().emitter = totem;
			//Camera.main.GetComponent<CameraController>().pois.Add(myRobots[i].transform);

        }
        Instantiate(prefabHUD);
        if (numberPlayer < 4)
        {
            GameObject.Find("HUDJoueur4").SetActive(false);
        }
        if (numberPlayer < 3)
        {
            GameObject.Find("HUDJoueur3").SetActive(false);
        }
        if (numberPlayer < 2)
        {
            GameObject.Find("HUDJoueur2").SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberPlayer; i++)
        {
            if (myRobots[i].GetComponent<RobotGestionPoint>().getPoint() >= 100 && Time.timeScale != 0)
            {


                Time.timeScale = 0;
                GameObject.Find("UI").GetComponent<ShowPanels>().ShowWinPanel();

                if (numberPlayer < 4)
                {
                    GameObject.Find("TextForth").SetActive(false);
                }
                if (numberPlayer < 3)
                {
                    GameObject.Find("TextThird").SetActive(false);
                }
                int[] pointPlayer = new int[numberPlayer];

                for (int j = 0; j < numberPlayer; j++)
                {
                    pointPlayer[j] = myRobots[j].GetComponent<RobotGestionPoint>().getPoint();
                }

                int[] ladder = new int[numberPlayer];
                ladder[0] = i;
                pointPlayer[i] = -1;
                ladder[1] = 0;
                GameObject.Find("TextFirst").GetComponent<Text>().text += ladder[0] + 1;


                //On cherche le deuxième
                for (int j = 0; j < numberPlayer; j++)
                {
                    if (pointPlayer[j] > pointPlayer[ladder[1]])
                    {
                        ladder[1] = j;
                    }
                }
                pointPlayer[ladder[1]] = -1;
                GameObject.Find("TextSecond").GetComponent<Text>().text += ladder[1] + 1;
                //On cherche le troisième
                if (numberPlayer > 2)
                {
                    ladder[2] = 0;
                    for (int j = 0; j < numberPlayer; j++)
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
                if (numberPlayer > 3)
                {

                    ladder[3] = 0;
                    for (int j = 0; j < numberPlayer; j++)
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

    public void setNumberPlayer(int _numberPlayer){
        this.numberPlayer = _numberPlayer;
    }
}
