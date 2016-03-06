using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ReduceLinkPower : PickupPower
{
    public override void Activate(RobotGestionPoint _bot)
    {
        GameManager.Instance().rangeReducers.Add(_bot.GetComponent<RobotController>());
    }
}
