using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class InputManager : Singleton<InputManager> {

  //Singleton protection
  protected InputManager() { }

	void Awake ()
  {
    AutoMapControllers();

	}


  private ShooterInputs _shooterInputs = null;
  private PilotInputs _pilotInputs = null;

  private const string Xbox360ControllerName = "Controller (XBOX 360 For Windows)";
  private const string Ps4ControllerName = "Wireless Controller";


  public void AutoMapControllers()
  {
    _shooterInputs = null;
    _pilotInputs = null;

    uint i = 0;
    //D'abord on tente de mettre tout sur les manettes
    foreach (string controller_name in Input.GetJoystickNames())
    {
      if (_pilotInputs == null && havePilotMappingForThisController(controller_name))
      {
        _pilotInputs = createPilotMapping(controller_name, i);
        Debug.Log("Mapped " + controller_name + " to pilot");
      }
      else if (_shooterInputs == null && haveShooterMappingForThisController(controller_name))
      {
        _shooterInputs = createShooterMapping(controller_name, i);
        Debug.Log("Mapped " + controller_name + " to shooter");
      }
      else if (_shooterInputs != null && _pilotInputs != null)
      {
        Debug.Log("More controllers than needed: " + controller_name);
      }
      else
      {
        Debug.Log("Can't find a mapping for controller: " + controller_name + " skipping it");
      }

      i++;
      if (i > 3)
      {
        //Don't support more than 4 controllers
        break;
      }

    }


    //Ensuite le clavier
    if (_shooterInputs == null && _pilotInputs == null)
    {
      //aucune manette: pilotage clavier, shooter souris
      Debug.Log("Mapped Mouse to shooter");
      Debug.Log("Mapped Keyboard to pilot");
      _pilotInputs = new KeyboardPilotController();
      _shooterInputs = new MouseShooterContoller();
    }
    else if (_shooterInputs == null)
    {
      //Controle uniquement a la souris
      _shooterInputs = new MouseShooterContoller();
      Debug.Log("Mapped Mouse to shooter");

    }
    else if (_pilotInputs == null)
    {
      //controle du pilotage clavier + souris
      _pilotInputs = new MouseAndKeyboardPilotController();
      Debug.Log("Mapped Mouse & Keyboard to pilot");

    }

    Assert.IsNotNull(_pilotInputs);
    Assert.IsNotNull(_shooterInputs);

  }

  public ShooterInputs shooter
  {
    get
    {
      return _shooterInputs;
    }
  }

  public PilotInputs pilot
  {
    get
    {
      return _pilotInputs;
    }
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

  public ShooterInputs createShooterMapping(string controller_name, uint id)
  {
    ShooterInputs mapping = null;

    if (controller_name == Xbox360ControllerName)
    {
      mapping = new ShooterXbox360Controller();
    }

    if (mapping != null)
    {
      mapping.ControllerID = id;
    }
    return mapping;
  }

  public PilotInputs createPilotMapping(string controller_name, uint id)
  {
    PilotInputs mapping = null;

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
