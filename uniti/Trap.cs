using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour {
    [SerializeField] private int damage = 10;
    [SerializeField] private float hurtInterval = 2;
    private PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            health = collision.gameObject.GetComponent<PlayerHealth>();
            StartCoroutine(HurtPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            health = null;
            StopAllCoroutines();
        }
    }

    private IEnumerator HurtPlayer() {
        while (health != null) {
            health.TakeDamage(damage);

            yield return new WaitForSeconds(hurtInterval);
        }
    }
}
