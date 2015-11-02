using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class AxisInput : MonoBehaviour {

	public int inputDevice = 0;
	public InputMap axisX = InputMap.AxisLeftX;
	public InputMap axisY = InputMap.AxisLeftY;

    public float x;
	public float y;

    private PlayerController m_Controller;

    // Use this for initialization
    void Start () {
        m_Controller = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        x = InputSystem.GetController(m_Controller.m_PlayerIndex).Get(axisX);
		y = InputSystem.GetController(m_Controller.m_PlayerIndex).Get(axisY);
	}
}
