using UnityEngine;
using System.Collections;

public class scriptCaisse : MonoBehaviour {

    [SerializeField]
    private PickupPower m_content;
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, Time.deltaTime * 100, 0, Space.World);
	}

    public void Consume(RobotController _consumer)
    {
        if(m_content != null)
            _consumer.GetComponent<RobotGestionPoint>().Powerup = m_content;
    }
}
