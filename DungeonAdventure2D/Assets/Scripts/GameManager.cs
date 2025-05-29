using System.Collections;
using Unity.Cinemachine;
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
    public Player player;
    private GameObject newPlayer;// use this to spawn the player

    [Header("VFX")]
    [SerializeField] private GameObject deathVfx;

    [Header("Cinemachine Camera")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //camera
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
    }

    private void Start()
    {
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        cinemachineCamera.Follow = newPlayer.transform;
    }

    public void UpdateRespawnPoint(Transform RespawnPoint) => spawnPoint = RespawnPoint;
    public void SpawnPlayer() => StartCoroutine(SpawnCouroutine());

    private IEnumerator SpawnCouroutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        SetupPlayer();
        player = newPlayer.GetComponent<Player>();
    }

    public void AddFruit()
    {
        fruitCollected++;
        UIingame.instance.UpdateFruitText(fruitCollected);
    }

    public void Die()
    {
        GameObject newDeathVfx = Instantiate(deathVfx, newPlayer.transform.position, Quaternion.identity);
        Destroy(newPlayer);
        SpawnPlayer();
    }
}
