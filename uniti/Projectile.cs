using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float dissappearDist = 20;
    public float Speed = 5;


    private void Update() {
        transform.position += new Vector3(Speed * Time.deltaTime, 0);

        if (Vector2.Distance(Camera.main.transform.position, transform.position) > dissappearDist) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyLogic>().TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
