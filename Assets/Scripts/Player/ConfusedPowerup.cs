using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ConfusedPowerup : PickupPower
{
    public override void Activate(RobotGestionPoint _bot)
    {
        Debug.Log("Confused Powerup activated !");
    }
}
