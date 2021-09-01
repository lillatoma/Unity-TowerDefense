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

    /// <summary>
    /// Pauses or unpaused the level spawning
    /// </summary>
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
    /// <summary>
    /// Spawns a regular enemy on a lane
    /// </summary>
    /// <param name="lane"></param>
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
    /// <summary>
    /// Spanws a boss on the bosslane
    /// Boss has 2x scale and immunity to freeze
    /// </summary>
    void SpawnBoss()
    {
        GameObject gO = GameObject.Instantiate(gameInfoHolder.enemyInfoHolder.enemies[gameInfoHolder.currentLevelHolder.enemyIndex]);
        gO.transform.parent = enemyContainer.transform;
        gO.transform.position = new Vector3(100, 100, 100);
        gO.transform.localScale = new Vector3(2f, 2f, 2f);
        gO.GetComponent<Enemy>().health = gameInfoHolder.currentLevelHolder.enemyHealth;
        gO.GetComponent<Enemy>().totalHealth = gameInfoHolder.currentLevelHolder.enemyHealth;
        gO.GetComponent<Enemy>().speed = gameInfoHolder.currentLevelHolder.enemySpeed;
        gO.GetComponent<Enemy>().path = gameInfoHolder.pathHolder.bossPath;
        gO.GetComponent<Enemy>().immune_to_freeze = true;
    }
    /// <summary>
    /// Manages the spawning of opponents in a level
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnLevel()
    {
        levelEnded = false;
        gameInfoHolder.currentLevelHolder.GenerateLevel();
        gameInfoHolder.currentLevelHolder.AdvanceLevel();

       

        yield return new WaitForSeconds(1f);


        int lanes = gameInfoHolder.pathHolder.paths.Length;

        if (gameInfoHolder.currentLevelHolder.IsBossRound())
        {
            for (int i = 0; i < gameInfoHolder.currentLevelHolder.remainingEnemies;)
            {
                SpawnBoss();
                gameInfoHolder.currentLevelHolder.remainingEnemies--;
                if (gameInfoHolder.currentLevelHolder.remainingEnemies == 0)
                    break;
            }
        }
        else
            for (int i = 0; i < gameInfoHolder.currentLevelHolder.remainingEnemies;)
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
    /// <summary>
    /// Changes the timescale of the game
    /// </summary>
    /// <param name="timeScale"></param>
    public void ChangeTimescale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
    /// <summary>
    /// Calculates the points earned for the current match
    /// </summary>
    /// <returns></returns>
    public int CalculatePoints()
    {
        int points = 0;
        points += gameInfoHolder.currentLevelHolder.level * gameInfoHolder.currentLevelHolder.level * 100;
        points += gameInfoHolder.statHolder.moneyGained;
        points += gameInfoHolder.statHolder.eliminatedOpponents * 20;

        return points;
    }
    /// <summary>
    /// Calculates the points for the current points
    /// Sets the highscore if it's beaten
    /// Loads the score menu
    /// </summary>
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
