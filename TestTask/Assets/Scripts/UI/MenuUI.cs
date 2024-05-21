using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;


    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject audioPanel;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Slider slider;
    private void Start()
    {
        if (menuPanel == null)
            menuPanel = transform.GetChild(0).gameObject;

        if (settingsPanel == null)
            settingsPanel = transform.GetChild(1).gameObject;

        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        //Set volume settings
        Debug.LogError($"volume: {SoundManager.Instance.GetVolume()} | scaled: {(SoundManager.Instance.GetVolume() * 100)}");
        inputField.text = (SoundManager.Instance.GetVolume() * 100).ToString();
        slider.value = SoundManager.Instance.GetVolume() * 100;

        controlsPanel.SetActive(true);
        audioPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ShowSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }


    public void HideSettings()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }


    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        audioPanel.SetActive(false);
    }
    public void ShowAudioSettings()
    {
        controlsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }


    public void ChangeVolume_InputField()
    {
        ChangeValueFromInputField(inputField, slider);
        SoundManager.Instance.SetVolume((int)slider.value);
    }

    public void ChangeVolume_Slider()
    {
        ChangeValueFromSlider(slider, inputField);
        SoundManager.Instance.SetVolume((int)slider.value);
    }


    private void ChangeValueFromInputField(TMP_InputField textField, Slider slider)
    {
        int value;
        if (int.TryParse(textField.text, out value))
        {
            textField.text = value.ToString();
        }

        slider.value = value;

        Debug.Log(value);
    }
    private void ChangeValueFromSlider(Slider slider, TMP_InputField textField)
    {
        textField.text = ((int)slider.value).ToString();
    }

}
