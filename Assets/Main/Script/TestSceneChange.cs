using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to change scene after 5 seconds
        StartCoroutine(ChangeSceneAfterDelay(3f));
    }

    // Coroutine that waits for the given delay (in seconds) before changing the scene
    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for 5 seconds
        Debug.Log("change");
        SceneManager.LoadScene("Jumping");       // Load the scene named "Jumping"
    }
}
