using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public TMPro.TextMeshProUGUI volumeValueText;
    public TMPro.TextMeshProUGUI sensitivityValueText;

    public void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        float temp = volumeSlider.value * 100;
        volumeValueText.text = temp.ToString("0.0");
    }
    public void SetMouseSensitivity()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
        sensitivityValueText.text = sensitivitySlider.value.ToString("0.0");
    }

}
