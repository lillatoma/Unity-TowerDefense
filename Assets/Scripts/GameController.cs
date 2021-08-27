using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return new WaitForSeconds(1f);


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

    // Update is called once per frame
    void Update()
    {
        if (!spawnPaused && levelEnded)
            StartCoroutine(SpawnLevel());
        if (enemyContainer.transform.childCount == 0 && gameInfoHolder.currentLevelHolder.remainingEnemies == 0)
            levelEnded = true;
    }
}
