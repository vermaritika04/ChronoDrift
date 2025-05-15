//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI; // Make sure to include this for UI elements

//public class CarAudioManager : MonoBehaviour
//{
//    // Variables for audio clips
//    public AudioClip startAudio;
//    public AudioClip idleAudio;

//    // AudioSource component
//    private AudioSource audioSource;

//    // State flags for audio management
//    private bool isAudioPlaying = false;
//    private bool isAudioEnabled = true; // To manage audio toggle state

//    // Reference to the Sound Toggle UI element
//    public Toggle soundToggle; // Assign this in the Inspector

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Add and setup the AudioSource component
//        audioSource = gameObject.AddComponent<AudioSource>();
//        audioSource.playOnAwake = false; // Prevent auto-play on awake
//        audioSource.loop = false;  // Ensure looping is disabled by default

//        // Setup sound toggle listener
//        if (soundToggle != null)
//        {
//            soundToggle.onValueChanged.AddListener(delegate {
//                ToggleSound(soundToggle.isOn);
//            });
//            soundToggle.isOn = true; // Default state to playing audio
//        }
//    }

//    void Update()
//    {
//        // Start audio with the E key
//        if (Input.GetKeyDown(KeyCode.E) && !isAudioPlaying && isAudioEnabled)
//        {
//            PlayStartAudio();
//        }

//        // Stop audio briefly with the Space key
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            StartCoroutine(StopAudioAfterDelay(0.01f));
//        }

//        // Resume audio if moving or using arrow keys
//        if ((isAudioPlaying && Input.GetAxis("Vertical") != 0 && isAudioEnabled) ||
//            (Input.GetKeyDown(KeyCode.UpArrow) ||
//             Input.GetKeyDown(KeyCode.DownArrow) ||
//             Input.GetKeyDown(KeyCode.LeftArrow) ||
//             Input.GetKeyDown(KeyCode.RightArrow)))
//        {
//            PlayAudioIfEnabled();
//        }
//    }

//    internal bool IsAudioEnabled()
//    {
//        throw new NotImplementedException();
//    }

//    // Coroutine to stop audio after a specified delay
//    private IEnumerator StopAudioAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        StopAudio(); // Stop the audio after the delay
//    }

//    // Method to play the starting audio
//    private void PlayStartAudio()
//    {
//        if (!isAudioEnabled) return; // do not play audio if audio is disabled

//        isAudioPlaying = true; // Set the playing state
//        audioSource.clip = startAudio; // Assign the start audio clip
//        audioSource.Play(); // Play the start audio

//        // After the start audio finishes playing, switch to idle audio
//        StartCoroutine(PlayIdleAudioAfterStart());
//    }

//    // Coroutine to transition to idle audio after start audio finishes
//    private IEnumerator PlayIdleAudioAfterStart()
//    {
//        // Wait for the duration of the start audio
//        yield return new WaitForSeconds(startAudio.length);

//        // Set and play idle audio
//        audioSource.clip = idleAudio;
//        audioSource.loop = true;  // Set idle audio to loop
//        audioSource.Play(); // Play idle audio
//    }

//    // Method to stop the audio
//    private void StopAudio()
//    {
//        audioSource.Stop(); // Stop any audio playing
//        isAudioPlaying = false; // Reset the playing state
//        audioSource.loop = false; // Disable looping afterwards
//    }

//    // Method to toggle sound on and off
//    public void ToggleSound(bool isOn)
//    {
//        isAudioEnabled = isOn; // Update audio enable state

//        if (isOn)
//        {
//            PlayAudioIfEnabled(); // Restart audio if enabled
//        }
//        else
//        {
//            StopAudio(); // Stop audio if disabled
//        }
//    }

//    // Method to play audio if sound is enabled
//    public void PlayAudioIfEnabled()
//    {
//        if (isAudioEnabled && !isAudioPlaying)
//        {
//            PlayStartAudio();
//        }
//    }
//}
//27.3
//using System;
//using UnityEngine;

//public class CarAudioManager : MonoBehaviour
//{
//    // Audio clips for different sounds
//    public AudioClip engineStartClip;
//    public AudioClip drivingClip;
//    public AudioClip engineStopClip;

//    // AudioSource components for each sound
//    private AudioSource engineStartSource;
//    private AudioSource drivingSource;
//    private AudioSource engineStopSource;

//    // State flags for audio management
//    private bool isEngineRunning = false;
//    private bool isAudioEnabled = true; // To manage audio toggle state

//    void Start()
//    {
//        // Create and setup AudioSource components
//        engineStartSource = gameObject.AddComponent<AudioSource>();
//        drivingSource = gameObject.AddComponent<AudioSource>();
//        engineStopSource = gameObject.AddComponent<AudioSource>();

//        // Assign audio clips to the AudioSources
//        engineStartSource.clip = engineStartClip;
//        drivingSource.clip = drivingClip;
//        engineStopSource.clip = engineStopClip;

//        // Ensure no audio plays on scene load
//        StopAudio();
//    }

//    void Update()
//    {
//        // Check for input to start/stop engine
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            if (!isEngineRunning && isAudioEnabled)
//            {
//                StartEngine();
//            }
//            else if (isEngineRunning && isAudioEnabled)
//            {
//                StopEngine();
//            }
//        }

//        // Play driving sound when moving
//        if (isEngineRunning && isAudioEnabled &&
//            (Input.GetKey(KeyCode.UpArrow) ||
//             Input.GetKey(KeyCode.DownArrow) ||
//             Input.GetKey(KeyCode.LeftArrow) ||
//             Input.GetKey(KeyCode.RightArrow)))
//        {
//            PlayDrivingSound();
//        }
//        else
//        {
//            StopDrivingSound();
//        }
//    }

//    internal object PlayAudioIfEnabled()
//    {
//        throw new NotImplementedException();
//    }

//    private void StartEngine()
//    {
//        if (!isAudioEnabled) return; // Do not play audio if audio is disabled

//        isEngineRunning = true; // Set the engine running state
//        engineStartSource.Play(); // Play the engine start sound
//    }

//    private void StopEngine()
//    {
//        if (!isAudioEnabled) return; // Do not play audio if audio is disabled

//        if (isEngineRunning) // Ensure the engine is running before stopping
//        {
//            isEngineRunning = false; // Reset the engine running state
//            engineStopSource.Play(); // Play the engine stop sound
//        }
//    }

//    private void PlayDrivingSound()
//    {
//        if (!drivingSource.isPlaying && isAudioEnabled)
//        {
//            drivingSource.Play(); // Play the driving sound
//        }
//    }

//    private void StopDrivingSound()
//    {
//        if (drivingSource.isPlaying)
//        {
//            drivingSource.Stop(); // Stop the driving sound
//        }
//    }

//    public void ToggleSound(bool isOn)
//    {
//        isAudioEnabled = isOn; // Update audio enable state

//        if (isOn)
//        {
//            if (isEngineRunning)
//            {
//                PlayDrivingSound(); // Ensure driving sound is playing
//            }
//            // No need to restart the engine sound; it will play when "E" is pressed
//        }
//        else
//        {
//            StopEngine(); // Stop engine sound if disabled
//            StopDrivingSound(); // Stop driving sound if disabled
//        }
//    }

//    public bool IsAudioEnabled()
//    {
//        return isAudioEnabled;
//    }

//    public void StopAudio()
//    {
//        StopEngine();
//        StopDrivingSound();
//    }
//}



//using System;
//using UnityEngine;

//public class CarAudioManager : MonoBehaviour
//{
//    // Audio clips for different sounds
//    public AudioClip engineStartClip;
//    public AudioClip drivingClip;

//    // AudioSource components for each sound
//    private AudioSource engineStartSource;
//    private AudioSource drivingSource;

//    // State flags for audio management
//    private bool isEngineRunning = false;
//    private bool isAudioEnabled = true; // To manage audio toggle state
//    private bool isPaused = false; // To track if audio is paused

//    void Start()
//    {
//        // Create and setup AudioSource components
//        engineStartSource = gameObject.AddComponent<AudioSource>();
//        drivingSource = gameObject.AddComponent<AudioSource>();

//        // Assign audio clips to the AudioSources
//        engineStartSource.clip = engineStartClip;
//        drivingSource.clip = drivingClip;

//        // Ensure no audio plays on scene load
//        StopAudio();
//    }

//    void Update()
//    {
//        // Check for input to start/stop engine
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            if (!isEngineRunning && isAudioEnabled)
//            {
//                StartEngine();
//            }
//            else if (isEngineRunning && isAudioEnabled)
//            {
//                StopEngine();
//            }
//        }

//        // Play driving sound when moving
//        if (isEngineRunning && isAudioEnabled && !isPaused)
//        {
//            PlayDrivingSound();
//        }
//        else
//        {
//            StopDrivingSound();
//        }
//    }

//    public void ResumeAudio()
//    {
//        isPaused = false; // Set paused state to false
//        if (isEngineRunning && isAudioEnabled)
//        {
//            PlayDrivingSound(); // Resume driving sound if the engine is running
//        }
//    }

//    public void PauseAudio()
//    {
//        isPaused = true; // Set paused state to true
//        StopDrivingSound(); // Stop driving sound when paused
//    }

//    private void StartEngine()
//    {
//        if (!isAudioEnabled) return; // Do not play audio if audio is disabled

//        isEngineRunning = true; // Set the engine running state
//        engineStartSource.Play(); // Play the engine start sound
//        Invoke("PlayDrivingSound", engineStartClip.length); // Start driving sound after engine start sound
//    }

//    private void StopEngine()
//    {
//        if (!isAudioEnabled) return; // Do not play audio if audio is disabled

//        if (isEngineRunning) // Ensure the engine is running before stopping
//        {
//            isEngineRunning = false; // Reset the engine running state
//            // Removed engine stop sound
//        }
//    }

//    private void PlayDrivingSound()
//    {
//        if (!drivingSource.isPlaying && isAudioEnabled)
//        {
//            drivingSource.Play(); // Play the driving sound
//        }
//    }

//    private void StopDrivingSound()
//    {
//        if (drivingSource.isPlaying)
//        {
//            drivingSource.Stop(); // Stop the driving sound
//        }
//    }

//    public void ToggleSound(bool isOn)
//    {
//        isAudioEnabled = isOn; // Update audio enable state

//        if (isOn)
//        {
//            if (isEngineRunning)
//            {
//                PlayDrivingSound(); // Ensure driving sound is playing
//            }
//        }
//        else
//        {
//            StopEngine(); // Stop engine state if disabled
//            StopDrivingSound(); // Stop driving sound if disabled
//        }
//    }

//    public bool IsAudioEnabled()
//    {
//        return isAudioEnabled;
//    }

//    public void StopAudio()
//    {
//        isEngineRunning = false; // Reset engine running state
//        StopDrivingSound();
//    }
//}

//conditional sound volume adjustment

using System;
using UnityEngine;

public class CarAudioManager : MonoBehaviour
{
    public AudioClip engineStartClip;
    public AudioClip drivingClip;

    private AudioSource engineStartSource;
    private AudioSource drivingSource;

    private bool isEngineRunning = false;
    private bool isAudioEnabled = true;
    private bool isPaused = false;

    public Rigidbody carRigidbody; // Assign this in Inspector or via code

    [Range(0f, 1f)]
    public float minVolume = 0.2f;
    [Range(0f, 1f)]
    public float maxVolume = 1.0f;

    public float lowSpeedThreshold = 30f;   // Idle to normal transition
    public float highSpeedThreshold = 90f;  // Max volume at this speed

    void Start()
    {
        engineStartSource = gameObject.AddComponent<AudioSource>();
        drivingSource = gameObject.AddComponent<AudioSource>();

        engineStartSource.clip = engineStartClip;
        drivingSource.clip = drivingClip;

        drivingSource.loop = true; // Loop the driving sound

        StopAudio();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isEngineRunning && isAudioEnabled)
                StartEngine();
            else if (isEngineRunning && isAudioEnabled)
                StopEngine();
        }

        if (isEngineRunning && isAudioEnabled && !isPaused)
        {
            PlayDrivingSound();
            AdjustDrivingSoundVolume();
        }
        else
        {
            StopDrivingSound();
        }
    }

    private void AdjustDrivingSoundVolume()
    {
        if (carRigidbody == null || !drivingSource.isPlaying) return;

        float speed = carRigidbody.velocity.magnitude * 3.6f; // Convert to km/h

        if (speed <= 0.5f)
        {
            drivingSource.volume = 0f;
        }
        else if (speed <= lowSpeedThreshold)
        {
            drivingSource.volume = minVolume;
        }
        else if (speed <= highSpeedThreshold)
        {
            float t = (speed - lowSpeedThreshold) / (highSpeedThreshold - lowSpeedThreshold);
            drivingSource.volume = Mathf.Lerp(minVolume, maxVolume, t);
        }
        else
        {
            drivingSource.volume = maxVolume;
        }
    }

    public void ResumeAudio()
    {
        isPaused = false;
        if (isEngineRunning && isAudioEnabled)
        {
            PlayDrivingSound();
        }
    }

    public void PauseAudio()
    {
        isPaused = true;
        StopDrivingSound();
    }

    private void StartEngine()
    {
        if (!isAudioEnabled) return;

        isEngineRunning = true;
        engineStartSource.Play();
        Invoke(nameof(PlayDrivingSound), engineStartClip.length);
    }

    private void StopEngine()
    {
        if (!isAudioEnabled) return;

        if (isEngineRunning)
        {
            isEngineRunning = false;
        }
    }

    private void PlayDrivingSound()
    {
        if (!drivingSource.isPlaying && isAudioEnabled)
        {
            drivingSource.Play();
        }
    }

    private void StopDrivingSound()
    {
        if (drivingSource.isPlaying)
        {
            drivingSource.Stop();
        }
    }

    public void ToggleSound(bool isOn)
    {
        isAudioEnabled = isOn;

        if (isOn)
        {
            if (isEngineRunning)
            {
                PlayDrivingSound();
            }
        }
        else
        {
            StopEngine();
            StopDrivingSound();
        }
    }

    public bool IsAudioEnabled()
    {
        return isAudioEnabled;
    }

    public void StopAudio()
    {
        isEngineRunning = false;
        StopDrivingSound();
    }
}

