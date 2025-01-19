using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Toggle muteToggle;
    public Slider volumeSlider;
    public Button closeButton;
    public Button mainMenuButton;
    public Button settingsButton;
    public GameObject settingsPanel;

    private void Start()
    {
        muteToggle.isOn = SettingsManager.Instance.IsMuted;
        volumeSlider.value = SettingsManager.Instance.Volume;

        muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);

        closeButton.onClick.AddListener(ToggleSettingsPanel);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);

        settingsPanel.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    private void OnMuteToggleChanged(bool isOn)
    {
        SettingsManager.Instance.IsMuted = isOn;
        UpdateAudio();
    }

    private void OnVolumeSliderChanged(float value)
    {
        SettingsManager.Instance.Volume = value;
        UpdateAudio();
    }

    private void UpdateAudio()
    {
        AudioSource audioSource = FindObjectOfType<BackgroundMusicPlayer>()?.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.mute = SettingsManager.Instance.IsMuted;
            audioSource.volume = SettingsManager.Instance.Volume;
        }
    }

    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerCanMove = isActive;
        }
    }
    
    private void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerCanMove = true;
        }
    }
    
 
    private void ReturnToMainMenu()
    {
        
        ToggleSettingsPanel();
        GameManager.Instance.ClearAllDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");
    }
}
