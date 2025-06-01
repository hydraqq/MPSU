using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            return;
        }

        if (PlayerMovement.CurrCheckpoint != null) {
            PlayerMovement.CurrCheckpoint.animator.SetBool("IsActive", false);
        }

        PlayerMovement.CurrentCheckpoint = transform.position;
        PlayerMovement.CurrCheckpoint = this;
        animator.SetBool("IsActive", true);
    }
}
