using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // === 音频剪辑 ===
    public AudioClip bgmWelcome;      // 主菜单 BGM
    public AudioClip bgmFight;        // 战斗场景 BGM
    public AudioClip sePlayerShoot;   // 玩家射击音效
    public AudioClip seEnemyShoot;    // 敌人射击音效
    public AudioClip explode;         // 爆炸音效
    public AudioClip seenemyhit;      // 敌人被击中音效
    public AudioClip buttonclick;    // 按钮点击音效
    public AudioClip playerDead;       // 玩家被击败音效
    public AudioClip playerWin;
    
    // === 音轨数组（可在 Unity Inspector 中分配 AudioSource 数量）===
    public AudioSource[] tracks;

    // === 单例模式 ===
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("✅ AudioManager created.");
        }
        else
        {
            Debug.LogWarning("⚠️ Duplicate AudioManager found!");
            Destroy(gameObject);
            return;
        }

        // 自动创建音轨（如果未在 Inspector 中设置）
        if (tracks == null || tracks.Length == 0)
        {
            int defaultTrackCount = 4;
            tracks = new AudioSource[defaultTrackCount];
            for (int i = 0; i < tracks.Length; i++)
            {
                tracks[i] = gameObject.AddComponent<AudioSource>();
            }
        }
        
        if (tracks[8] == null)
        {
            tracks[8].ignoreListenerPause = true; // 关键：不受 Time.timeScale 和 Pause 影响
        }
    }

    // === 播放音频方法 ===
    public void Play(int index, string clipName, bool loop)
    {
        if (index < 0 || index >= tracks.Length)
        {
            Debug.LogError($"AudioManager.Play: Index {index} out of range. Track count is {tracks.Length}");
            return;
        }

        AudioClip clip = GetAudioClip(clipName);
        if (clip != null && tracks[index] != null)
        {
            tracks[index].clip = clip;
            tracks[index].loop = loop;
            tracks[index].volume = 1f;
            tracks[index].Play();
        }

        Debug.Log($"Playing clip: {clipName} on track {index}");
        Debug.Log($"Track {index} clip: {tracks[index].clip?.name}, isPlaying: {tracks[index].isPlaying}");
    }

    // === 带淡入的播放方法 ===
    public void PlayWithFade(int index, string clipName, bool loop, float duration = 1f)
    {
        StartCoroutine(FadeTrackIn(index, clipName, loop, duration));
    }

    private IEnumerator FadeTrackIn(int index, string clipName, bool loop, float duration)
    {
        AudioClip clip = GetAudioClip(clipName);
        if (clip != null && tracks[index] != null)
        {
            tracks[index].clip = clip;
            tracks[index].loop = loop;
            tracks[index].volume = 0f;
            tracks[index].Play();

            Debug.Log($"[FadeTrackIn] Track {index} is now playing: {clip.name}");

            
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                tracks[index].volume = Mathf.Lerp(0f, 1f, elapsed / duration);
                yield return null;
            }

            tracks[index].volume = 1f;
        }
    }

    // === 带淡出的停止方法 ===
    public void StopWithFade(int index, float duration = 1f)
    {
        StartCoroutine(FadeTrackOut(index, duration));
    }

    private IEnumerator FadeTrackOut(int index, float duration)
    {
        if (tracks[index] == null || !tracks[index].isPlaying)
        {
            yield break;
        }

        float elapsed = 0f;
        float startVolume = tracks[index].volume;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            tracks[index].volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        tracks[index].Stop();
        tracks[index].volume = startVolume;
    }

    // === 根据名称获取音频剪辑 ===
    AudioClip GetAudioClip(string clipName)
    {
        switch (clipName)
        {
            case "bgmWelcome": return bgmWelcome;
            case "bgmFight": return bgmFight;
            case "seplayerfx01Shoot": return sePlayerShoot;
            case "seenemyfx14Shoot": return seEnemyShoot;
            case "explode": return explode;
            case "seenemyhit": return seenemyhit;
            case "buttonclick": return buttonclick;
            case "playerDead": return playerDead;
            case "playerWin": return playerWin;
            default:
                Debug.LogWarning($"AudioManager: AudioClip '{clipName}' not found.");
                return null;
        }
    }

    // === 停止所有音轨 ===
    public void StopAll()
    {
        foreach (var track in tracks)
        {
            if (track != null && track.isPlaying)
            {
                track.Stop();
                track.volume = 1f;
            }
        }
        
    }

    // === 停止特定音轨 ===
    public void Stop(int index)
    {
        if (index >= 0 && index < tracks.Length && tracks[index] != null && tracks[index].isPlaying)
        {
            tracks[index].Stop();
        }
    }
    
    public void StopAllWithFade(float duration = 1f)
{
    for (int i = 0; i < tracks.Length; i++)
    {
        if (tracks[i] != null && tracks[i].isPlaying)
        {
            StartCoroutine(FadeTrackOut(i, duration));
        }
    }
}
public void PlayOneShot(string clipName)
{
    AudioClip clip = GetAudioClip(clipName);
}


    
}
