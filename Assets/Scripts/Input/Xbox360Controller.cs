using UnityEngine;
using System.Collections;
using System;


public class PilotXbox360Controller : PlayerInputs
{
  private uint _id;
  private string _axisBasename;
  private string _axisEndname;

  float PlayerInputs.Backward
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersL" + _axisEndname);
    }
  }

  float PlayerInputs.CameraX
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "R_XAxis" + _axisEndname);
    }
  }

  float PlayerInputs.CameraY
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

  float PlayerInputs.Forward
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "TriggersR" + _axisEndname);
    }
  }


  float PlayerInputs.Pitch
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "L_YAxis" + _axisEndname);
    }
  }

  bool PlayerInputs.Turbo
  {
    get
    {
      return Input.GetButtonDown(_axisBasename + "A" + _axisEndname);
    }
  }

    bool PlayerInputs.X
    {
        get
        {
            return Input.GetButtonDown(_axisBasename + "X" + _axisEndname);
        }
    }

    bool PlayerInputs.Y
    {
        get
        {
            return Input.GetButtonDown(_axisBasename + "Y" + _axisEndname);
        }
    }

    bool PlayerInputs.B
    {
        get
        {
            return Input.GetButtonDown(_axisBasename + "B" + _axisEndname);
        }
    }

  float PlayerInputs.Yaw
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "L_XAxis" + _axisEndname);
    }
  }

  float PlayerInputs.SwitchCamera
  {
    get
    {
       return Input.GetAxisRaw(_axisBasename + "DPad_YAxis" + _axisEndname);
    }
  }
}