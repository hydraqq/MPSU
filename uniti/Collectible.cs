using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField] private AudioSource audioEffect;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            return;
        }
        animator.SetTrigger("Collected");
        audioEffect.Play();
        PickUpCounter.Instance.AddPickUp();
        Invoke(nameof(Destroy), 1);
    }

    private void Destroy() {
        Destroy(gameObject);
    }
}
