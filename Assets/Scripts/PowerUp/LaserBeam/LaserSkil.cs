using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkil : MonoBehaviour
{
    public GameObject beamPrefab;
    public float activeTime = 5f;
    public float cooldownTime = 10f;

    private GameObject beamInstance;

    void Start()
    {
        beamInstance = Instantiate(beamPrefab, transform.position, Quaternion.identity, transform);
        beamInstance.SetActive(false);
        StartCoroutine(BeamCycle());
    }

    IEnumerator BeamCycle()
    {
        while (true)
        {
            beamInstance.SetActive(true);
            yield return new WaitForSeconds(activeTime);

            beamInstance.SetActive(false);
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
