using UnityEngine;
using System.Collections;

// Collect x and y from axis
[RequireComponent(typeof(PlayerController))]
public class AxisInput : MonoBehaviour {

    public int inputDeviceIndex = 0;
    [Header("Axis")]
    public InputMap axisX = InputMap.AxisLeftX;
	public InputMap axisY = InputMap.AxisLeftY;

    [Header("Output values")]
    [Tooltip("Just for testing")]
    [ShowOnly] public float x;
    [Tooltip("Just for testing")]
    [ShowOnly] public float y;

    private PlayerController m_Controller;

    // Use this for initialization
    void Start () {
        m_Controller = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        x = InputSystem.GetController(m_Controller.PlayerIndex).Get(axisX);
		y = InputSystem.GetController(m_Controller.PlayerIndex).Get(axisY);
	}
}
