﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PowerUpPower : PickupPower
{
    public override void Activate(RobotGestionPoint _bot)
    {
        _bot.reducePoint(-10);
    }
}
