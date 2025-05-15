//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    // UI Elements
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    // Car Rigidbody reference
//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    // Internal variables
//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 0;
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f; // 3 minutes in seconds

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        // Button Listeners
//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        // Initialize sound toggle
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        // Check if the game is not paused and the car is moving
//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            // Increment time only if it's below the max limit
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            // If time reaches max, open settings panel
//            if (raceTime >= maxRaceTime)
//            {
//                OpenSettingsPanel();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);

//        if (audioManager != null)
//        {
//            audioManager.PlayAudioIfEnabled();
//        }
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = maxRaceTime - raceTime;
//            timerText.text = "Time: " + Mathf.Max(0, remainingTime).ToString("F2") + "s"; // Ensure it doesn't go negative
//        }
//    }

//    private void OpenSettingsPanel()
//    {
//        PauseGame();
//    }
//}
//27.3

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    // UI Elements
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    // Car Rigidbody reference
//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    // Internal variables
//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 0;
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f; // 3 minutes in seconds

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        // Button Listeners
//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        // Initialize sound toggle
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        // Check if the game is not paused and the car is moving
//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            // Increment time only if it's below the max limit
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            // If time reaches max, open settings panel
//            if (raceTime >= maxRaceTime)
//            {
//                OpenSettingsPanel();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//        StopAudio(); // Stop audio when the game is paused
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);

//        if (audioManager != null && audioManager.IsAudioEnabled())
//        {
//            object p = audioManager.PlayAudioIfEnabled(); // Resume audio if enabled
//        }
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = maxRaceTime - raceTime;
//            timerText.text = "Time: " + Mathf.Max(0, remainingTime).ToString("F2") + "s"; // Ensure it doesn't go negative
//        }
//    }

//    private void OpenSettingsPanel()
//    {
//        PauseGame();
//    }

//    private void StopAudio()
//    {
//        if (audioManager != null)
//        {
//            audioManager.StopAudio(); // Stop all audio when the game is paused
//        }
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    // UI Elements
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    // Car Rigidbody reference
//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    // Internal variables
//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 0;
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f; // 3 minutes in seconds

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        // Button Listeners
//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        // Initialize sound toggle
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        // Check if the game is not paused and the car is moving
//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            // Increment time only if it's below the max limit
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            // If time reaches max, open settings panel
//            if (raceTime >= maxRaceTime)
//            {
//                OpenSettingsPanel();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//        audioManager.PauseAudio(); // Pause audio when the game is paused
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        audioManager.ResumeAudio(); // Resume audio if enabled
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = maxRaceTime - raceTime;
//            timerText.text = "Time: " + Mathf.Max(0, remainingTime).ToString("F2") + "s"; // Ensure it doesn't go negative
//        }
//    }

//    private void OpenSettingsPanel()
//    {
//        PauseGame();
//    }
//}

//27.3.25

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    // UI Elements
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    // Car Rigidbody reference
//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    // Internal variables
//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 0;
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f; // 3 minutes in seconds

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        // Button Listeners
//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        // Initialize sound toggle
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        // Check if the game is not paused and the car is moving
//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            // Increment time only if it's below the max limit
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            // If time reaches max, open settings panel
//            if (raceTime >= maxRaceTime)
//            {
//                OpenSettingsPanel();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);

//        if (audioManager != null)
//        {
//            audioManager.PlayAudioIfEnabled();
//        }
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = maxRaceTime - raceTime;
//            timerText.text = "Time: " + Mathf.Max(0, remainingTime).ToString("F2") + "s"; // Ensure it doesn't go negative
//        }
//    }

//    private void OpenSettingsPanel()
//    {
//        PauseGame();
//    }
//}
//27.3

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    // UI Elements
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    // Car Rigidbody reference
//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    // Internal variables
//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 0;
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f; // 3 minutes in seconds

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        // Button Listeners
//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        // Initialize sound toggle
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        // Check if the game is not paused and the car is moving
//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            // Increment time only if it's below the max limit
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            // If time reaches max, open settings panel
//            if (raceTime >= maxRaceTime)
//            {
//                OpenSettingsPanel();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//        StopAudio(); // Stop audio when the game is paused
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);

//        if (audioManager != null && audioManager.IsAudioEnabled())
//        {
//            object p = audioManager.PlayAudioIfEnabled(); // Resume audio if enabled
//        }
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = maxRaceTime - raceTime;
//            timerText.text = "Time: " + Mathf.Max(0, remainingTime).ToString("F2") + "s"; // Ensure it doesn't go negative
//        }
//    }

//    private void OpenSettingsPanel()
//    {
//        PauseGame();
//    }

//    private void StopAudio()
//    {
//        if (audioManager != null)
//        {
//            audioManager.StopAudio(); // Stop all audio when the game is paused
//        }
//    }
//}

//7.04.25

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    public GameObject lapTrigger; // Lap Trigger reference

//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private int currentLap = 1; // Start from lap 1
//    private int totalLaps = 3;
//    private const float maxRaceTime = 180f;
//    private bool raceEnded = false;

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        if (raceEnded) return;

//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//            }
//            else
//            {
//                EndGame();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//        audioManager.PauseAudio();
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        audioManager.ResumeAudio();
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null)
//            lapCounterText.text = "Lap: " + currentLap + "/" + totalLaps;
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = Mathf.Max(0, maxRaceTime - raceTime);
//            timerText.text = "Time: " + remainingTime.ToString("F2") + "s";
//        }
//    }

//    private void EndGame()
//    {
//        raceEnded = true;
//        raceTime = maxRaceTime;
//        UpdateTimerDisplay();
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (lapTrigger != null && other.gameObject == lapTrigger)
//        {
//            if (currentLap < totalLaps)
//            {
//                currentLap++;
//                UpdateLapCounter();

//                if (currentLap > totalLaps)
//                {
//                    EndGame();
//                }
//            }
//        }
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class PauseMenu : MonoBehaviour
//{
//    public GameObject settingsPanel;
//    public TMP_Text timerText;
//    public TMP_Text lapCounterText;
//    public Button pauseButton;
//    public Button resumeButton;
//    public Button restartButton;
//    public Button closeButton;
//    public Toggle soundToggle;

//    public Rigidbody carRigidbody;
//    private CarAudioManager audioManager;

//    private bool isPaused = false;
//    private float raceTime = 0f;
//    private const float maxRaceTime = 180f;
//    private bool raceEnded = false;

//    private CarController carController;

//    void Start()
//    {
//        audioManager = FindObjectOfType<CarAudioManager>();
//        carController = FindObjectOfType<CarController>();

//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        ResetTimer();
//        UpdateLapCounter();

//        pauseButton.onClick.AddListener(PauseGame);
//        resumeButton.onClick.AddListener(ResumeGame);
//        restartButton.onClick.AddListener(RestartGame);
//        closeButton.onClick.AddListener(CloseGame);

//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
//            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
//        }
//    }

//    void Update()
//    {
//        if (raceEnded) return;

//        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
//        {
//            if (raceTime < maxRaceTime)
//            {
//                raceTime += Time.deltaTime;
//                UpdateTimerDisplay();
//                UpdateLapCounter();
//            }
//            else
//            {
//                EndGame();
//            }
//        }
//    }

//    public void PauseGame()
//    {
//        isPaused = true;
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//        audioManager.PauseAudio();
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1;
//        settingsPanel.SetActive(false);
//        audioManager.ResumeAudio();
//    }

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }

//    public void CloseGame()
//    {
//        Application.Quit();
//    }

//    public void ToggleSound()
//    {
//        if (audioManager != null)
//        {
//            audioManager.ToggleSound(soundToggle.isOn);
//        }
//    }

//    public void UpdateLapCounter()
//    {
//        if (lapCounterText != null && carController != null)
//        {
//            lapCounterText.text = "Lap: " + carController.GetCurrentLap() + "/" + carController.GetTotalLaps();
//        }
//    }

//    public void ResetTimer()
//    {
//        raceTime = 0f;
//        UpdateTimerDisplay();
//    }

//    private void UpdateTimerDisplay()
//    {
//        if (timerText != null)
//        {
//            float remainingTime = Mathf.Max(0, maxRaceTime - raceTime);
//            timerText.text = "Time: " + remainingTime.ToString("F2") + "s";
//        }
//    }

//    private void EndGame()
//    {
//        raceEnded = true;
//        raceTime = maxRaceTime;
//        UpdateTimerDisplay();
//        Time.timeScale = 0;
//        settingsPanel.SetActive(true);
//    }
//}

//10/4/25

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public TMP_Text timerText;
    public TMP_Text lapCounterText;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public Button closeButton;
    public Toggle soundToggle;

    public Rigidbody carRigidbody;
    private CarAudioManager audioManager;

    private bool isPaused = false;
    private float raceTime = 0f;
    private const float maxRaceTime = 180f;
    private bool raceEnded = false;

    private CarController carController;

    // 🔁 Engine toggle variables
    public Image engineImage;                 // UI Image reference
    public Sprite engineOnSprite;            // Sprite for engine ON
    public Sprite engineOffSprite;           // Sprite for engine OFF
    private bool isEngineOn = false;         // Initially OFF

    void Start()
    {
        audioManager = FindObjectOfType<CarAudioManager>();
        carController = FindObjectOfType<CarController>();

        Time.timeScale = 1;
        settingsPanel.SetActive(false);
        ResetTimer();
        UpdateLapCounter();

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        closeButton.onClick.AddListener(CloseGame);

        if (soundToggle != null)
        {
            soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
            soundToggle.isOn = audioManager != null && audioManager.IsAudioEnabled();
        }

        // Set engine OFF sprite at start
        isEngineOn = false;
        UpdateEngineImage();
    }

    void Update()
    {
        if (raceEnded) return;

        // Toggle engine sprite on pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEngineOn = !isEngineOn;
            UpdateEngineImage();
        }

        if (!isPaused && carRigidbody != null && carRigidbody.velocity.magnitude > 0.1f)
        {
            if (raceTime < maxRaceTime)
            {
                raceTime += Time.deltaTime;
                UpdateTimerDisplay();
                UpdateLapCounter();
            }
            else
            {
                EndGame();
            }
        }
    }

    private void UpdateEngineImage()
    {
        if (engineImage != null && engineOnSprite != null && engineOffSprite != null)
        {
            engineImage.sprite = isEngineOn ? engineOnSprite : engineOffSprite;
            Debug.Log("Engine image updated to: " + (isEngineOn ? "ON" : "OFF"));
        }
        else
        {
            Debug.LogWarning("Engine image or sprites not assigned in inspector!");
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
        audioManager.PauseAudio();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
        audioManager.ResumeAudio();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ToggleSound()
    {
        if (audioManager != null)
        {
            audioManager.ToggleSound(soundToggle.isOn);
        }
    }

    public void UpdateLapCounter()
    {
        if (lapCounterText != null && carController != null)
        {
            lapCounterText.text = "Lap: " + carController.GetCurrentLap() + "/" + carController.GetTotalLaps();
        }
    }

    public void ResetTimer()
    {
        raceTime = 0f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            float remainingTime = Mathf.Max(0, maxRaceTime - raceTime);
            timerText.text = "Time: " + remainingTime.ToString("F2") + "s";
        }
    }

    private void EndGame()
    {
        raceEnded = true;
        raceTime = maxRaceTime;
        UpdateTimerDisplay();
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
    }
}

