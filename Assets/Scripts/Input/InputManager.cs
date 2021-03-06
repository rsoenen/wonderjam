﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class InputManager : Singleton<InputManager> {

  //Singleton protection
  protected InputManager() { }

	void Awake ()
    {
        AutoMapControllers();
        DontDestroyOnLoad(gameObject);
	    }

    
  private List<PlayerInputs> _PlayerInputs = new List<PlayerInputs>();

  private const string Xbox360ControllerName = "Controller (XBOX 360 For Windows)";
  private const string Ps4ControllerName = "Wireless Controller";

  public static Color GetColorFromPlayer(int p)
  {
    switch (p)
    { 
      default:
      case 0:
        return new Color(1.0f, 0.0f, 0.0f);
      case 1:
        return new Color(0.0f, 162.0f / 255.0f, 232.0f / 255.0f);
      case 2:
        return new Color(0.0f, 193.0f / 255.0f, 0.0f);
      case 3:
        return new Color(1.0f, 1.0f, 0.0f);
    }
  }

  public void AutoMapControllers()
  {
    uint i = 0;
    //D'abord on tente de mettre tout sur les manettes
    foreach (string controller_name in Input.GetJoystickNames())
    {
            /*if (_PlayerInputs == null && havePilotMappingForThisController(controller_name))
            {
              _PlayerInputs.Add(createPilotMapping(controller_name, i));
              Debug.Log("Mapped " + controller_name + " to pilot");
            }
            else
            {
              Debug.Log("Can't find a mapping for controller: " + controller_name + " skipping it");
            }*/
            _PlayerInputs.Add(new PilotXbox360Controller());
            _PlayerInputs[(int)i].ControllerID = i;
            i++;
      if (i > 3)
      {
        //Don't support more than 4 controllers
        break;
      }

    }
    Assert.IsNotNull(_PlayerInputs);
  }

  public List<PlayerInputs> controllers
  {
    get
    {
      return _PlayerInputs;
    }
  }

    public PlayerInputs GetController(int _index)
    {
        if (_index < _PlayerInputs.Count)
            return _PlayerInputs[_index];

        return null;
    }

  public bool haveShooterMappingForThisController(string controller_name)
  {
    if (controller_name == Xbox360ControllerName)
    {
      return true;
    }
    return false;
  }

  public bool havePilotMappingForThisController(string controller_name)
  {
    if (controller_name == Xbox360ControllerName)
    {
      return true;
    }
    return false;
  }

  public PlayerInputs createPilotMapping(string controller_name, uint id)
  {
    PlayerInputs mapping = null;

    if (controller_name == Xbox360ControllerName)
    {
      mapping = new PilotXbox360Controller();
    }

    if (mapping != null)
    {
      mapping.ControllerID = id;
    }
    return mapping;
  }

}
