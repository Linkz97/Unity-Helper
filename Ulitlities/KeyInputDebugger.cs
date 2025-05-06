// If you Getting Error/ Missing Input System
// .. Go to Unity Editor > Window > Package Manager
// .. Install Input System

#if ENABLE_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
namespace LinkzJ.Games.Ultilities
{
    public class KeyInputDebugger : MonoBehaviour
    {
        [SerializeField] private bool debug = false;
        void Update()
        {
            if (!debug) return;

            foreach (InputDevice device in InputSystem.devices)
            {
                foreach (InputControl control in device.allControls)
                {
                    if (control is ButtonControl button && button.wasPressedThisFrame)
                    {
                        // Skip synthetic controls like "anyKey"
                        if (control.name == "anyKey")
                            continue;
                        
                        Debug.Log($"Input detected from {device.name}: {control.displayName}");
                        return;
                    }
                }
            }
        }
    }
}
#endif
