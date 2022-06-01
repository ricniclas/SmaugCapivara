using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    public float speed;
    private Vector3 currentPosition, lastPosition, positionDifference;
    public Transform mainCamera;

    private void Awake()
    {
    }

    private void LateUpdate()
    {
        currentPosition = new Vector3(mainCamera.position.x, mainCamera.position.y,0);
        if (currentPosition == lastPosition)
        {
            positionDifference = Vector3.zero;
        }
        else
        {
            positionDifference = currentPosition - lastPosition;
        }
        transform.Translate(new Vector3(positionDifference.x, positionDifference.y, 0) * -1 * speed * Time.deltaTime, Space.World);
        lastPosition = currentPosition;
    }
}
