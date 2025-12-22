using UnityEngine;

public class DeathArea : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            return;
        }

        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(100);
    }
}
