using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject menu;

    [SerializeField] private Slider effects;
    [SerializeField] private Slider music;

    private void Start() {
        effects.value = PlayerPrefs.GetFloat("effects");
        music.value = PlayerPrefs.GetFloat("music");
        menu.SetActive(false);
    }

    private void OnDestroy() {
        PlayerPrefs.SetFloat("effects", effects.value);
        PlayerPrefs.SetFloat("music", music.value);
        PlayerPrefs.Save();
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }

    public void UnPause() {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
