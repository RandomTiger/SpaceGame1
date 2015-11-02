using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class ThrusterScalarInput : MonoBehaviour, IThrusterScalar
{
    public InputMap mButton = InputMap.LTrigger;
    private PlayerController mPlayerController;

    void Start()
    {
        mPlayerController = GetComponent<PlayerController>();
    }

    public float GetValue()
    {
        return InputSystem.GetController(mPlayerController.m_PlayerIndex).Get(mButton);
    }
}