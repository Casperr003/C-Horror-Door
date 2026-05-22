using UnityEngine;
using System.Collections;

public class ObjectSwapTrigger : MonoBehaviour
{
    [Header("Object currently in scene")]
    public GameObject objectToReplace;
    [Header("Prefab/object to spawn")]
    public GameObject replacementPrefab;
    [Header("Options")]
    public bool destroyOriginal = false;
    [Header("Revert")]
    public bool revertAfterTime = true;
    public float revertDelay = 5f;
    bool triggered = false;
    GameObject spawnedReplacement;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        if (!other.CompareTag("Player"))
            return;
        triggered = true;
        if (objectToReplace == null ||
            replacementPrefab == null)
        {
            Debug.LogWarning(
                "Missing object references"
            );
            return;
        }
        Vector3 pos =
            objectToReplace.transform.position;
        Quaternion rot =
            objectToReplace.transform.rotation;
        Vector3 scale =
            objectToReplace.transform.localScale;
        spawnedReplacement =
            Instantiate(
                replacementPrefab,
                pos,
                rot
            );
        spawnedReplacement.transform.localScale =
            scale;
        if (destroyOriginal)
        {
            objectToReplace.SetActive(false);
        }
        else
        {
            objectToReplace.SetActive(false);
        }
        if (revertAfterTime)
        {
            StartCoroutine(RevertSwap());
        }
    }
    IEnumerator RevertSwap()
    {
        yield return new WaitForSeconds(revertDelay);
        if (spawnedReplacement != null)
        {
            Destroy(spawnedReplacement);
        }
        objectToReplace.SetActive(true);
        triggered = false;
    }
    private void OnEnable()
    {
        triggered = false;
    }
}