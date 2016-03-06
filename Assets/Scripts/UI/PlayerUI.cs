using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {
    [SerializeField]
    private Text energyLabel, playerNameLabel, scoreLabel;
    public RobotController robot;

    public void Init(RobotController robot)
    {
        this.robot = robot;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
