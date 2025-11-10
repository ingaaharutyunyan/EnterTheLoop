using UnityEngine;

public class InsideHallwayCheck : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] colliders;

    void Start()
    {
        foreach (BoxCollider2D col in colliders) col.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") foreach (BoxCollider2D col in colliders) col.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") foreach (BoxCollider2D col in colliders) col.enabled = false;
    }
}
