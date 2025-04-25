using UnityEngine;

public class _LandMine : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 10f;
    public LayerMask affectedLayers;
    
    // Optional: Visual effects
    public GameObject explosionEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            // Trigger the explosion when something enters the collider
            Explode(transform.position);

            // Optional: Spawn visual effect
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }

            // Optional: Destroy this object after explosion
            //Destroy(gameObject);
        }
    }
    
    void Explode(Vector2 explosionPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius, affectedLayers);
    
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        
            if (rb != null)
            {
                // Get the exact position of the object
                Vector2 objectPosition = rb.position;
            
                // Calculate direction from explosion to object
                Vector2 direction = objectPosition - explosionPosition;
            
                // Skip if object is at exact explosion point
                if (direction.sqrMagnitude < 0.001f)
                    continue;
                
                // Normalize to get pure direction
                direction.Normalize();
            
                // Calculate distance for force falloff
                float distance = Vector2.Distance(explosionPosition, objectPosition);
                float forceFalloff = 1.0f - Mathf.Clamp01(distance / explosionRadius);
            
                // Apply force in the calculated direction
                float finalForce = explosionForce * forceFalloff;
                rb.velocity = Vector2.zero; // Optional: reset existing velocity
                rb.AddForce(direction * finalForce, ForceMode2D.Impulse);
            
                // Debug visualization
                Debug.DrawRay(explosionPosition, direction * 2, Color.red, 2f);
            }
        }
    }
    
    // Optional: Visualize explosion radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}