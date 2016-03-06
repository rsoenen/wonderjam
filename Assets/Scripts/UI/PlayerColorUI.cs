using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerColorUI : MonoBehaviour
{
    public RobotController robot;
    // Use this for initialization
    void Start()
    {
        robot = GetComponentInParent<PlayerUI>().robot;
        if (GetComponent<Text>() != null)
            GetComponent<Text>().color = InputManager.GetColorFromPlayer(robot.playerId);
        if (GetComponent<Image>() != null)
            GetComponent<Image>().color = InputManager.GetColorFromPlayer(robot.playerId);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
