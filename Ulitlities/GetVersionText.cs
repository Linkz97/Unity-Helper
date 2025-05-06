using TMPro;
using UnityEngine;

namespace LinkzJ.Games.Ultilities
{
    public class GetVersionText : MonoBehaviour
    {
        private TextMeshProUGUI version_TMP = null;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            version_TMP = GetComponent<TextMeshProUGUI>();
            version_TMP.text = Application.version;
        }
    }

}

