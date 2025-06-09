using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text fpsText;
    [SerializeField] private float updateInterval = 0.5f;

    private int frames = 0;
    private float timeElapsed = 0f;

    void Update()
    {
        frames++;
        timeElapsed += Time.unscaledDeltaTime;

        if (timeElapsed >= updateInterval)
        {
            int fps = Mathf.RoundToInt(frames / timeElapsed);
            fpsText.text = $"FPS: {fps}";
            frames = 0;
            timeElapsed = 0f;
        }
    }
}