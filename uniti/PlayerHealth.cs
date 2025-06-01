using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private Image fill;
    [SerializeField] private AudioSource hurtSound;

    public float Health = 100;

    public void TakeDamage(int damage) {
        Health -= damage;
        hurtSound.Play();
        UpdateBar();
    }

    public void SetHealth(int health) {
        Health = health;
        UpdateBar();
    }

    private void UpdateBar() {
        if (Health <= 0) {
            transform.position = PlayerMovement.CurrentCheckpoint;
            Health = 100;
        }
        fill.fillAmount = Health / 100.0f;
    }
}
