using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupIcon : MonoBehaviour {

    public RobotController robot;
    public PickupPower currentPower;
    private Image img;
    // Use this for initialization
    void Start ()
    {
        robot = GetComponentInParent<PlayerUI>().robot;
        img = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        PickupPower newPower = robot.GetComponent<RobotGestionPoint>().Powerup;
        if (newPower != currentPower)
        {
            print("Changed Icon");
            currentPower = newPower;
            if (currentPower == null)
            {
                img.sprite = null;
                img.color = new Color(0, 0, 0, 0);
            }
            else
            {
                img.sprite = currentPower.Icon;
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
