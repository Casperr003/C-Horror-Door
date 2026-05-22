using UnityEngine;
using System.Collections;

public class MagicWall : MonoBehaviour
{
    [Header("Object to affect")]
    public GameObject targetObject;
    [Header("Behaviour")]
    public bool destroyObject = false;
    [Header("Spawn safety")]
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
        if (!canTrigger)
            return;
        if (!other.CompareTag("Player"))
            return;
        if (hasTriggered)
            return;
        hasTriggered = true;
        if (targetObject == null)
        {
            Debug.LogWarning("No target object assigned.");
            return;
        }
        if (destroyObject)
        {
            Destroy(targetObject);
        }
        else
        {
            targetObject.SetActive(false);
        }
    }
}