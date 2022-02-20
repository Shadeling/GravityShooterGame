using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MyManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] menusToCloseAfterDeath;

    [SerializeField] Text gameOverText;


    [SerializeField] Health playerHealth;
    [SerializeField] PlayerController playerController;


    private bool _pauseMenustate = false;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerHealth.onDie += OnPlayerDeath;
        playerController.escButtonDown += ChangePauseMenu;
        LoadSettings();
    }

    private void LoadSettings()
    {
        //Настройка звука
        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume");
        FindObjectOfType<Camera>().fieldOfView = PlayerPrefs.GetFloat("FovAngle");
    }



    private void OnPlayerDeath()
    {
        foreach (var item in menusToCloseAfterDeath)
        {
            item.SetActive(false);
        }
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gameOverText.text = "Вы проиграли!";
        gameOverMenu.SetActive(true);
    }

    // Update is called once per frame
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangePauseMenu()
    {
        _pauseMenustate = !_pauseMenustate;

        Time.timeScale = _pauseMenustate ? 0 : 1;
        Cursor.lockState = _pauseMenustate ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _pauseMenustate;

        pauseMenu.SetActive(_pauseMenustate);
    }

    public void OnWin() 
    {
        foreach (var item in menusToCloseAfterDeath)
        {
            item.SetActive(false);
        }
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gameOverText.text = "Поздравляем! Вы прошли первый уровень. Больше пока нету :(";
        gameOverMenu.SetActive(true);
    }
}
