using UnityEngine;
using System.Collections;

public enum GenericPad
{
    A,
    B,
    X,
    Y,
    Back,
    Start,
    LTrigger,
    RTrigger,
    LShoulder,
    RShoulder,
    DPadUp,
    DPadDown,
    DPadLeft,
    DPadRight,
    LThumstickDown,
    RThumstickDown,
    AxisLeftX,
    AxisLeftY,
    AxisRightX,
    AxisRightY,
}

public class InputSystem
{
    private static int m_MaxControllers = 2;
    private static Controller[] m_Controller;

    static InputSystem()
    {
        m_Controller = new Controller[m_MaxControllers];
        for(int i = 0; i < m_Controller.Length; i++)
        {
            m_Controller[i] = new Controller(i);
        }
    }

    public static Controller GetController(int index)
    {
        return m_Controller[index];
    }
}
