using UnityEngine;
using System.Collections.Generic;

public class ShieldInput : MonoBehaviour, IShieldBool
{
    public InputMap mButton = InputMap.DPadUp;
    private PlayerController mPlayerController;

    void Start()
    {
        mPlayerController = GetComponent<ColliderQueryChild>().GetRoot().GetComponent<PlayerController>();
    }

    public bool GetRequest()
    {
        return InputSystem.GetController(mPlayerController.m_PlayerIndex).IsJustReleased(mButton);
    }
}