using System.Collections;
using PersonalPackage.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    private static readonly WaitForSeconds WaitForSeconds1 = new(1f);

    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem successParticles;

    private AudioSource audioSource;

    private bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) return;
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
        inputReader.DisablePlayerInputActions();
        AudioBehaviour(playerConfig.CrashSfx);
        crashParticles.Play();
        yield return WaitForSeconds1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WinningSequence()
    {
        isControllable = false;
        inputReader.DisablePlayerInputActions();
        AudioBehaviour(playerConfig.SuccessSfx);
        successParticles.Play();
        yield return WaitForSeconds1;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void AudioBehaviour(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}