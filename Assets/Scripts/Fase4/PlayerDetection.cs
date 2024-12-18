using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool playerInArea;
    public Transform player;

    [SerializeField] private string detectionTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            playerInArea = true;
            player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            playerInArea = false;
            player = null;
        }
    }
}
