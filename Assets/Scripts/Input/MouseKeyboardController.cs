using UnityEngine;
using System.Collections;
using System;

public class MouseShooterContoller : ShooterInputs
{
  float ShooterInputs.AimX
  {
    get
    {
      return Mathf.Clamp(Input.GetAxisRaw("MouseX"),-1,1);
    }
  }

  float ShooterInputs.AimY
  {
    get
    {
      return Mathf.Clamp(-1*Input.GetAxisRaw("MouseY"),-1,1);
    }
  }

  uint PlayerController.ControllerID
  {
    get
    {
      return 0; //No ID for mouse
    }

    set
    {
    }
  }

  bool ShooterInputs.FireLeftCanon
  {
    get
    {
      return Input.GetMouseButton(0);
    }
  }

  bool ShooterInputs.FireRightCanon
  {
    get
    {
      return Input.GetMouseButton(1);
    }
  }
}

public class KeyboardPilotController : PilotInputs
{
  float PilotInputs.Backward
  {
    get
    {
      if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
      {
        return 1;  
      }
      return 0;
    }
  }

  float PilotInputs.CameraX
  {
    get
    {
      if (Input.GetKey(KeyCode.Keypad4))
      {
        return -0.5f;
      }
      else if (Input.GetKey(KeyCode.Keypad6))
      {
        return 0.5f;
      }
      return 0;
    }
  }

  float PilotInputs.CameraY
  {
    get
    {
      if (Input.GetKey(KeyCode.Keypad8))
      {
        return -1;
      }
      else if (Input.GetKey(KeyCode.Keypad2))
      {
        return 1;
      }
      return 0;

    }
  }

  uint PlayerController.ControllerID
  {
    get
    {
      //No ID for keyboard
      return 0;
    }

    set
    {
    }
  }

  float PilotInputs.Forward
  {
    get
    {
      if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
      {
        return 1;
      }
      return 0;
    }
  }

  float PilotInputs.Pitch
  {
    get
    {
      if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
      {
        return -1;
      }
      else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
      {
        return 1;
      }
      return 0;
    }
  }

  float PilotInputs.SwitchCamera
  {
    get
    {
      if (Input.GetKey(KeyCode.F1))
      {
        return 1;
      }
      else if (Input.GetKey(KeyCode.F2))
      {
        return -1;
      }
      return 0;
    }
  }

  bool PilotInputs.Turbo
  {
    get
    {
      return Input.GetKey(KeyCode.Space);
    }
  }

  float PilotInputs.Yaw
  {
    get
    {
      if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
      {
        return -1;
      }
      else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
      {
        return 1;
      }
      return 0;
    }
  }
}

public class MouseAndKeyboardPilotController : PilotInputs
{
  PilotInputs baseController;

  public MouseAndKeyboardPilotController()
  {
    baseController = new KeyboardPilotController();
  }

  float PilotInputs.Backward
  {
    get
    {
      return baseController.Backward;
    }
  }

  float PilotInputs.CameraX
  {
    get
    {
      return Mathf.Clamp(Input.GetAxisRaw("MouseX"),-1,1);
    }
  }

  float PilotInputs.CameraY
  {
    get
    {
      return Mathf.Clamp(-1*Input.GetAxisRaw("MouseY"),-1,1);
    }
  }

  uint PlayerController.ControllerID
  {
    get
    {
      return 0;
    }

    set
    {
    }
  }

  float PilotInputs.Forward
  {
    get
    {
      return baseController.Forward;
    }
  }

  float PilotInputs.Pitch
  {
    get
    {
      return baseController.Pitch;
    }
  }

  float PilotInputs.SwitchCamera
  {
    get
    {
      return baseController.SwitchCamera;
    }
  }

  bool PilotInputs.Turbo
  {
    get
    {
      return baseController.Turbo;
    }
  }

  float PilotInputs.Yaw
  {
    get
    {
      return baseController.Yaw;
    }
  }
}