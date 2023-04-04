using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//Old project code from my Unity class
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject WinUI;

    public float threshold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    
    //public void MainScreen() {
    //    SceneManager.LoadScene("StartScreen");
   // }

    //public void Restart() {
   //     SceneManager.LoadScene("SampleScene");
    //}

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame() {
        Application.Quit();
    }

    //public void GameOver() {
    //    SceneManager.LoadScene("GameOverScene");
    //}

    public void WinGame()
    {
        Time.timeScale = 0;
    }

}