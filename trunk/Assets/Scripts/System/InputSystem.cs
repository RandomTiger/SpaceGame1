using UnityEngine;
using System.Collections;

public enum InputMap
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

public class InputSystem : MonoBehaviour
{
    private static int m_MaxControllers = 2;
    private static Controller[] m_Controller;

    #region Singleton
    static InputSystem mInstance;

    public static InputSystem Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject("InputSystem Singleton");
                mInstance = go.AddComponent<InputSystem>();
                GameObject.DontDestroyOnLoad(mInstance);
            }
            return mInstance;
        }
    }
    #endregion

    static InputSystem()
    {
        // todo create new monobehavour to update controllers

        m_Controller = new Controller[m_MaxControllers];
        for(int i = 0; i < m_Controller.Length; i++)
        {
            m_Controller[i] = new Controller(i);
        }

        InputSystem.Instance.Update();
    }

    public static Controller GetController(int index)
    {
        return m_Controller[index];
    }

    void Update()
    {
        for (int i = 0; i < m_Controller.Length; i++)
        {
            m_Controller[i].Update();
        }
    }
}