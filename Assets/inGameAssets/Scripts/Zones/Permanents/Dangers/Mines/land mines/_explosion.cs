using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class _explosion : MonoBehaviour
{
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float explosionForceMultiplier = 100000;
    [SerializeField] private float explosionRadius = 5;

    public void Explode()
    {
        
        
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rb = o.GetComponent<Rigidbody2D>();

            if (o_rb != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = explosionForceMultiplier / distanceVector.magnitude;
                    o_rb.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }
}
