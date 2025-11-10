using UnityEngine;
using System;

public class LoopHallway : MonoBehaviour
{
    [SerializeField] private GameObject[] otherHallways;
    [SerializeField] private float distanceToTeleport;
    [SerializeField] private bool leftTeleport;
    [SerializeField] private GameAssetsManager[] rmManager;
    public event Action HallwayLooped;

    void Start()
    {
        HallwayLooped += TriggerRemove;
    }

    void OnDisable()
    {
        HallwayLooped -= TriggerRemove;
    }


    void TriggerRemove()
    {
        foreach (GameAssetsManager rm in rmManager) rm.ChangeNextIteration();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered method");
            GameObject targetHallway = GetTargetHallway();

            if (targetHallway == null)
            {
                Debug.LogWarning("No valid hallway found.");
                return;
            }

            float distanceBetween = Mathf.Abs(transform.position.x - targetHallway.transform.position.x);
            if (distanceBetween < 20f) return;

            Vector3 newPosition = targetHallway.transform.position;
            newPosition.x += distanceToTeleport;
            targetHallway.transform.position = newPosition;

            if (!leftTeleport) {
                HallwayLooped?.Invoke();
                Debug.Log("Teleport called");
            }

            //Debug.Log($"Teleported {targetHallway.name} to {newPosition.x}");
        }
    }

    private GameObject GetTargetHallway()
    {
        if (otherHallways == null || otherHallways.Length == 0) return null;

        GameObject target = otherHallways[0];

        foreach (GameObject hallway in otherHallways)
        {
            if (hallway == null) continue;

            if (leftTeleport)
            {
                // Looking for the RIGHTMOST hallway
                if (hallway.transform.position.x > target.transform.position.x)
                {
                    target = hallway;
                }
            }
            else
            {
                // Looking for the LEFTMOST hallway
                if (hallway.transform.position.x < target.transform.position.x)
                {
                    target = hallway;
                }
            }
        }

        return target;
    }
}
