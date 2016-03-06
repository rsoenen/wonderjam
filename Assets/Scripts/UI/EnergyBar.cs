using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    RectTransform rect;
    public RobotController robot;
    public Text text;

	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
        robot = GetComponentInParent<PlayerUI>().robot;
	}
	
	// Update is called once per frame
	void Update ()
    {
        rect.localScale = new Vector3((20 - robot.GetComponent<RobotGestionPoint>().getPoint()) /20f, 1, 1);
        text.text = robot.GetComponent<RobotGestionPoint>().getPoint() + "/20";

	}

}
