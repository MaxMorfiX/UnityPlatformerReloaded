using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameUI : MonoBehaviour {
    [SerializeField]GameObject gameOverText;
    [SerializeField]GameObject levelCompletedText;
    [SerializeField]GameObject PABTR; //press any button to restart (text)
    [SerializeField]GameObject player; //press any button to restart (text)

    bool isGamePlaying = true;

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
          return;
     }

     if (Input.anyKeyDown) {
          restart();
          player.GetComponent<playerController>().restartGame();
        }
    }
}
