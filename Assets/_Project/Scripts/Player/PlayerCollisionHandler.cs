using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1 = new(1f);

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("You win!");
                StartCoroutine(LoadNextLevel());
                break;
            default:
                Debug.Log("You lose!");
                StartCoroutine(ReloadLevel());
                break;
        }    
    }

    private IEnumerator ReloadLevel()
    {
        yield return _waitForSeconds1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadNextLevel()
    {
        yield return _waitForSeconds1;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
