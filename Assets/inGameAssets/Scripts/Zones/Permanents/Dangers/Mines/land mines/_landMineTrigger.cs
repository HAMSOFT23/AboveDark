using UnityEngine;
public class _landMineTrigger : MonoBehaviour
{
    [SerializeField] private string excludeTag = "Ground";  // Tag to exclude from triggering the mine
    public _explosion explosionScript;
    // Reference to the explosion script

    [SerializeField] private GameObject player;
    
    private bool hasTriggered = false;                     // Prevents multiple triggers

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't process if already triggered or if the collision is with an excluded object
        if (!hasTriggered && !collision.gameObject.CompareTag(excludeTag))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
// --- Add these lines ---

                // 1. Check if the 'player' variable has been assigned in the Inspector.
                if (player != null)
                {
                    // 2. Get the script component from the pre-assigned 'player' GameObject.
                    //    Replace 'PlayerScript' with the actual name of the script on your Player.
                    playerController scriptOnPlayer = player.GetComponent<playerController>();

                    // 3. Check if the component was actually found on that 'player' GameObject.
                    if (scriptOnPlayer != null)
                    {
                        // 4. Call the public function within that script.
                        //    Replace 'YourFunctionName' with the actual function name.
                        scriptOnPlayer.Death();
                    }
                    else
                    {
                        // Optional: Warning if the script is missing on the assigned player object
                        Debug.LogWarning("PlayerScript component not found on the assigned 'player' GameObject.", player);
                    }
                }
                else
                {
                    // Optional: Warning if the 'player' variable itself wasn't assigned
                    Debug.LogWarning("'player' variable is not assigned in the Inspector for this LandMineTrigger.", gameObject);
                }
                // --- End of added lines ---
            }
            
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