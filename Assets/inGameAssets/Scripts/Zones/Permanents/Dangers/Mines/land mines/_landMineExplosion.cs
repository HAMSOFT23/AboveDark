using System.Collections;
using UnityEngine;

public class _landMineExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float maxExplosionRadius = 5.0f;    // Maximum explosion radius
    [SerializeField] private float expansionDuration = 0.5f;     // How long it takes to reach max radius
    [SerializeField] private float explosionForce = 500f;        // Force applied to objects in explosion
    [SerializeField] private string excludeTag = "Player";       // Tag to exclude from explosion effects
    
    [Header("Optional Effects")]
    [SerializeField] private GameObject explosionVFX;            // Visual effect prefab
    [SerializeField] private AudioClip explosionSound;           // Sound effect for explosion

    // Component references
    private CircleCollider2D blastCollider;                      // The collider that will expand
    private bool hasExploded = false;                            // Flag to prevent multiple explosions

    private void Awake()
    {
        // Get existing blast collider or create one
        blastCollider = GetComponent<CircleCollider2D>();
        if (blastCollider == null)
        {
            blastCollider = gameObject.AddComponent<CircleCollider2D>();
        }
        
        // Configure the blast collider - this will expand during explosion
        blastCollider.isTrigger = true;
        blastCollider.radius = 0.01f;  // Start very small, almost invisible
        blastCollider.enabled = false;  // Disable until explosion
    }

    // Public method that can be called from another script
    public void TriggerExplosion()
    {
        if (!hasExploded)
        {
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        // Set flag to prevent multiple explosions
        hasExploded = true;
        
        // Enable the blast collider
        blastCollider.enabled = true;
        
        // Play explosion VFX if assigned
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        
        // Play explosion sound if assigned
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        
        // Track objects we've already affected to avoid applying force multiple times
        Hashtable affectedObjects = new Hashtable();
        
        // Start time for the expansion
        float startTime = Time.time;
        float endTime = startTime + expansionDuration;
        
        // Expand the collider over time
        while (Time.time < endTime)
        {
            // Calculate the current radius based on time
            float elapsedTime = Time.time - startTime;
            float expansionProgress = elapsedTime / expansionDuration;
            
            // Update the blast collider radius using smooth interpolation
            float currentRadius = Mathf.Lerp(0.01f, maxExplosionRadius, expansionProgress);
            blastCollider.radius = currentRadius;
            
            // We need to manually check for objects inside the blast radius
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, currentRadius);
            
            foreach (Collider2D col in colliders)
            {
                // Skip objects with the excluded tag
                if (col.CompareTag(excludeTag))
                    continue;
                
                // Skip if we've already affected this object
                if (affectedObjects.ContainsKey(col.GetInstanceID()))
                    continue;
                
                // Get the rigidbody to apply force
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Calculate explosion direction (away from center)
                    Vector2 direction = (col.transform.position - transform.position).normalized;
                    
                    // Calculate force based on distance (closer = stronger force)
                    float distance = Vector2.Distance(transform.position, col.transform.position);
                    float forceFactor = 1.0f - Mathf.Clamp01(distance / maxExplosionRadius);
                    float adjustedForce = explosionForce * forceFactor;
                    
                    // Apply the explosion force
                    rb.AddForce(direction * adjustedForce, ForceMode2D.Impulse);
                    
                    // Mark this object as affected
                    affectedObjects.Add(col.GetInstanceID(), true);
                }
            }
            
            // Wait for the next frame
            yield return null;
        }
        
        // Ensure the collider reaches its final size
        blastCollider.radius = maxExplosionRadius;
        
        // Optional: destroy the mine after explosion
        //Destroy(gameObject, 0.5f);
    }

    // Helpful for visualizing the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxExplosionRadius);
    }
}