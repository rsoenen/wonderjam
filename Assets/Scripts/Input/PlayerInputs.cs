using UnityEngine;
using System.Collections;

public interface PlayerInputs : PlayerController
{
    /* AXIS */
  /// <summary>
  /// Yaw control range [-1,1] 
  /// </summary>
  float Yaw { get; }

  /// <summary>
  /// Pitch control range [-1,1]
  /// </summary>
  float Pitch { get; }

  /// <summary>
  /// Accelerate with thrusters range [0,1]
  /// </summary>
  float Forward { get; }

  /// <summary>
  /// Deccelerate with thrusters range [0,1]
  /// </summary>
  float Backward { get; }

  /// <summary>
  /// Control the pilot camera (interior/exterior)
  /// Horizontal
  /// </summary>
  float CameraX { get; }

  /// <summary>
  /// Control the pilot camera (interior/exterior)
  /// Vertical
  /// </summary>
  float CameraY { get; }
  
  /// <summary>
  /// Switch between (interior/exterior camera) range [-1,1]
  ///  1 Interior
  /// -1 Exterior
  /// </summary>
  float SwitchCamera { get; }


  /* BUTTONS */
  
  /// <summary>
  /// Accelerate with Nozzle
  /// </summary>
  bool Turbo { get; }

    // The X Button
    bool X { get; }

    // The Y Button
    bool Y { get; }

}
