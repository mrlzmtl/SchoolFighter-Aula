using Assets.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    public int numberOfEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSection;

        
    void Update()
    {
        if (currentEnemies >= numberOfEnemies)
        {
            int enemies = FindObjectsByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                LevelManager.ChangeSection(nextSection);

                this.gameObject.SetActive(false);
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition;

        spawnPosition.y = Random.Range(-0.95f, -0.36f);

        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        spawnPosition.x = rightSectionBound;

        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        currentEnemies++;

        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;

            SpawnEnemy();
        }
    }
}
