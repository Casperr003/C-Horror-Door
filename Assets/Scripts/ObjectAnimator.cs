using UnityEngine;
using System.Collections;

public class ObjectAnimatorTrigger : MonoBehaviour
{
    [Header("Object to animate")]
    public Transform targetObject;
    [Header("Movement")]
    public bool enableMovement;
    public Vector3 moveOffset;
    public float moveDuration = 1f;
    [Header("Rotation")]
    public bool enableRotation;
    public Vector3 rotationOffset;
    public float rotateDuration = 1f;
    [Header("Options")]
    public bool loop = false;
    [Header("Return Settings")]
    public bool returnToStart = true;
    public float returnDelay = 20f;
    [Header("Spawn Protection")]
    public float spawnDelay = 0.5f;
    bool canTrigger = false;
    bool hasTriggered = false;
    Vector3 originalPos;
    Quaternion originalRot;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("No target object assigned.");
            return;
        }
        originalPos = targetObject.localPosition;
        originalRot = targetObject.localRotation;
    }

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
        StopAllCoroutines();
        StartCoroutine(AnimateObject());
    }
    IEnumerator AnimateObject()
    {
        do
        {
            float timer = 0f;
            Vector3 targetPos =
                originalPos + moveOffset;
            Quaternion targetRot =
                originalRot *
                Quaternion.Euler(rotationOffset);
            while (timer < Mathf.Max(moveDuration, rotateDuration))
            {
                timer += Time.deltaTime;
                if (enableMovement)
                {
                    float moveT =
                        Mathf.Clamp01(timer / moveDuration);
                    targetObject.localPosition =
                        Vector3.Lerp(
                            originalPos,
                            targetPos,
                            moveT);
                }
                if (enableRotation)
                {
                    float rotT =
                        Mathf.Clamp01(timer / rotateDuration);
                    targetObject.localRotation =
                        Quaternion.Lerp(
                            originalRot,
                            targetRot,
                            rotT);
                }
                yield return null;
            }
        } while (loop);
        if (returnToStart && !loop)
        {
            yield return new WaitForSeconds(returnDelay);
            float timer = 0f;
            Vector3 currentPos =
                targetObject.localPosition;
            Quaternion currentRot =
                targetObject.localRotation;
            while (timer < Mathf.Max(moveDuration, rotateDuration))
            {
                timer += Time.deltaTime;
                if (enableMovement)
                {
                    float moveT =
                        Mathf.Clamp01(timer / moveDuration);
                    targetObject.localPosition =
                        Vector3.Lerp(
                            currentPos,
                            originalPos,
                            moveT);
                }
                if (enableRotation)
                {
                    float rotT =
                        Mathf.Clamp01(timer / rotateDuration);
                    targetObject.localRotation =
                        Quaternion.Lerp(
                            currentRot,
                            originalRot,
                            rotT);
                }
                yield return null;
            }
        }
    }
}