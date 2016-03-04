using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraControlBase : MonoBehaviour {


  [Range(-180f, 180f)] public float xMin = -80, xMax = 80;
  [Range(-180f, 180f)] public float yMin = -80, yMax = 80;
  [Range(0.11f, 1.0f)] public float damping = 1.0f;
	protected float x, y;
	protected float targetX, targetY;


  public void initialize()
  {
    Assert.IsTrue(xMin < xMax);
    Assert.IsTrue(yMin < yMax);
    Assert.IsTrue(damping > 0.1f);
  }

	public void toggleLock() {
		if(Cursor.lockState == CursorLockMode.None) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

  public float ClampAngle(float y, float min, float max)
  {
    if (y < -360) y += 360;
		if (y > 360) y -= 360;
		return Mathf.Clamp (y, min, max);
	}


}
