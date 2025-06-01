using TMPro;
using UnityEngine;

public class PickUpCounter : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int pickupWorth = 5;
    [SerializeField] private string baseText = "Points: ";

    private int counter = 0;

    public static PickUpCounter Instance;

    private void Awake() {
        Instance = this;
    }

    public void AddPickUp() {
        counter++;
        text.text = baseText + (counter * pickupWorth);
    }
}
