using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public bool isNorthDoor;
    RoomManager manager;
    bool canTrigger = true;
    float cooldownTime = 5f;

    void Start()
    {
        manager = FindFirstObjectByType<RoomManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (!canTrigger)
            return;
        canTrigger = false;
        Invoke(nameof(ResetTrigger), cooldownTime);
        if (isNorthDoor)
            manager.PlayerChoseNoAnomaly();
        else
            manager.PlayerChoseAnomaly();
    }
    void ResetTrigger()
    {
        canTrigger = true;
    }
}