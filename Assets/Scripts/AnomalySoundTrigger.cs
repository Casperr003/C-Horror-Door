using UnityEngine;
using System.Collections;

public class AnomalySoundTrigger : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip anomalyClip;
    [Header("Settings")]
    public float spawnDelay = 0.5f;
    bool canTrigger = false;
    bool hasTriggered = false;

    void OnEnable()
    {
        hasTriggered = false;
        canTrigger = false;
        StartCoroutine(ArmTrigger());
    }
    IEnumerator ArmTrigger()
    {
        yield return new WaitForSeconds(spawnDelay);
        canTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTER: " + other.name);
        Debug.Log("ROOT: " + other.transform.root.name);
        if (!canTrigger)
        {
            Debug.Log("Ignored: spawn lock active");
            return;
        }
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Ignored: not player");
            return;
        }
        if (hasTriggered)
        {
            Debug.Log("Ignored: already triggered");
            return;
        }
        hasTriggered = true;
        Debug.Log("ANOMALY TRIGGERED");
        if (audioSource != null && anomalyClip != null)
        {
            audioSource.PlayOneShot(anomalyClip);
        }
        else
        {
            Debug.LogWarning("AudioSource or Clip missing");
        }
    }
}