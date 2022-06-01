using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public DistanceJoint2D distanceJoint2D;
    public LineRenderer lineRenderer;
    public bool isHoldingHookInput;
    public bool isHooked;
    private float maxHookDistance;
    private float hookMovementSpeed;
    private float verticalInput;
    [SerializeField] private Transform hookAnchor;
    [SerializeField] private Transform hookAim;
    [SerializeField] private LayerMask hoockableLayer;

    public void enableHook(Vector2 position)
    {
        distanceJoint2D.enabled = true;
        lineRenderer.enabled = true;
        distanceJoint2D.connectedAnchor = position;
        isHooked = true;
    }

    public void disableHook()
    {
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
        isHooked = false;
    }

    public void throwHook()
    {
        if (!isHooked)
        {
            RaycastHit2D hit = Physics2D.Raycast(hookAnchor.position, hookAnchor.right, 10f, hoockableLayer);
            if (hit.collider != null)
            {
                Debug.DrawRay(hookAnchor.position, hookAnchor.right * 10f, Color.green);
                enableHook(hit.point);
            }
            else
            {
                Debug.DrawRay(hookAnchor.position, hookAnchor.right * 10f, Color.red);
            }
        }
    }
    

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, hookAnchor.position);
            lineRenderer.SetPosition(1, distanceJoint2D.connectedAnchor);
        }

        if (isHooked)
        {
            if (verticalInput != 0)
            {
                distanceJoint2D.distance = Mathf.Clamp(distanceJoint2D.distance -= ((verticalInput * hookMovementSpeed) * Time.deltaTime), 0.2f, maxHookDistance);
            }
        }

    }

    public void setVerticalInput(float verticalInput)
    {
        this.verticalInput = verticalInput;
    }

    public void setHookValues(float maxHookDistance, float hookMovementSpeed)
    {
        this.maxHookDistance = maxHookDistance;
        this.hookMovementSpeed = hookMovementSpeed;
    }
}
