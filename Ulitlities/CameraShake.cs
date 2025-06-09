using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private Vector3 originalPos;
    private Camera camera;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    [SerializeField] private AudioSource shakeAudioSource;
    private void Awake()
    {
        Instance = this;
        camera = Camera.main;
        originalPos = camera.transform.position;
    }

    [Button]
    public void Shake()
    {
        Shake(duration, magnitude);
    }
    
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;
        shakeAudioSource.Play();
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camera.transform.position = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.position = originalPos;
    }
}