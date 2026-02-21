using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    // [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem successParticles;
    
    private AudioSource audioSource;
    
    private bool isControllable = true;
    private static readonly WaitForSeconds WaitForSeconds1 = new(1f);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable)
        {
            return;
        }
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
        isControllable = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerThrust>().enabled = false;
        // inputReader.DisablePlayerInputActions();
        audioSource.Stop();
        audioSource.PlayOneShot(playerConfig.CrashSfx);
        crashParticles.Play();
        yield return WaitForSeconds1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    private IEnumerator WinningSequence()
    {
        isControllable = false;
        GetComponent<PlayerMovement>().enabled = false; // The audio setup is dogwater I can´t just disable the inputs via the interface 
        // inputReader.DisablePlayerInputActions(); // because the audio bugs when thrusting + winning or crashing routine fires up
        GetComponent<PlayerThrust>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(playerConfig.SuccessSfx);
        successParticles.Play();
        yield return WaitForSeconds1;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
