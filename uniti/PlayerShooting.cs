using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private Projectile prefab;
    [SerializeField] private float cooldown;

    [SerializeField] private Transform shootPos;

    [SerializeField] private AudioSource shootSound;

    private bool canShoot = true;

    private void Update() {
        if (Time.timeScale > 0 && canShoot && Input.GetButtonDown("Fire1")) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() {
        canShoot = false;

        Instantiate(prefab, shootPos.position, Quaternion.identity).Speed *= transform.localScale.x;
        shootSound.Play();

        yield return new WaitForSeconds(cooldown);

        canShoot = true;
    }
}
