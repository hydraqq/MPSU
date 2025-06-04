using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] private AudioMixer mixer;

    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(ValudeChanged);
    }

    public void ValudeChanged(float value) {
        mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
    }
}
