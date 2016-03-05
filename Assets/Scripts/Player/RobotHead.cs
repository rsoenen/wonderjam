using UnityEngine;
using System.Collections;

public class RobotHead : MonoBehaviour
{

    RobotController robot;
    // Use this for initialization
    void Start()
    {
        robot = GetComponentInParent<RobotController>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Inverse(transform.parent.localRotation) * Quaternion.LookRotation(robot.lookDirection);

        transform.localRotation = Quaternion.Euler(new Vector3(270, rotation.eulerAngles.y, 0));
    }

}
