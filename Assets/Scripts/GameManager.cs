using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Old project code from my Unity class
public class GameManager : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SkillsMenuUI;
    public SkillGenerator skillGenerator;
    public bool isFullScreen = true;
    public Dropdown resolutionDropdown;
    [SerializeField] GameObject boss;
    Resolution[] resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }   
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(boss.GetComponent<EnemyBehavior>().currentHealth < 0)
            WinGame();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SkillPause()
    {
        skillGenerator.generate();
        SkillsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SkillUnpause()
    {
        GameIsPaused = false;
        SkillsMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1f;
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameIsPaused = false;
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void GameOver() {
       SceneManager.LoadScene("LoseScreen");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //Credit to Brackeys for menus: https://www.youtube.com/watch?v=JivuXdrIHK0
    //                              https://youtu.be/YOaYQrN1oYQ
}