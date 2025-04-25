using UnityEngine;
using System.Collections;

public class DeteccionTorreta : MonoBehaviour
{
    public enum DetectionMethod
    {
        Raycast,
        BoxCast
    }

    [Header("Detection Settings")]
    [SerializeField] private DetectionMethod detectionMethod = DetectionMethod.BoxCast;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private Vector2 detectionBoxSize = new Vector2(10f, 5f);
    [SerializeField] private float detectionAngle = 0f;
    [SerializeField] private float detectionInterval = 0.2f;
    [SerializeField] private bool requireLineOfSight = true;
    [SerializeField] private Transform debugTarget;

    [Header("Cast Position Settings")]
    [SerializeField] private Vector2 castPositionOffset = Vector2.zero;
    [SerializeField] private bool useChildTransformForCast = false;
    [SerializeField] private int castOriginChildIndex = 0;

    [Header("Line of Sight Settings")]
    [SerializeField] private Vector2 boxCastSize = new Vector2(0.5f, 0.5f);

    // Event to notify when target changes
    public delegate void TargetChangedHandler(Transform newTarget);
    public event TargetChangedHandler OnTargetChanged;

    private Transform currentTarget;
    
    private void Start()
    {
        StartCoroutine(ScanForTargets());
    }
    
    private Vector2 GetCastOriginPosition()
    {
        if (useChildTransformForCast && transform.childCount > castOriginChildIndex)
        {
            return (Vector2)transform.GetChild(castOriginChildIndex).position + castPositionOffset;
        }
        
        return (Vector2)transform.position + castPositionOffset;
    }
    
    private IEnumerator ScanForTargets()
    {
        WaitForSeconds wait = new WaitForSeconds(detectionInterval);
        
        while (true)
        {
            if (debugTarget != null)
            {
                SetTarget(debugTarget);
                yield return wait;
                continue;
            }
            
            Vector2 castOrigin = GetCastOriginPosition();
            Collider2D[] colliders;
            
            if (detectionMethod == DetectionMethod.BoxCast)
            {
                colliders = Physics2D.OverlapBoxAll(
                    castOrigin, 
                    detectionBoxSize, 
                    detectionAngle, 
                    targetLayers
                );
            }
            else
            {
                colliders = Physics2D.OverlapCircleAll(
                    castOrigin, 
                    detectionRadius, 
                    targetLayers
                );
            }
            
            Transform bestTarget = null;
            float closestDistance = float.MaxValue;
            
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    Transform potentialTarget = col.transform;
                    Vector2 castOriginPos = GetCastOriginPosition();
                    float distance = Vector2.Distance(castOriginPos, potentialTarget.position);
                    Vector2 direction = potentialTarget.position - (Vector3)castOriginPos;
                    
                    if (requireLineOfSight)
                    {
                        bool hasLineOfSight = false;
                        
                        if (detectionMethod == DetectionMethod.BoxCast)
                        {
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            RaycastHit2D hit = Physics2D.BoxCast(
                                castOriginPos,
                                boxCastSize,
                                angle,
                                direction,
                                distance,
                                targetLayers | LayerMask.GetMask("Obstacles")
                            );
                            
                            VisualizeBoxCast(castOriginPos, boxCastSize, angle, direction, distance, hit);
                            hasLineOfSight = hit.collider == null || hit.collider.transform == potentialTarget;
                        }
                        else
                        {
                            RaycastHit2D hit = Physics2D.Raycast(
                                castOriginPos,
                                direction,
                                distance,
                                targetLayers | LayerMask.GetMask("Obstacles")
                            );
                            
                            VisualizeRaycast(castOriginPos, direction, distance, 
                                            hit.collider != null, hit.point);
                            hasLineOfSight = hit.collider == null || hit.collider.transform == potentialTarget;
                        }
                        
                        if (!hasLineOfSight)
                            continue;
                    }
                    
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestTarget = potentialTarget;
                    }
                }
            }
            
            SetTarget(bestTarget);
            yield return wait;
        }
    }
    
    private void VisualizeRaycast(Vector2 startPosition, Vector2 direction, float distance, 
                                 bool hitSomething = false, Vector2 hitPoint = default)
    {
        Color rayColor = hitSomething ? Color.green : Color.red;
        Debug.DrawRay(startPosition, direction.normalized * distance, rayColor, detectionInterval);
        
        if (hitSomething)
        {
            float crossSize = 0.2f;
            Debug.DrawLine(
                new Vector2(hitPoint.x - crossSize, hitPoint.y),
                new Vector2(hitPoint.x + crossSize, hitPoint.y),
                Color.yellow, detectionInterval
            );
            Debug.DrawLine(
                new Vector2(hitPoint.x, hitPoint.y - crossSize),
                new Vector2(hitPoint.x, hitPoint.y + crossSize),
                Color.yellow, detectionInterval
            );
        }
    }
    
    private void VisualizeBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, 
                                 float distance, RaycastHit2D hit)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            Vector3.zero, 
            Quaternion.Euler(0, 0, angle), 
            Vector3.one
        );
        
        Vector2 halfSize = size * 0.5f;
        Vector2[] boxCorners = new Vector2[4];
        boxCorners[0] = new Vector2(-halfSize.x, -halfSize.y);
        boxCorners[1] = new Vector2(halfSize.x, -halfSize.y);
        boxCorners[2] = new Vector2(halfSize.x, halfSize.y);
        boxCorners[3] = new Vector2(-halfSize.x, halfSize.y);
        
        Color startColor = hit.collider != null ? Color.yellow : Color.blue;
        Color endColor = hit.collider != null ? Color.red : Color.cyan;
        
        for (int i = 0; i < 4; i++)
        {
            Vector2 current = rotationMatrix.MultiplyPoint(boxCorners[i]);
            Vector2 next = rotationMatrix.MultiplyPoint(boxCorners[(i + 1) % 4]);
            
            Debug.DrawLine(origin + current, origin + next, startColor, detectionInterval);
            
            if (hit.collider == null)
            {
                Debug.DrawLine(
                    origin + current + direction.normalized * distance,
                    origin + next + direction.normalized * distance,
                    endColor,
                    detectionInterval
                );
            }
        }
        
        if (hit.collider != null)
        {
            Vector2 hitPoint = hit.point;
            float crossSize = 0.2f;
            Debug.DrawLine(
                new Vector2(hitPoint.x - crossSize, hitPoint.y),
                new Vector2(hitPoint.x + crossSize, hitPoint.y),
                Color.red,
                detectionInterval
            );
            Debug.DrawLine(
                new Vector2(hitPoint.x, hitPoint.y - crossSize),
                new Vector2(hitPoint.x, hitPoint.y + crossSize),
                Color.red,
                detectionInterval
            );
        }
    }
    
    private void SetTarget(Transform newTarget)
    {
        if (currentTarget == newTarget)
            return;
            
        currentTarget = newTarget;
        
        // Notify listeners about target change
        OnTargetChanged?.Invoke(currentTarget);
    }
    
    // Public method to get current target
    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector2 castOrigin = GetCastOriginPosition();
        
        // Draw cast origin
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(castOrigin, 0.2f);
        
        // Draw detection area
        Gizmos.color = Color.yellow;
        if (detectionMethod == DetectionMethod.BoxCast)
        {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(
                castOrigin,
                Quaternion.Euler(0, 0, detectionAngle),
                Vector3.one
            );
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(detectionBoxSize.x, detectionBoxSize.y, 0.1f));
            Gizmos.matrix = Matrix4x4.identity;
        }
        else
        {
            Gizmos.DrawWireSphere(castOrigin, detectionRadius);
        }
        
        // Draw target line
        if (currentTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(castOrigin, currentTarget.position);
        }
    }
}