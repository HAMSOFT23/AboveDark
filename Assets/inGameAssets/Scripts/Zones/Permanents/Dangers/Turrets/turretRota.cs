using UnityEngine;

public class turretRota : MonoBehaviour {
    [Header("Targeting")]
    public Transform player_;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 20f;
    // The minimum and maximum rotation angles relative to the turret’s default forward.
    public float minRotation = -90f;
    public float maxRotation = 90f;
    // Default angle when no target is present.
    public float defaultAngle = 90f;

    [Header("Sprite Orientation")]
    // Adjust this offset based on your sprite’s default facing direction.
    // For example, if your turret sprite is drawn pointing up, set this to 90.
    public float spriteOffset = 90f;

    public GameObject turret;

    private void Start() {
        // Assume the turret is the first child of this GameObject.
        turret = transform.GetChild(0).gameObject;
    }

    private void Update() {
        TrackPlayer();
    }

    private void TrackPlayer() {
        Transform turretTransform = turret.transform;
        if (player_ == null) {
            // No target—rotate to the default angle.
            Quaternion defaultRot = Quaternion.Euler(0f, 0f, defaultAngle);
            turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, defaultRot, rotationSpeed * Time.deltaTime);
        }
        else {
            // Calculate the direction from the turret to the player.
            Vector2 direction = player_.position - turretTransform.position;
            // Determine the angle (in degrees) relative to the positive X-axis.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Adjust the computed angle using the sprite offset.
            float desiredAngle = angle - spriteOffset;
            // Clamp the angle so the turret cannot rotate beyond the set limits.
            desiredAngle = ClampAngle(desiredAngle, minRotation, maxRotation);
            // Create the target rotation.
            Quaternion targetRot = Quaternion.Euler(0f, 0f, desiredAngle);
            // Smoothly interpolate toward the target rotation.
            turretTransform.localRotation = Quaternion.Slerp(turretTransform.localRotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    // Normalizes the angle to [-180, 180] before clamping.
    private float ClampAngle(float angle, float min, float max) 
    {
        angle = (angle + 180f) % 360f - 180f;
        return Mathf.Clamp(angle, min, max);
    }
    
    private void OnTriggerEnter2D(Collider2D colliderInfo) {
        if (colliderInfo.CompareTag("Player")) {
            player_ = colliderInfo.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D colliderInfo) {
        if (colliderInfo.CompareTag("Player")) {
            player_ = null;
        }
    }
}
