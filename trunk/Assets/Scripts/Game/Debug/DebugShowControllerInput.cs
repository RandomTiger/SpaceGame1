using UnityEngine;
using System.Collections.Generic;

public class DebugShowControllerInput : MonoBehaviour
{
    public bool m_ShowOnscreen = true;

    void OnGUI()
    {
        int y = 10;
        if (m_ShowOnscreen)
        {
            Controller controller = InputSystem.GetController(0);

            foreach (GenericPad button in System.Enum.GetValues(typeof(GenericPad)))
            {
                float val = controller.Get(button);
                GUI.Label(new Rect(10, y, 200, 20), button.ToString() + " " + val);
                y += 10;
            }
        }
    }
}