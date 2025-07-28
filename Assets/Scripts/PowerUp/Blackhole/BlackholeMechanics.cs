using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeMechanics : MonoBehaviour
{
[Header("Pulse Settings")]
    public float speed = 10f;
    public float maxTravelDistance = 8f;

    [Header("Black Hole Settings")]
    public float pullForce = 8f;
    public float duration = 2f;
    public float pullRadius = 4f;

    private Vector2 direction;
    private Vector2 startPosition;
    private bool isBlackHole = false;
    private float blackHoleTimer = 0f;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isBlackHole)
        {
            // Move the pulse forward
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            float distance = Vector2.Distance(startPosition, transform.position);
            if (distance >= maxTravelDistance)
            {
                ActivateBlackHole();
            }
        }
        else
        {
            // Pull all enemies nearby
            blackHoleTimer += Time.deltaTime;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pullRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Transform enemy = hit.transform;
                    Vector2 dir = (transform.position - enemy.position).normalized;
                    enemy.position += (Vector3)(dir * pullForce * Time.deltaTime);
                }
            }

            if (blackHoleTimer >= duration)
                Destroy(gameObject);
        }
    }

    void ActivateBlackHole()
    {
        isBlackHole = true;
        blackHoleTimer = 0f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
