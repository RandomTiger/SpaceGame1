using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Controller
{
    private int m_ControllerIndex;
    private float mThreshold = 1.0f;

    public Controller(int index)
    {
        m_ControllerIndex = index;
    }

    public float Get(GenericPad button)
    {
        int index = m_ControllerIndex + 1;

        switch (button)
        {
            case GenericPad.AxisLeftX: return  UnityEngine.Input.GetAxisRaw("L_XAxis_" + index);
            case GenericPad.AxisLeftY: return -UnityEngine.Input.GetAxisRaw("L_YAxis_" + index);
            case GenericPad.AxisRightX: return  UnityEngine.Input.GetAxisRaw("R_XAxis_" + index);
            case GenericPad.AxisRightY: return -UnityEngine.Input.GetAxisRaw("R_YAxis_" + index);

            case GenericPad.LTrigger: return UnityEngine.Input.GetAxisRaw("TriggersL_" + index);
            case GenericPad.RTrigger: return UnityEngine.Input.GetAxisRaw("TriggersR_" + index);

            case GenericPad.LShoulder: return UnityEngine.Input.GetAxisRaw("LB_" + index);
            case GenericPad.RShoulder: return UnityEngine.Input.GetAxisRaw("RB_" + index);

            case GenericPad.A: return UnityEngine.Input.GetAxisRaw("A_" + index);
            case GenericPad.B: return UnityEngine.Input.GetAxisRaw("B_" + index);
            case GenericPad.X: return UnityEngine.Input.GetAxisRaw("X_" + index);
            case GenericPad.Y: return UnityEngine.Input.GetAxisRaw("Y_" + index);

            case GenericPad.Back: return UnityEngine.Input.GetAxisRaw("Back_" + index);
            case GenericPad.Start: return UnityEngine.Input.GetAxisRaw("Start_" + index);
            
            case GenericPad.LThumstickDown: return UnityEngine.Input.GetAxisRaw("LS_" + index);
            case GenericPad.RThumstickDown: return UnityEngine.Input.GetAxisRaw("RS_" + index);

            case GenericPad.DPadLeft:   return -Mathf.Min(0, UnityEngine.Input.GetAxisRaw("DPad_XAxis_" + index));
            case GenericPad.DPadRight:  return  Mathf.Max(0, UnityEngine.Input.GetAxisRaw("DPad_XAxis_" + index));
            case GenericPad.DPadUp:     return  Mathf.Min(0, UnityEngine.Input.GetAxisRaw("DPad_YAxis_" + index));
            case GenericPad.DPadDown:   return -Mathf.Max(0, UnityEngine.Input.GetAxisRaw("DPad_YAxis_" + index));
        }

        return 0.0f;
    }

    public bool GetDown(GenericPad button)
    {
        return Get(button) > (1.0f - mThreshold);
    }

    public bool GetUp(GenericPad button)
    {
        return Get(button) < mThreshold;
    }
}
