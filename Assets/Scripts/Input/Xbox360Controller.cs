using UnityEngine;
using System.Collections;
using System;


public class PilotXbox360Controller : PilotInputs
{
  private uint _id;
  private string _axisBasename;
  private string _axisEndname;

  float PilotInputs.Backward
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersL" + _axisEndname);
    }
  }

  float PilotInputs.CameraX
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "R_XAxis" + _axisEndname);
    }
  }

  float PilotInputs.CameraY
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "R_YAxis" + _axisEndname);
    }
  }

  uint PlayerController.ControllerID
  {
    get
    {
      return _id;
    }

    set
    {
      _id = value;
      _axisBasename = "Xbox360_";
      _axisEndname = "_" + (_id+1).ToString();
    }
  }

  float PilotInputs.Forward
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersR" + _axisEndname);
    }
  }


  float PilotInputs.Pitch
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "L_YAxis" + _axisEndname);
    }
  }

  bool PilotInputs.Turbo
  {
    get
    {
      return Input.GetButton(_axisBasename + "A" + _axisEndname);
    }
  }

  float PilotInputs.Yaw
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "L_XAxis" + _axisEndname);
    }
  }

  float PilotInputs.SwitchCamera
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "DPad_YAxis" + _axisEndname);
    }
  }
}


public class ShooterXbox360Controller : ShooterInputs
{
  private uint _id;
  private string _axisBasename;
  private string _axisEndname;

  float ShooterInputs.AimX
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "R_XAxis" + _axisEndname);
    }
  }

  float ShooterInputs.AimY
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "R_YAxis" + _axisEndname);
    }
  }

  uint PlayerController.ControllerID
  {
    get
    {
      return _id;
    }

    set
    {
      _id = value;
      _axisBasename = "Xbox360_";
      _axisEndname = "_" + (_id+1).ToString();
    }
  }

  bool ShooterInputs.FireLeftCanon
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersL" + _axisEndname)!=0;
    }
  }

  bool ShooterInputs.FireRightCanon
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersR" + _axisEndname)!=0;
    }
  }

}