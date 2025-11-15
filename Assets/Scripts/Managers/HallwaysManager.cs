using UnityEngine;
using Persistence;

public class HallwaysManager : MonoBehaviour
{
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private GameObject player;

    void Start()
    {
        // Set player X position
        Vector3 pos = player.transform.position;
        pos.x = pSO.GetXPosition();
        player.transform.position = pos;
    }
}
