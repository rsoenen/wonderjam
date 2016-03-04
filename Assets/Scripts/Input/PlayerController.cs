using UnityEngine;
using System.Collections;

public interface PlayerController
{
  /* Player/Controller ID */

  /// <summary>
  /// The ID of the player/controller
  /// Start from 0 (the joystick axis id is ID + 1)
  /// </summary>
  uint ControllerID { get; set; }

}