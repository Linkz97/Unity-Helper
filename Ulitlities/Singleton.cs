using UnityEngine;

namespace LinkzJ.Games.Ultilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; protected set; }

        public static bool IsExist => Instance != null;
        public static bool IsNull => Instance == null;

        protected virtual void Awake()
        {
            if (IsExist)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }
        }

        public bool IsThisTheInstance() => Instance == this;

        protected virtual void OnDestroy()
        {
            if (IsThisTheInstance())
            {
                Instance = null;
            }
        }
    }
}

