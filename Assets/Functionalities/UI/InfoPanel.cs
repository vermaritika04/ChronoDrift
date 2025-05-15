using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for Button
using TMPro; // Required for TextMeshPro

public class InfoPanel : MonoBehaviour
{
    public GameObject infoPanel; // Assign the panel in the inspector
    public Button enterButton; // Assign the Enter button in the inspector
    public Button helpButton; // Assign the Help button in the inspector
    public TMP_Text instructionsText; // Assign the TextMeshPro text in the inspector

    void Start()
    {
        // Always show info panel at the start of each new session
        infoPanel.SetActive(true);

        // Add listener to the Enter button
        if (enterButton != null)
        {
            enterButton.onClick.AddListener(OnEnterButtonClicked);
        }

        // Add listener to the Help button
        if (helpButton != null)
        {
            helpButton.onClick.AddListener(OnHelpButtonClicked);
        }
    }

    void OnEnterButtonClicked()
    {
        // Hide the info panel when Enter is clicked
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    void OnHelpButtonClicked()
    {
        // Show the info panel when Help button is clicked
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }

    void Update()
    {
        // Check for Enter key press to close the info panel
        if (infoPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterButtonClicked();
        }
    }
}
