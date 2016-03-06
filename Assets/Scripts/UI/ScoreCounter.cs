using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

    public RobotController robot;
    public PickupPower currentPower;
    private Text text;
    // Use this for initialization
    void Start()
    {
        robot = GetComponentInParent<PlayerUI>().robot;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        text.text = robot.getKillsCount() + "";
	}
}
