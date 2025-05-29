using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{
    private Player playerHealth;
    [SerializeField] private RawImage HealthbarTotal;
    [SerializeField] private RawImage HealthbarCurrent;

    private void Awake()
    {
        InvokeRepeating(nameof(UpdatePlayerRef), 0, 1);
    }

    private void Start()
    {
        HealthbarTotal.uvRect = new Rect(playerHealth.currentHealth / 10f, 0, 1, 1);
    }

    private void Update()
    {
        if (playerHealth != null)
            HealthbarCurrent.uvRect = new Rect(playerHealth.currentHealth / 10f, 0, 1, 1);
    }

    private void UpdatePlayerRef()
    {
        if (playerHealth == null)
            playerHealth = FindFirstObjectByType<Player>();
    }
}
