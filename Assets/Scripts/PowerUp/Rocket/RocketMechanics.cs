using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMechanics : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 30f;
    public float lifetime = 3f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime); // auto-destroy after time
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        transform.right = direction; // rotate sprite to face target
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null) hp.TakeDamage(damage);
            Destroy(gameObject); // Rocket disappears on impact
        }
    }
}
