using UnityEngine;
using System.Collections;

public class TimerAndDeletion : MonoBehaviour {

    [SerializeField]
    private float m_DestructionETA;
    private float m_timer;

	// Update is called once per frame
	void Update () {

        m_timer += Time.deltaTime;
        if(m_timer > m_DestructionETA)
        {
            BeepAndDestroy beepAndDestroy = gameObject.AddComponent<BeepAndDestroy>();
            beepAndDestroy.Init(5, 0.2f, 2.0f);
            Destroy(this);
        }
	
	}
}
