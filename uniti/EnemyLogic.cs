using UnityEngine;

public class EnemyLogic : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private Transform patrolStart;
    [SerializeField] private Transform patrolEnd;

    [SerializeField] private int health = 40;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 velocity = Vector2.zero;
    private float patrolTime = 0;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        velocity = Vector3.Lerp(patrolStart.position, patrolEnd.position, (Mathf.Sin(patrolTime / 1.5f) + 1.0f) / 2.0f) - transform.position;
        velocity.Normalize();
        patrolTime += Time.deltaTime;

        animator.SetBool("IsRunning", Mathf.Abs(velocity.x) > 0.01f);
        if (Mathf.Abs(velocity.x) > 0.01f) {
            rb.linearVelocityX = moveSpeed * Time.deltaTime * velocity.x;
            if (velocity.x < 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}