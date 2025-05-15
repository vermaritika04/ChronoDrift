using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeedDisplayManager : MonoBehaviour
{
    [Header("Car Components")]
    public CarController carController; // Reference to the CarController script

    [Header("UI Elements")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI gearText;
    public TextMeshProUGUI brakeText;
    public TextMeshProUGUI engineText; // UI for Engine ON/OFF
    public Button restartButton; // Restart Button

    private bool isEngineOn = false; // Engine state

    void Start()
    {
        if (speedText == null || gearText == null || brakeText == null || engineText == null || carController == null || restartButton == null)
        {
            Debug.LogError("Missing UI elements or CarController in the Inspector!");
            return;
        }

        ApplyTextStyle(speedText);
        ApplyTextStyle(gearText);
        ApplyTextStyle(brakeText);
        ApplyTextStyle(engineText);

        UpdateEngineStatus();

        restartButton.onClick.AddListener(RestartScene); // Attach restart function to button
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Toggle engine
        {
            isEngineOn = !isEngineOn;
            UpdateEngineStatus();
        }

        if (isEngineOn && carController != null)
        {
            float speed = carController.GetSpeed(); // Get speed from CarController
            UpdateSpeed(speed);
            UpdateGear(carController.GetCurrentGear()); // Get gear from CarController
            UpdateBrake(carController.IsBraking()); // Update brake status
        }
    }

    void ApplyTextStyle(TextMeshProUGUI textElement)
    {
        textElement.enableAutoSizing = true;
        textElement.fontSizeMin = 12f;
        textElement.fontSizeMax = 24f;
        textElement.alignment = TextAlignmentOptions.Center;
    }

    public void UpdateSpeed(float speed)
    {
        if (speedText != null)
            speedText.text = $"Speed: {Mathf.RoundToInt(speed)} km/h";
    }

    public void UpdateGear(int gear)
    {
        string gearTextValue = "N"; // Default gear (Neutral)

        if (gear == 1) // Forward
        {
            gearTextValue = "F";
        }
        else if (gear == -1) // Reverse
        {
            gearTextValue = "R";
        }

        UpdateGearText(gearTextValue);
    }

    public void UpdateGearText(string gear)
    {
        if (gearText != null)
            gearText.text = $" {gear}";
    }

    public void UpdateBrake(bool isBraking)
    {
        if (brakeText != null)
            brakeText.text = $"Braking: {(isBraking ? "ON" : "OFF")}";
    }

    private void UpdateEngineStatus()
    {
        engineText.text = isEngineOn ? "Engine: ON" : "Engine: OFF";
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene
    }
}
