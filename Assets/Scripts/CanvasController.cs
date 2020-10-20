using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {


    [SerializeField]
    private GameObject[] screens;
    private GameObject activeScreen;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private InputField playerName;

    [SerializeField]
    private Highscores scores;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text nameText;

    private Canvas menu;

    
    // initialize
    void Start() {
        activeScreen = screens[0];
        menu = this.GetComponent<Canvas>();
        // read Highscores and display them
        scores.ReadScores();
        UpdateScore();
    }

    // updates Highscorelist
    private void UpdateScore() {
        scoreText.text = scores.GetNumber();
        nameText.text = scores.GetName();
    }
   
    // switches to panel with buttonNr on buttonpress
    public void OnButtonPressed(int buttonNr) {
        activeScreen.SetActive(false);
        activeScreen = screens[buttonNr];
        activeScreen.SetActive(true);
    }

    // closes game
    public void ExitButton() {
        Application.Quit();
    }

    // starts game on buttonpress
    public void PlayButton() {
        activeScreen.SetActive(false);
        menu.enabled = false;
        scores.SetName(playerName.text);
        gameController.StartGame();
    }

    // shows GameOverscreen
    public void GameOver() {
        activeScreen = screens[3];
        activeScreen.SetActive(true);
        Cursor.visible = true;
        UpdateScore();
    }

}
