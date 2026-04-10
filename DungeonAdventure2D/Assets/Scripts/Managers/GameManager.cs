using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Fruit")]
    public int fruitCollected;

    [Header("Star")]
    public int starCollected;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay;
    public Player player;
    private GameObject newPlayer;

    [Header("VFX")]
    [SerializeField] private GameObject deathVfx;

    [Header("Cinemachine Camera")]
    private CinemachineCamera cinemachineCamera;

    private float timeDelay = 0.5f; 

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
        if (newPlayer != null)
        {
            Destroy(newPlayer);
        }
        newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        cinemachineCamera.Follow = newPlayer.transform;
        player = newPlayer.GetComponent<Player>();
    }

    public void UpdateRespawnPoint(Transform RespawnPoint) => spawnPoint = RespawnPoint;

    public void SpawnPlayer() => StartCoroutine(SpawnCouroutine());
    private IEnumerator SpawnCouroutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        SetupPlayer();  
    }

    public void AddFruit()
    {
        fruitCollected++;
        UIingame.instance.UpdateFruitText(fruitCollected);
    }

    public void AddStar()
    {
        starCollected++;
        UIingame.instance.starSystemHUD.UpdateImageStar(starCollected);
    }

    public void Die()
    {
        GameObject newDeathVfx = Instantiate(deathVfx, newPlayer.transform.position, Quaternion.identity);
        newPlayer.SetActive(false); 
        StartCoroutine(ShowDeathUICouroutine());
        /* SpawnPlayer();*/
    }

    private IEnumerator ShowDeathUICouroutine()
    {
        yield return new WaitForSeconds(timeDelay);

        if (UIingame.instance != null)
        {
            UIingame.instance.ShowDeathUI(fruitCollected);
        }
    }
}
