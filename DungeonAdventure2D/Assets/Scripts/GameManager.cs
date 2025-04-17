using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Fruit")]
    public int fruitCollected;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateRespawnPoint(Transform RespawnPoint) => spawnPoint = RespawnPoint;
    public void SpawnPlayer() => StartCoroutine(SpawnCouroutine());

    private IEnumerator SpawnCouroutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void AddFruit()
    {
        fruitCollected++;
    }
}
