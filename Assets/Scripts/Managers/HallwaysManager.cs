using UnityEngine;

public class HallwaysManager : MonoBehaviour
{
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private GameObject player;

    void Start()
    {
        Vector3 pos = player.transform.position;
        pos.x = pSO.GetXPosition();
        player.transform.position = pos;
        Debug.Log("Position has been set as " + pos);
    }
}
