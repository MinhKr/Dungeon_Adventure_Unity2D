using TMPro;
using UnityEngine;

public class UIingame : MonoBehaviour
{
    public static UIingame instance;

    [SerializeField] private TextMeshProUGUI fruitText;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void UpdateFruitText(int fruitCount)
    {
        if (fruitCount < 10)
            fruitText.text = "0" + fruitCount;
        else
            fruitText.text = fruitCount.ToString();
    }
}
