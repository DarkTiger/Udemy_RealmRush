using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 120f)] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemiesText;
    [SerializeField] AudioClip spawnedEnemySFX;
    int score = 0;


    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
        spawnedEnemiesText.text = score.ToString();
    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyParentTransform);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        score++;
        spawnedEnemiesText.text = score.ToString();
    }
}
