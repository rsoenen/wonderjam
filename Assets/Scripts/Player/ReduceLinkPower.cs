using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ReduceLinkPower : PickupPower
{
    public override void Activate(RobotGestionPoint _bot)
    {
        GameObject totem = GameObject.FindGameObjectWithTag("Totem");
        totem.GetComponent<EnergyTower>().reduceArc(_bot.getOwner());
    }
}
