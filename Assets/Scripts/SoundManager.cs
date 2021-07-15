using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioClip gameStartAudio;
    [SerializeField]private AudioClip touchAudio;
    [SerializeField]private AudioClip scoreAudio;
    [SerializeField]private AudioClip failedAudio;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        //
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.ONGameStarted += PlayStartGameAudio;
        GameManager.Instance.ONTouch += PlayTouchAudio;
        GameManager.Instance.ONCollectableCollision += PlayScoreAudio;
        GameManager.Instance.ONFailed += PlayFailedAudio;
    }

    private void PlayStartGameAudio()
    {
        audioSource.PlayOneShot(gameStartAudio);
    }

    private void PlayTouchAudio()
    {
        audioSource.volume = 1;
        audioSource.PlayOneShot(touchAudio);
        audioSource.volume = .3f;
    }

    private void PlayScoreAudio()
    {
        audioSource.PlayOneShot(scoreAudio);
    }
    void PlayFailedAudio()
    {
        audioSource.PlayOneShot(failedAudio);
    }
}
