using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MirelightFireflyLightController : MonoBehaviour
{
    public Light2D light2D;      
    public float intensityMin = 0.3f;
    public float intensityMax = 0.8f;
    public float flickerSpeed = 2f;

    public float moveRadius = 0.5f;
    public float moveSpeed = 1f;

    private Vector3 initialPosition;
    private float randomOffset;

    void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();

        initialPosition = transform.position;
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Flicker
        float intensity = Mathf.Lerp(intensityMin, intensityMax,
            (Mathf.Sin(Time.time * flickerSpeed + randomOffset) + 1f) / 2f);
        light2D.intensity = intensity;

        // Hover
        float offsetX = Mathf.PerlinNoise(Time.time * moveSpeed + randomOffset, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * moveSpeed + randomOffset) - 0.5f;
        transform.position = initialPosition + new Vector3(offsetX, offsetY, 0f) * moveRadius;
    }
}