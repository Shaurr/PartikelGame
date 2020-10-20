using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [SerializeField]
    private GameObject levelArea;
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private Canvas menu;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text wavetext;
    [SerializeField]
    private Text lifetext;
    private GameObject currentPlayer;
    private int wave;
    private int[] enemieCount = { 0, 0, 0 };
    private int enemiesInWave;

    // called on Game Over
    public void GameOver() {
        // deletes all Objects in scene
        spawner.DeleteEnemies();
        spawner.DeleteItems();
        Destroy(currentPlayer);

        // counts killed enemies in last wave
        int killedEnemies = enemiesInWave - enemieCount[0] - enemieCount[1] - enemieCount[2];
        // activate menu canvas
        levelArea.SetActive(false);
        menu.enabled = true;
        menu.GetComponent<CanvasController>().GameOver();
        // calculate scores, check highscore
        menu.GetComponent<Highscores>().CheckHighscore(wave, killedEnemies);
        Cursor.visible = true;
    }

    // starts game
    public void StartGame() {
        //activate Game screen
        levelArea.SetActive(true);
        Cursor.visible = false;
        //spawn player
        currentPlayer = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        spawner.SetPlayer(currentPlayer);
        // update ui
        UpdateLife(3);
        // start with wave 1
        wave = 1;
        NextWave();
    }

    // define next wave
    private void NextWave() {
        // update ui
        UpdateWave();
        //initialize
        enemieCount[0] = 0;
        enemieCount[1] = 0;
        enemieCount[2] = 0;
        enemiesInWave = 0;

        // define enemies in wave
        if (wave > 6) {

            int i = wave;
            // spawn health at wave 7 and then every 5 waves
            if ((wave - 7) % 5 == 0) {
                spawner.Spawn(0);
            }
            // spawn super on chance
            else {
                int k = Random.Range(0, 10);
                if (k < 2) {
                    spawner.Spawn(1);
                }
            }
            // split wavecount into enemiebuckets
            while (i >= 3) {
                int type = Random.Range(1, 4);
                enemieCount[type - 1]++;
                i -= type;
                enemiesInWave += 2;
            }
            if (i == 2) {
                int type = Random.Range(1, 3);
                if (type == 2) {
                    enemieCount[1]++;
                    enemiesInWave += 2;
                }
                else {
                    enemieCount[0] += 2;
                    enemiesInWave += 4;
                }
            }
            else if (i == 1) {
                enemieCount[0]++;
                enemiesInWave += 2;
            }
        }
        // pre defined tutorial waves
        else if (wave == 1) {
            enemieCount[0] = 2;
            enemiesInWave = 4;
        }
        else if (wave == 2) {
            enemieCount[1] = 2;
            enemiesInWave = 4;
        }
        else if (wave == 3) {
            enemieCount[0] = 1;
            enemieCount[1] = 1;
            enemiesInWave = 4;
        }
        else if (wave == 4) {
            enemieCount[0] = 2;
            enemieCount[2] = 1;
            enemiesInWave = 6;
        }
        else if (wave == 5) {
            enemieCount[1] = 2;
            enemieCount[2] = 1;
            enemiesInWave = 6;
        }
        else if (wave == 6) {
            enemieCount[0] = 1;
            enemieCount[1] = 1;
            enemieCount[2] = 1;
            enemiesInWave = 6;
        }

        // doubles enemies 
        for (int k = 0; k < 3; k++) {
            enemieCount[k] *= 2;
        }

        // tell spawner what to spawn
        spawner.SpawnEnemies(enemieCount);

    }

    // delete enemies on death
    public void DeleteEnemie(int type) {
        enemieCount[type]--;
        // when last enemie of wave die, start next on
        if ((enemieCount[0] == 0) && (enemieCount[1] == 0) && (enemieCount[2] == 0)) {
            wave++;
            StartCoroutine(TimeBetweenWaves());
        }

    }

    // waits short time befor new wave starts
    private IEnumerator TimeBetweenWaves() {

        yield return new WaitForSeconds(0.3f);
        NextWave();

    }

    // updates ui
    public void UpdateLife(int life) {
        lifetext.text = "Live: " + life;

    }

    //updates ui 
    public void UpdateWave() {
        wavetext.text = "Wave: " + wave;

    }

    // returns current player
    public GameObject GetCurrentPlayer() {
        return currentPlayer;
    }

}
