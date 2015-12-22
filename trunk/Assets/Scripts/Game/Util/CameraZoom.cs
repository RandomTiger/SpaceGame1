using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    private PlayerController m_Controller;
    private Camera m_Camera;
    private float m_ZoomFactor = 20.0f;
    private float m_Min = 3.0f;
    private float m_Max = 100.0f;

    private float m_DefaultSize;
    private float m_UnitInterval;

    // Use this for initialization
    void Start()
    {
        m_Controller    = GetComponent<PlayerController>();
        m_Camera        = GetComponent<Camera>();
        m_DefaultSize   = m_Camera.orthographicSize;
        m_UnitInterval  = CamSizeToUnit(m_DefaultSize);

        m_Camera.orthographicSize = m_DefaultSize;
    }

    void Update()
    {
        bool reset = InputSystem.GetController(m_Controller.PlayerIndex).IsPressed(InputMap.RThumstickDown);
        if (reset)
        {
            m_UnitInterval = CamSizeToUnit(m_DefaultSize);
            SetCameraZoom(m_UnitInterval);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = InputSystem.GetController(m_Controller.PlayerIndex).Get(InputMap.AxisRightY);

        m_UnitInterval += input * Time.fixedDeltaTime;
        m_UnitInterval = Mathf.Clamp(m_UnitInterval, 0.0f, 1.0f);

        SetCameraZoom(m_UnitInterval);
    }

    void SetCameraZoom(float unitInterval)
    {
        m_Camera.orthographicSize = UnitToCamSize(unitInterval);
    }

    float UnitToCamSize(float unitInterval)
    {
        return m_Min + Mathf.Sin(unitInterval * Mathf.PI / 2.0f) * (m_Max - m_Min);
    }

    float CamSizeToUnit(float camSize)
    {
        //return Mathf.Asin((camSize - m_Min) / m_Max);
        return Mathf.Asin((camSize - m_Min) / (m_Max - m_Min)) / (Mathf.PI / 2.0f);
    }
}
