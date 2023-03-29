using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = transform.position;
    }

    void Update() {
        {
            if (shakeDuration > 0)
            {
                transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
                Debug.Log(shakeDuration);
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }
}
