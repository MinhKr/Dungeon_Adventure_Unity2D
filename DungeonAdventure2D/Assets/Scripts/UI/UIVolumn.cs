using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIVolumn : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;  
    private float mixerMultiplier = 25;

    [Header("Background Music")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParameter;

    [Header("Sound Effect")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParameter;

    public void BGMSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParameter, newValue);
    }
    public void SFXSliderValue(float value)
    {
        float newValue = Mathf.Log10(value) * mixerMultiplier; 
        audioMixer.SetFloat(sfxParameter, newValue);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParameter, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParameter, bgmSlider.value);
        PlayerPrefs.Save();
    }
    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParameter, .7f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParameter, .7f);
    }
}
