using UnityEngine;

namespace LinkzJ.Games.Ultilities
{
    public class DontDestoryOnLoad : MonoBehaviour
	{
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}