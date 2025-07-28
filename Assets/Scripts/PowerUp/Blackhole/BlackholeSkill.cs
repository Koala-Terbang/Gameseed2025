using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : MonoBehaviour
{
    public GameObject pulsePrefab;
    public float cooldown = 6f;

    void Start()
    {
        StartCoroutine(BlackHoleLoop());
    }

    IEnumerator BlackHoleLoop()
    {
        while (true)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                ThrowPulse(target.transform.position);
            }

            yield return new WaitForSeconds(cooldown);
        }
    }

    void ThrowPulse(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
        GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity);
        pulse.GetComponent<BlackholeMechanics>().SetDirection(dir);
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
