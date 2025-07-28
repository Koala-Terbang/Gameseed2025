using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMechanics : MonoBehaviour
{
    public float speed = 10f;
    public float returnDelay = 0.5f;
    public float damage = 20f;

    private Vector2 targetPos;
    private Vector2 launchPoint;
    private bool returning = false;

    public void SetTarget(Vector2 target, Vector2 origin)
    {
        targetPos = target;
        launchPoint = origin;
        StartCoroutine(ReturnToLaunchPointAfterDelay());
    }

    void Update()
    {
        Vector2 direction;

        if (!returning)
        {
            direction = (targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            direction = (launchPoint - (Vector2)transform.position).normalized;
        }

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        transform.right = direction;

        // Auto-destroy if it's close enough to launch point (to prevent overshooting)
        if (returning && Vector2.Distance(transform.position, launchPoint) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ReturnToLaunchPointAfterDelay()
    {
        yield return new WaitForSeconds(returnDelay);
        returning = true;
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
