﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField] private int m_PlayerIndex;
    public int PlayerIndex { get { return m_PlayerIndex; } }

    public Controller m_Controller;

	// Use this for initialization
	void Start ()
    {
        m_Controller = InputSystem.GetController(m_PlayerIndex);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
