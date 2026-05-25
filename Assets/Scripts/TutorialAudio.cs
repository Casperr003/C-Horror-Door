using UnityEngine;
using System.Collections;

public class TutorialAudio : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    [Header("Freeze")]
    public float freezeDuration = 40f;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip introClip;
    bool hasPlayed = false;
    Vector3 lockedPosition;

    void Start()
    {
        if (hasPlayed)
            return;
        hasPlayed = true;
        if (player == null)
        {
            Debug.LogWarning("Player not assigned");
            return;
        }
        lockedPosition = player.position;
        StartCoroutine(FreezePlayer());
    }

    IEnumerator FreezePlayer()
    {
        if (audioSource != null && introClip != null)
        {
            audioSource.PlayOneShot(introClip);
        }
        float timer = 0f;
        while (timer < freezeDuration)
        {
            player.position = lockedPosition;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}