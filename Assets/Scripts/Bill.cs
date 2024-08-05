using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bill : MonoBehaviour
{
    public ObjectPool<Bill> ObjectPool { get; set; }

    private readonly Vector3 gravity = new(0, -981f, 0);
    private Vector3 velocity;
    private float maxHeight;

    private bool isActive;
    private float screenBottomY;

    private void Awake()
    {
        screenBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).y;
    }

    public void Initialize(Vector3 initialVelocity, float maxHeight)
    {
        this.maxHeight = maxHeight;
        velocity = initialVelocity;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        transform.position += velocity * Time.deltaTime;

        if (transform.position.y > maxHeight)
        {
            velocity = gravity;
        }

        if (transform.position.y < screenBottomY)
        {
            isActive = false;
            ObjectPool.Release(this);
        }
    }
}
