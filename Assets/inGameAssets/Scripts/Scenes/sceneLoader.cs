using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

// Define an enum for scene selection via Build Index.
// --- VERY IMPORTANT ---
// The integer values assigned here (e.g., = 0, = 1) MUST EXACTLY MATCH
// the Build Index of the corresponding scene listed in File -> Build Settings.
// If you reorder scenes in Build Settings, you MUST come back here and
// update these integer values to match the new order!
public enum TargetSceneBuildIndex
{
    MainMenu = 0,   // Corresponds to scene at index 0 in Build Settings
    Búnker = 1,     // Corresponds to scene at index 1 in Build Settings
    Cavern = 2,     // Corresponds to scene at index 2 in Build Settings
    GameOver = 3    // Corresponds to scene at index 3 in Build Settings
    // Add/modify scenes and ensure their assigned number = their Build Index
}

// Ensure this GameObject has a Collider2D component.
// You still need to manually set "Is Trigger" to true in the Inspector.
[RequireComponent(typeof(Collider2D))]
public class sceneLoader : MonoBehaviour // Consider renaming the class/file if preferred
{
    [Header("Trigger Settings")]
    [Tooltip("Select the scene identifier. The script will load the scene using the Build Index number assigned to this identifier in the enum definition above. Make sure that number matches the actual Build Index in File -> Build Settings.")]
    [SerializeField] private TargetSceneBuildIndex sceneToLoad;

    [Tooltip("Specify the tag of the object that can trigger the scene load (e.g., 'Player').")]
    [SerializeField] private string triggeringTag = "Player";

    // This flag prevents loading the scene multiple times if the object stays in the trigger
    private bool triggered = false;

    private void Awake()
    {
        // Optional safety check: Warn if the attached collider is not set to Trigger
        Collider2D col = GetComponent<Collider2D>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"The Collider2D on {gameObject.name} is not set to 'Is Trigger'. Scene loading might not work as expected.", this);
        }
    }

    // This function is called when another Collider2D enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if already triggered to avoid multiple loads
        if (triggered)
        {
            return;
        }

        // Check if the object entering the trigger has the specified tag
        if (other.CompareTag(triggeringTag))
        {
            // Set triggered flag to true
            triggered = true;

            // Get the build index directly by casting the enum value to an integer
            // This works because we assigned explicit integer values in the enum definition.
            int buildIndex = (int)sceneToLoad;

            Debug.Log($"Triggered by {other.name} (Tag: {triggeringTag}). Loading scene with Build Index: {buildIndex}");

            // Load the selected scene using its Build Index
            SceneManager.LoadScene(buildIndex);
        }
    }
}