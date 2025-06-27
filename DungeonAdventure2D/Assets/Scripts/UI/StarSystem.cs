using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{

    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite starOnSprite;
    [SerializeField] private Sprite starOffSprite;

    public void UpdateImageStar(int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = i < starCount ? starOnSprite : starOffSprite;
        }
    }
}
