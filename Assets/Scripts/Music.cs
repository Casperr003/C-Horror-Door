using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float delay = 60f;
    void Start()
    {
        StartCoroutine(PlayLoop());
    }
    IEnumerator PlayLoop()
    {
        while (true)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}