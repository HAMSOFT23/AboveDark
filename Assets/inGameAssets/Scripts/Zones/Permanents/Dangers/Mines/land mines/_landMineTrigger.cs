using UnityEngine;
public class _landMineTrigger : MonoBehaviour
{
    [SerializeField] private string excludeTag = "Ground";  // Tag to exclude from triggering the mine
    public _explosion explosionScript;                      // Reference to the explosion script

    private bool hasTriggered = false;                     // Prevents multiple triggers

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't process if already triggered or if the collision is with an excluded object
        if (!hasTriggered && !collision.gameObject.CompareTag(excludeTag))
        {
            // Set the flag to prevent multiple explosions
            hasTriggered = true;
            
            // Make sure we have a valid reference to the explosion script
            if (explosionScript != null)
            {
                // Call the explosion method on the other script
                explosionScript.Explode();
                Debug.Log("Explosion Triggered");
            }
            else
            {
                Debug.LogWarning("Cannot trigger explosion: No explosion script assigned to " + gameObject.name);
            }
        }
    }
}