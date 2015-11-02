using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Controller
{
    private int m_ControllerIndex;
    private const float mThreshold = 0.1f;
    private const float mOneMinisThreshold = 1.0f - mThreshold;
    private float [] mPreviousState = new float [Enum.GetNames(typeof(InputMap)).Length];

    public Controller(int index)
    {
        m_ControllerIndex = index;
    }

    public float Get(InputMap button)
    {
        int index = m_ControllerIndex + 1;

        switch (button)
        {
            case InputMap.AxisLeftX: return UnityEngine.Input.GetAxisRaw("L_XAxis_" + index);
            case InputMap.AxisLeftY: return -UnityEngine.Input.GetAxisRaw("L_YAxis_" + index);
            case InputMap.AxisRightX: return UnityEngine.Input.GetAxisRaw("R_XAxis_" + index);
            case InputMap.AxisRightY: return -UnityEngine.Input.GetAxisRaw("R_YAxis_" + index);

            case InputMap.LTrigger: return UnityEngine.Input.GetAxisRaw("TriggersL_" + index);
            case InputMap.RTrigger: return UnityEngine.Input.GetAxisRaw("TriggersR_" + index);

            case InputMap.LShoulder: return UnityEngine.Input.GetAxisRaw("LB_" + index);
            case InputMap.RShoulder: return UnityEngine.Input.GetAxisRaw("RB_" + index);

            case InputMap.A: return UnityEngine.Input.GetAxisRaw("A_" + index);
            case InputMap.B: return UnityEngine.Input.GetAxisRaw("B_" + index);
            case InputMap.X: return UnityEngine.Input.GetAxisRaw("X_" + index);
            case InputMap.Y: return UnityEngine.Input.GetAxisRaw("Y_" + index);

            case InputMap.Back: return UnityEngine.Input.GetAxisRaw("Back_" + index);
            case InputMap.Start: return UnityEngine.Input.GetAxisRaw("Start_" + index);

            case InputMap.LThumstickDown: return UnityEngine.Input.GetAxisRaw("LS_" + index);
            case InputMap.RThumstickDown: return UnityEngine.Input.GetAxisRaw("RS_" + index);

            case InputMap.DPadLeft: return -Mathf.Min(0, UnityEngine.Input.GetAxisRaw("DPad_XAxis_" + index));
            case InputMap.DPadRight: return Mathf.Max(0, UnityEngine.Input.GetAxisRaw("DPad_XAxis_" + index));
            case InputMap.DPadUp: return Mathf.Max(0, UnityEngine.Input.GetAxisRaw("DPad_YAxis_" + index));
            case InputMap.DPadDown: return -Mathf.Min(0, UnityEngine.Input.GetAxisRaw("DPad_YAxis_" + index));
        }

        return 0.0f;
    }

    public bool IsPressed(InputMap button)
    {
        return Get(button) > mOneMinisThreshold;
    }

    public bool IsReleased(InputMap button)
    {
        return Get(button) < mThreshold;
    }

    // Dont query from FixedUpdate
    public bool IsJustReleased(InputMap button)
    {
        return (Get(button) < mThreshold) && (mPreviousState[(int) button] > mOneMinisThreshold);
    }

    public void Update()
    {
        foreach(InputMap button in Enum.GetValues(typeof(InputMap)))
        {
            mPreviousState[(int) button] = Get(button);
        }
    }
}
