using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healingValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healingValue);
            gameObject.SetActive(false);
        }
    }
}