using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] sfx;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlaySFX(int sfxIndexToPlay)
    {
        if (sfxIndexToPlay >= sfx.Length)
            return;

        sfx[sfxIndexToPlay].pitch = Random.Range(1.2f ,1.4f);
        sfx[sfxIndexToPlay].Play();
    }

    public void StopSFX(int sfxIndexToStop) => sfx[sfxIndexToStop].Stop();
}

