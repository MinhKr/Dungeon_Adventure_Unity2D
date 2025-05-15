using UnityEngine;

public class UIOptionMenu : MonoBehaviour
{
    [SerializeField] private int curentSkinIndex;
    [SerializeField] private Animator skinDisplay;

    public void NextSkin()
    {
        curentSkinIndex++;
        if (curentSkinIndex > 3)
        {
            curentSkinIndex = 0;
        }
        UpdateSkinDisplay();
    }

    public void PreviousSkin()
    {
        curentSkinIndex--;
        if (curentSkinIndex < 0)
        {
            curentSkinIndex = 3;
        }
        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {
        for (int i = 0; i < skinDisplay.layerCount; i++)
        {
            skinDisplay.SetLayerWeight(i, 0);
        }

        skinDisplay.SetLayerWeight(curentSkinIndex, 1);
    }

    public void ChooseSkin()
    {
        SkinManager.instance.SetSkinIndex(curentSkinIndex);
    }
}
