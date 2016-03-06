using UnityEngine;
using System.Collections;

public class FallEmulation : MonoBehaviour {

    [SerializeField]
    float m_MaxFallSpeed = 5.0f;

    [SerializeField]
    float m_DistFromGround = 1.0f;

	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_DistFromGround, LayerMask.GetMask("Floor")))
            Destroy(this);

        transform.position += Vector3.down * m_MaxFallSpeed * Time.deltaTime;
	}
}
