using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject settings;
    public GameObject mainMenuPanel;
    public Button back;
    public AudioMixer mainMixer;
    public Slider general;
    public Slider music;
    public Slider sfx;
    public Button languageButton;
    public Text languageText;

    private List<string> languages = new List<string> { "English", "Turkish", "German" };
    private int currentLanguageIndex = 0;

    void Start()
    {
        back.onClick.AddListener(CloseSettingsPanel);
        general.onValueChanged.AddListener(SetGeneralVolume);
        music.onValueChanged.AddListener(SetMusicVolume);
        sfx.onValueChanged.AddListener(SetSfxVolume);
        languageButton.onClick.AddListener(ChangeLanguage);

        UpdateLanguageUI();
    }

    public void CloseSettingsPanel()
    {
        settings.SetActive(false);
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void SetGeneralVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void ChangeLanguage()
    {
        currentLanguageIndex = (currentLanguageIndex + 1) % languages.Count;
        UpdateLanguageUI();
    }

    private void UpdateLanguageUI()
    {
        if (languageText != null)
        {
            languageText.text = languages[currentLanguageIndex];
        }
    }
}
