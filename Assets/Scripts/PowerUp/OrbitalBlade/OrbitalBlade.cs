using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalBlade : MonoBehaviour
{
    [Header("Blade Setup")]
    public GameObject bladePrefab;
    public int bladeCount = 6;
    public float radius = 1.5f;
    public float rotationSpeed = 180f;

    [Header("Timing")]
    public float activeDuration = 5f;
    public float cooldownDuration = 10f;

    private List<GameObject> blades = new List<GameObject>();
    private bool isOnCooldown = false;

    void Start()
    {
        StartCoroutine(BladeCycle());
    }

    IEnumerator BladeCycle()
    {
        while (true)
        {
            if (!isOnCooldown)
            {
                SpawnBlades();
                yield return new WaitForSeconds(activeDuration);

                DespawnBlades();
                isOnCooldown = true;
                yield return new WaitForSeconds(cooldownDuration);
                isOnCooldown = false;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (blades.Count > 0)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    void SpawnBlades()
    {
        float angleStep = 360f / bladeCount;
        for (int i = 0; i < bladeCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject blade = Instantiate(bladePrefab, transform.position + offset, Quaternion.identity, transform);
            blades.Add(blade);
        }
    }

    void DespawnBlades()
    {
        foreach (GameObject blade in blades)
        {
            Destroy(blade);
        }
        blades.Clear();
    }
}
