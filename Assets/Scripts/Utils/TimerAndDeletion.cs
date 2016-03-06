using UnityEngine;
using System.Collections;

public class TimerAndDeletion : MonoBehaviour {

    [SerializeField]
    private float m_DestructionETA;
    private float m_timer;

    private bool m_running = true;

    public void Init(float _DestructionETA)
    {
        m_DestructionETA = _DestructionETA;
    }

    public void Enable(bool _enable)
    {
        if(_enable)
        {
            m_running = true;
        }
        else
        {
            m_timer = 0;
            m_running = false;
        }
    }

	// Update is called once per frame
	void Update () {
        if (!m_running)
            return;

        m_timer += Time.deltaTime;
        if(m_timer > m_DestructionETA)
        {
            BeepAndDestroy beepAndDestroy = gameObject.AddComponent<BeepAndDestroy>();
            beepAndDestroy.Init(5, 0.2f, 2.0f);
            Destroy(this);
        }
	
	}
}
