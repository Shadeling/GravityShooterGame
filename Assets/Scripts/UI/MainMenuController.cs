using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;

    [SerializeField] Toggle tipsToggle;
    [SerializeField] Slider soundVolume;
    [SerializeField] Slider FOV;

    private void Start()
    {
        setDefaultOptions(false);
    }

    public void OptionsMenuTrigger(bool state)
    {
        optionMenu.SetActive(state);
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void saveTipsOption(bool tipChecked)
    {
        var tip = tipChecked ? 1 : 0;
        PlayerPrefs.SetInt("TipShow", tip);
    }

    public void saveSoundValue(float value)
    {
        //Debug.Log("Sound saved" + value);
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    public void saveFovValue(float value)
    {
        PlayerPrefs.SetFloat("FovAngle", value);
    }

    public void setDefaultOptions(bool forseDefault)
    {
        if(!PlayerPrefs.HasKey("TipShow") || forseDefault) PlayerPrefs.SetInt("TipShow", 1);
        if (!PlayerPrefs.HasKey("SoundVolume") || forseDefault) PlayerPrefs.SetFloat("SoundVolume", 0.5f);
        if (!PlayerPrefs.HasKey("FovAngle") || forseDefault) PlayerPrefs.SetFloat("FovAngle", 90f);


        tipsToggle.isOn = PlayerPrefs.GetInt("TipShow") != 0;
        soundVolume.value = PlayerPrefs.GetFloat("SoundVolume");
        FOV.value = PlayerPrefs.GetFloat("FovAngle");
    }
}
