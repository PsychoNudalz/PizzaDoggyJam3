using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float flickerSpeed = 0.1f;

    private Light targetLight;
    private float baseIntensity;
    private float timer;

    void Awake()
    {
        targetLight = GetComponent<Light>();
        baseIntensity = targetLight.intensity;
    }

    void Update()
    {
        timer += Time.deltaTime * (1f / flickerSpeed);
        float noise = Mathf.PerlinNoise(Time.time * 10f, timer);
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
