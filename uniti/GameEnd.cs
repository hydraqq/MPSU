using UnityEngine;

public class GameEnd : MonoBehaviour {
    [SerializeField] private GameObject winScreen;
    [SerializeField] private PauseMenu menu;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            winScreen.SetActive(true);
            menu.enabled = false;
            Time.timeScale = 0;
        }
    }
}
