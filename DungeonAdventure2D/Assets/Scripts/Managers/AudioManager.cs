using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] sfx;

    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private int bgmIndex;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        InvokeRepeating(nameof(PlayBgmIfNeeded), 0, 2);
    }

    public void PlayBgmIfNeeded()
    {
        if (bgm[bgmIndex].isPlaying == false)
            RandomBGM();
    }

    public void RandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int bgmIndexToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();

        bgmIndex = bgmIndexToPlay;
        bgm[bgmIndexToPlay].Play();
    }

    public void PlaySFX(int sfxIndexToPlay)
    {
        if (sfxIndexToPlay >= sfx.Length)
            return;

        sfx[sfxIndexToPlay].pitch = Random.Range(1.2f, 1.4f);
        sfx[sfxIndexToPlay].Play();
    }

    public void StopSFX(int sfxIndexToStop) => sfx[sfxIndexToStop].Stop();
}

