using UnityEngine;

namespace LinkzJ.Games.Ultilities
{
    public static class GameObjectExtensions {

        // Get, or adds if doesn't containt the component yet
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }

        // Return if gameobject has target component
        public static bool Has<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }

    }  
}

