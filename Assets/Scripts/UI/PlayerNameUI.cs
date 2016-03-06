using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameUI : MonoBehaviour {

    public RobotController robot;
    public PickupPower currentPower;
    private Text text;
    // Use this for initialization
    void Start()
    {
        robot = GetComponentInParent<PlayerUI>().robot;
        text = GetComponent<Text>();
        text.text = "Player " + robot.playerId; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
