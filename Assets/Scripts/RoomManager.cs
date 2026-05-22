using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Physical room positions")]
    public Transform roomPointA;
    public Transform roomPointB;
    [Header("Room pools")]
    public GameObject[] normalRooms;
    public GameObject[] anomalyRooms;
    public GameObject guaranteedSafeRoom;
    [Header("Score")]
    public ScoreManager scoreManager;
    GameObject roomA;
    GameObject roomB;
    bool playerInA = true;
    bool nextRoomHasAnomaly;

    void Start()
    {
        roomA = Instantiate(
            normalRooms[Random.Range(0, normalRooms.Length)],
            roomPointA.position,
            roomPointA.rotation
        );
        GenerateRoomAtB(false);
    }
    public void PlayerChoseNoAnomaly()
    {
        bool correct = !nextRoomHasAnomaly;
        ProcessChoice(correct);
    }
    public void PlayerChoseAnomaly()
    {
        bool correct = nextRoomHasAnomaly;
        ProcessChoice(correct);
    }
    void ProcessChoice(bool correct)
    {
        if (correct)
            scoreManager.AddPoint();
        else
            scoreManager.ResetScore();
        SwapRooms();
        if (correct)
            GenerateNextRandomRoom();
        else
            GenerateGuaranteedSafe();
    }
    void SwapRooms()
    {
        playerInA = !playerInA;
    }

    void GenerateNextRandomRoom()
    {
        nextRoomHasAnomaly = Random.value > .5f;

        if (playerInA)
        {
            Destroy(roomB);
            GenerateRoomAtB(false);
        }
        else
        {
            Destroy(roomA);
            GenerateRoomAtA(false);
        }
    }
    void GenerateGuaranteedSafe()
    {
        nextRoomHasAnomaly = false;
        if (playerInA)
        {
            Destroy(roomB);
            roomB = Instantiate(
                guaranteedSafeRoom,
                roomPointB.position,
                roomPointB.rotation
            );
        }
        else
        {
            Destroy(roomA);
            roomA = Instantiate(
                guaranteedSafeRoom,
                roomPointA.position,
                roomPointA.rotation
            );
        }
    }
    void GenerateRoomAtA(bool forceSafe)
    {
        GameObject prefab;
        nextRoomHasAnomaly = !forceSafe && Random.value > .5f;
        prefab = nextRoomHasAnomaly
            ? anomalyRooms[Random.Range(0, anomalyRooms.Length)]
            : normalRooms[Random.Range(0, normalRooms.Length)];
        roomA = Instantiate(
            prefab,
            roomPointA.position,
            roomPointA.rotation
        );
    }

    void GenerateRoomAtB(bool forceSafe)
    {
        GameObject prefab;
        nextRoomHasAnomaly = !forceSafe && Random.value > .5f;
        prefab = nextRoomHasAnomaly
            ? anomalyRooms[Random.Range(0, anomalyRooms.Length)]
            : normalRooms[Random.Range(0, normalRooms.Length)];
        roomB = Instantiate(
            prefab,
            roomPointB.position,
            roomPointB.rotation
        );
    }
}