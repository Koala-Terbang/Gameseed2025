using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSkill : MonoBehaviour
{
    public GameObject rocketPrefab;
    public float cooldown = 2f;

    void Start()
    {
        StartCoroutine(FireRockets());
    }

    IEnumerator FireRockets()
    {
        while (true)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                FireAtTarget(target.transform);
            }

            yield return new WaitForSeconds(cooldown);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    void FireAtTarget(Transform target)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        GameObject rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
        rocket.GetComponent<RocketMechanics>().SetDirection(dir);
    }
}
