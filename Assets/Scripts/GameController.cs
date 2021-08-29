using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public bool spawnPaused = true;
    public float spawnGapTime;
    public bool levelEnded = true;
    private GameObject enemyContainer;
    private GameInfoHolder gameInfoHolder;

    public void ChangePauseBehaviour()
    {
        spawnPaused = !spawnPaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    void SpawnEnemy(int lane)
    {
        GameObject gO = GameObject.Instantiate(gameInfoHolder.enemyInfoHolder.enemies[gameInfoHolder.currentLevelHolder.enemyIndex]);
        gO.transform.parent = enemyContainer.transform;
        gO.transform.position = new Vector3(100, 100, 100);
        gO.GetComponent<Enemy>().health = gameInfoHolder.currentLevelHolder.enemyHealth;
        gO.GetComponent<Enemy>().totalHealth = gameInfoHolder.currentLevelHolder.enemyHealth;
        gO.GetComponent<Enemy>().speed = gameInfoHolder.currentLevelHolder.enemySpeed;
        gO.GetComponent<Enemy>().path = gameInfoHolder.pathHolder.paths[lane];

    }

    IEnumerator SpawnLevel()
    {
        levelEnded = false;
        gameInfoHolder.currentLevelHolder.GenerateLevel();
        gameInfoHolder.currentLevelHolder.AdvanceLevel();

        float spawnWaitTime = 1.05f - (float)gameInfoHolder.currentLevelHolder.remainingEnemies * 0.005f;

        yield return new WaitForSeconds(spawnWaitTime);


        int lanes = gameInfoHolder.pathHolder.paths.Length;

        for(int i = 0; i < gameInfoHolder.currentLevelHolder.remainingEnemies; )
        {
            for (int j = 0; j < lanes; j++)
            {
                SpawnEnemy(j);
                gameInfoHolder.currentLevelHolder.remainingEnemies--;
                if (gameInfoHolder.currentLevelHolder.remainingEnemies == 0)
                    break;
            }
            yield return new WaitForSeconds(spawnGapTime);

        }

    }

    public int CalculatePoints()
    {
        int points = 0;
        points += gameInfoHolder.currentLevelHolder.level * gameInfoHolder.currentLevelHolder.level * 100;
        points += gameInfoHolder.statHolder.moneyGained;
        points += gameInfoHolder.statHolder.eliminatedOpponents * 20;

        return points;
    }

    public void FinishGame()
    {
        int points = CalculatePoints();
        int highPoints = PlayerPrefs.GetInt("Highscore",0);
        PlayerPrefs.SetInt("CurrentPoints", points);

        if (points > highPoints)
            PlayerPrefs.SetInt("Highscore", points);

        SceneManager.LoadScene("ScoreMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnPaused && levelEnded)
            StartCoroutine(SpawnLevel());
        if (enemyContainer.transform.childCount == 0 && gameInfoHolder.currentLevelHolder.remainingEnemies == 0)
            levelEnded = true;

        if (gameInfoHolder.statHolder.livesLeft <= 0)
            FinishGame();
    }
}
