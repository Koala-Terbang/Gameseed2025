using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMechanics : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    {
        Vector3 targetPosition = player.position;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 5f);
    }
}
