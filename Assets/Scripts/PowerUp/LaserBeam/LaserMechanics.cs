using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMechanics : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float damage = 20f;
    public float orbitRadius = 2f;

    private Transform player;
    private float currentAngle;

    void Start()
    {
        player = transform.parent;
        currentAngle = 0f;
    }

    void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        float rad = currentAngle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitRadius;
        transform.position = player.position + offset;
        transform.up = offset; // Optional: rotate beam to face outward
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);
        }
    }
}
