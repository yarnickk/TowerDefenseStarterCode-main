using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();

                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("SoundManager");
                    _instance = managerObject.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    // AudioSource components
    public AudioSource menuMusic;
    public AudioSource gameMusic;

    // UI sounds
    public GameObject audioSourcePrefab;
    public AudioClip[] uiSounds;

    // Tower sounds
    public AudioClip[] towerSounds;

    // FX sounds
    public enum FXType { GameStart, WaveStart, EnemyReachEnd, GameWin, GameLose, TowerBuild }
    public AudioClip[] fxSounds;

    // Start menu music and stop game music
    public void StartMenuMusic()
    {
        menuMusic.Play();
        gameMusic.Stop();
    }

    // Start game music and stop menu music
    public void StartGameMusic()
    {
        gameMusic.Play();
        menuMusic.Stop();
    }

    // Play a UI sound
    public void PlayUISound()
    {
        int index = Random.Range(0, uiSounds.Length);
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.clip = uiSounds[index];
        audioSource.Play();
        Destroy(soundGameObject, uiSounds[index].length);
    }

    // Play a tower sound based on tower type
    public void PlayTowerSound(int towerType)
    {
        if (towerType >= 0 && towerType < towerSounds.Length)
        {
            AudioSource.PlayClipAtPoint(towerSounds[towerType], Camera.main.transform.position);
        }
    }

    // Play a FX sound based on FX type
    public void PlayFXSound(FXType fxType)
    {
        switch (fxType)
        {
            case FXType.GameStart:
                if (fxSounds.Length > 0)
                    AudioSource.PlayClipAtPoint(fxSounds[0], Camera.main.transform.position);
                break;
            case FXType.WaveStart:
                if (fxSounds.Length > 1)
                    AudioSource.PlayClipAtPoint(fxSounds[1], Camera.main.transform.position);
                break;
            case FXType.EnemyReachEnd:
                if (fxSounds.Length > 2)
                    AudioSource.PlayClipAtPoint(fxSounds[2], Camera.main.transform.position);
                break;
            case FXType.GameWin:
                if (fxSounds.Length > 3)
                    AudioSource.PlayClipAtPoint(fxSounds[3], Camera.main.transform.position);
                break;
            case FXType.GameLose:
                if (fxSounds.Length > 4)
                    AudioSource.PlayClipAtPoint(fxSounds[4], Camera.main.transform.position);
                break;
            case FXType.TowerBuild:
                if (fxSounds.Length > 5)
                    AudioSource.PlayClipAtPoint(fxSounds[5], Camera.main.transform.position);
                break;
        }
    }
}
