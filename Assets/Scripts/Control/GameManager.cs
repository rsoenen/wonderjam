using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public int numberPlayer;
    public GameObject prefabRobot;
    public GameObject prefabHUD;
    public Transform totem;

    public float stuntDeceleration, stuntDuration, stuntStrength;
    public float dashDuration, dashSpeed;

    // Use this for initialization
    void Start () {
        Vector3[] posSpawn = new Vector3[] { new Vector3(1, 0.51f, 1), new Vector3(-1, 0.51f, -1), new Vector3(-1, 0.51f, 1), new Vector3(1, 0.51f, -1) };
        for (int i = 0; i < numberPlayer; i++)
        {
            GameObject myRobot =(GameObject) Instantiate(prefabRobot, posSpawn[i], Quaternion.identity);
            myRobot.GetComponent<RobotController>().playerId = i;
            myRobot.GetComponentInChildren<LightningBolt>().emitter = totem;
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
	
	}

    public void setNumberPlayer(int _numberPlayer){
        this.numberPlayer = _numberPlayer;
    }
}
