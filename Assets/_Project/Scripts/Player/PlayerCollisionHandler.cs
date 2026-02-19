using System.Collections;
using PersonalPackage.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioClip winAudioClip;

    private AudioSource audioSource;
    private static readonly WaitForSeconds _waitForSeconds3 = new(3f);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(winAudioClip);
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("You win!");
                StartCoroutine(WinningSequence());
                break;
            default:
                Debug.Log("You lose!");
                StartCoroutine(CrashSequence());
                break;           
        }
    }

    private IEnumerator CrashSequence()
    {
        GetComponent<PlayerThrust>().enabled = false;   
        GetComponent<PlayerMovement>().enabled = false;
        audioSource.PlayOneShot(deathAudioClip);
        yield return _waitForSeconds3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    private IEnumerator WinningSequence()
    {
        GetComponent<PlayerThrust>().enabled = false;   
        GetComponent<PlayerMovement>().enabled = false;
        audioSource.PlayOneShot(winAudioClip);
        yield return _waitForSeconds3;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
