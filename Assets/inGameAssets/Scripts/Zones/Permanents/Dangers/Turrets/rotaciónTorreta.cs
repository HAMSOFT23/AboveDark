using UnityEngine;

public class RotacionTorreta : MonoBehaviour 
{
    public float rotationSpeed = 20F;
    public float minRotation = -90F;
    public float maxRotation = 90F;
    public Quaternion targetRotation;
    public GameObject turret;
    
    private Transform currentTarget;
    private DeteccionTorreta detector;
    
    private void Start() 
    {
        turret = this.transform.GetChild(0).gameObject;
        detector = GetComponent<DeteccionTorreta>();
        
        if (detector != null)
            detector.OnTargetChanged += OnTargetChanged;
    }
    
    private void OnDestroy()
    {
        if (detector != null)
            detector.OnTargetChanged -= OnTargetChanged;
    }
    
    private void OnTargetChanged(Transform newTarget)
    {
        currentTarget = newTarget;
    }
    
    private void Update() 
    {
        TrackPlayer();
    }
    
private void TrackPlayer()
    {
        if (currentTarget == null)
        {
            // Apply the default rotation (existing code seems fine)
            turret.transform.localRotation = Quaternion.Slerp(
                turret.transform.localRotation,
                Quaternion.Euler(0F, 0F, 90F), // Assuming 90 degrees is your desired default 'up'
                (rotationSpeed * Time.deltaTime)
            );
        }
        else // When currentTarget is not null
        {
            // Get direction to the player
            var direction = (currentTarget.position - turret.transform.position);
            direction.z = 0;

            // Calculate the target angle in degrees
            var targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            // --- USE THE OFFSET FOR 'UP' ORIENTATION ---
            targetAngle -= 90f;
            // --- END OF OFFSET ---

            // --- TEMPORARILY SKIP CLAMPING ---
            // float clampedAngle = ClampAngle(targetAngle, minRotation, maxRotation);
            // --- USE THE UNCLAMPED ANGLE DIRECTLY ---
            float finalAngle = targetAngle;

            // Create the target rotation using Euler angles around the Z-axis
            Quaternion finalTargetRotation = Quaternion.Euler(0f, 0f, finalAngle); // Use finalAngle

            // Apply the rotation smoothly using Slerp to the turret's LOCAL rotation
            turret.transform.localRotation = Quaternion.Slerp(
                turret.transform.localRotation,
                finalTargetRotation,
                (rotationSpeed * Time.deltaTime)
            );

            // --- Add Debug Logs to see what's happening ---
            Debug.Log($"Target Direction: {direction.normalized}, Raw Atan2 Angle: {Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg}, Offset Angle: {finalAngle}");
        }

        float ClampAngle(float angle, float min, float max) 
        {
            // Normalize the angle - exact copy from original
            if (angle > 180) 
                angle -= 360;
            if (angle < -180) 
                angle += 360;
            
            // Clamp the angle
            return Mathf.Clamp(angle, min, max);
        }
    }
}