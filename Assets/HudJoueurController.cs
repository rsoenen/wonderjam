using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudJoueurController : MonoBehaviour {

    private RobotGestionPoint m_pointController = null;

    [SerializeField]
    private Text m_PointLabel;

    [SerializeField]
    private RectTransform m_PointRect;

    [SerializeField]
    private RectTransform m_BarrePointMax;

    [SerializeField]
    private Image m_PowerupIcon;

    [SerializeField]
    private Text m_killsText;


    public void Init(RobotGestionPoint _pointController)
    {
        if (m_pointController != null)
            UnsubscribeFromCurrentController();

        m_pointController = _pointController;
        SubscribeToCurrentController();
    }

    private void UnsubscribeFromCurrentController()
    {
        m_pointController.PointChanged -= OnPointChanged;
        m_pointController.PowerupChanged -= OnPowerupChanged;
    }

    private void SubscribeToCurrentController()
    {
        m_pointController.PointChanged += OnPointChanged;
        m_pointController.PowerupChanged += OnPowerupChanged;
    }

	void OnPointChanged(int _old, int _new)
    {
        m_PointLabel.text = _new + "/20";
        m_PointRect.sizeDelta = new Vector2(95 * (20 - _new) / 20, 15);
        m_PointRect.position = new Vector3(m_BarrePointMax.position.x + _new/ 2, m_BarrePointMax.position.y, 0);
    }

    void OnPowerupChanged(PickupPower _powerup)
    {
        if (_powerup != null)
        {

            m_PowerupIcon.sprite = _powerup.Icon;
            Debug.Log(m_PowerupIcon);
            
            //m_Background.color = new Color(0, 0, 0, 190 / 255);
            m_PowerupIcon.color = Color.white;
        }
        else
        {
            m_PowerupIcon.sprite = null;
            Debug.Log("Coucou1");
            m_PowerupIcon.color = Color.clear;
        }
    }
}
