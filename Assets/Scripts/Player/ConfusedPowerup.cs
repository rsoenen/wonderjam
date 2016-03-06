using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ConfusedPowerup : PickupPower
{
    public override void Activate(RobotGestionPoint _bot)
    {
        GameObject thrower =_bot.getOwner();
        GameObject gameController =GameObject.FindGameObjectWithTag("Constants");
        gameController.GetComponent<GameManager>().ThrowerInvertedControl = thrower;
        gameController.GetComponent<GameManager>().invertedControl = true;
    }
}
