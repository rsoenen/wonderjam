using UnityEngine;
using System.Collections;

public class PlayerUIPopulator : MonoBehaviour
{
    public Transform UIPrefab;

	// Use this for initialization
	public void Populate(RobotController[] robots)
    {
	    foreach(RobotController robot in robots)
        {
            Transform instance = Instantiate<Transform>(UIPrefab);
            instance.GetComponent<PlayerUI>().Init(robot);
            instance.parent = transform;
            print("INSTANCE");
        }
	}
}
