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
    }

    private void SubscribeToCurrentController()
    {
        m_pointController.PointChanged += OnPointChanged;
    }

	void OnPointChanged(int _old, int _new)
    {
        m_PointLabel.text = _new + "/100";
        m_PointRect.sizeDelta = new Vector2(95 * (100 - _new) / 100, 15);
        m_PointRect.position = new Vector3(m_BarrePointMax.position.x + _new/ 2, m_BarrePointMax.position.y, 0);
    }
}
