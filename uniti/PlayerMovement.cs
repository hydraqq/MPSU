using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpStrength = 5;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;

    [SerializeField] private AudioSource jumpAudio;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = false;

    public static Vector3 CurrentCheckpoint = Vector3.zero;
    public static Checkpoint CurrCheckpoint = null;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        CurrentCheckpoint = transform.position;
    }

    private void Update() {
        float moveInput = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();

        if (isGrounded && Input.GetButtonDown("Jump")) {
            rb.linearVelocityY = jumpStrength;
            jumpAudio.Play();
        }

        animator.SetBool("IsFalling", !isGrounded);
        animator.SetBool("isRunning", moveInput != 0);

        rb.linearVelocityX = moveInput * moveSpeed;

        if (moveInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius) != null;
    }
}
