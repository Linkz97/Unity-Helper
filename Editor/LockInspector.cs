#if UNITY_EDITOR
using UnityEditor;

// Shortcut Key to Hotbind Unity Lock Function
// % = Ctrl on Windows / Cmd on macOS
// # = Shift
// & = Alt
// _ = Use a key without modifiers
// Make sure to put this scripts on Editor folder on Unity

namespace LinkzJ.Games.Editor
{
    public class LockInspector
    {
        // Ctrl + l to lock/unlock inspector
        [MenuItem("Edit/Lock Inspector %l")]
        public static void Lock()
        {   
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        [MenuItem("Edit/Lock Inspector", true)]
        public static bool Valid()
        {
            return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
        }
    }
}

#endif
