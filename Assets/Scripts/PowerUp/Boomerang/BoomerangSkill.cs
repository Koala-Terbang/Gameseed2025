using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSkill : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public float cooldown = 3f;

    void Start()
    {
        StartCoroutine(ThrowBoomerangs());
    }

    IEnumerator ThrowBoomerangs()
    {
        while (true)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                ThrowAt(target.transform.position);
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

    void ThrowAt(Vector2 targetPos)
    {
        Vector2 launchFrom = transform.position;
        GameObject boomerang = Instantiate(boomerangPrefab, launchFrom, Quaternion.identity);
        boomerang.GetComponent<BoomerangMechanics>().SetTarget(targetPos, launchFrom);
    }
}
