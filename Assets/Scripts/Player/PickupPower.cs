using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PickupPower : MonoBehaviour
{
    [SerializeField]
    private Sprite m_icon;
    public Sprite Icon { get { return m_icon; } }

    public virtual void Activate(RobotGestionPoint _bot)
    {

    }

}