using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameСontroller : MonoBehaviour {
    [SerializeField]GameObject gameOverText;
    [SerializeField]GameObject levelCompletedText;
    [SerializeField]GameObject PABTR; //press any button to restart (text)
    [SerializeField]GameObject player;
    [SerializeField]GameObject pauseMenuObj;
    
    bool isGamePlaying = true;
    bool isPaused = false;

    public void gameOver() {
         gameOverText.SetActive(true);
         PABTR.SetActive(true);

         isGamePlaying = false;
    }
    public void levelCompleted() {
         levelCompletedText.SetActive(true);
         PABTR.SetActive(true);

         isGamePlaying = false;
    }
    public void restart() {
         gameOverText.SetActive(false);
         levelCompletedText.SetActive(false);
         PABTR.SetActive(false);

         isGamePlaying = true;
    }

    private void Update() {

     if(isGamePlaying) {
          if(Input.GetKeyDown(KeyCode.Escape)) {
               togglePause();
          }

          return;
     }

     if (Input.anyKeyDown) {
          restart();
          player.GetComponent<playerController>().restartGame();
        }
    }

    public void togglePause() {
      isPaused = !isPaused;

        pauseMenuObj.SetActive(isPaused);

        if(isPaused)
          player.GetComponent<playerController>().freezePlayer();
        else {
          player.GetComponent<playerController>().unfreezePlayer(true);
     }

    }

    public void goToMainMenu() {
     SceneManager.LoadScene(0);
    }
}
