using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBGMManager : MonoBehaviour
{
    public static SceneBGMManager Instance;
    private PlayerControl  _playerControl;
    
    private Coroutine autoCheckCoroutine; // 用于管理 AutoCheckTrack 协程
    private string currentSceneName = "";  // 记录当前场景名称


    [SerializeField] private SceneBgmConfig[] sceneBGMConfigs;

    private string currentBGMClipName = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    currentSceneName = scene.name;
    PlayBGMForScene(scene.name);

    if (scene.name == "_03_Battle01")
    {
        var player = GameManager.Instance.GetGenPlayer();
        if (player != null)
        {
            _playerControl = player.GetComponent<PlayerControl>();
        }
        else
        {
            _playerControl = null;
        }
    }
    else
    {
        _playerControl = null;
    }
}


        private void PlayBGMForScene(string sceneName)
    {
        foreach (var config in sceneBGMConfigs)
        {
            if (config.sceneName == sceneName)
            {
                if (config.shouldStopPrevious && currentBGMClipName != config.bgmClipName)
                {
                    StopCurrentBGM();
                }

                if (currentBGMClipName == config.bgmClipName)
                {
                    ForceResumeBGM(); // 如果已经播放相同 BGM，强制恢复播放
                    return;
                }

                // 播放新的 BGM
                GameManager.Instance.audioManager.PlayWithFade(0, config.bgmClipName, true, 1f);
                currentBGMClipName = config.bgmClipName;

                
                // 仅在玩家存在且未死亡时启动协程
                if (_playerControl != null && !_playerControl.isplayerDead)
                {
                    autoCheckCoroutine = StartCoroutine(AutoCheckTrack(0, config.bgmClipName, true));
                }
                else
                {
                    StartCoroutine(AutoCheckTrack(0, config.bgmClipName, true));
                    Debug.Log("[SceneBGMManager] Skipping AutoCheckTrack: Player is null or dead.");
                }
                
                // // 启动自动检测协程，防止播着播着突然没声
                
                

                Debug.Log($"[SceneBGMManager] Playing BGM: {config.bgmClipName}");
                return;
            }
        }

        Debug.LogWarning($"No BGM config found for scene: {sceneName}");
    }


    private void StopCurrentBGM()
    {
        if (GameManager.Instance.audioManager.tracks[0] != null && GameManager.Instance.audioManager.tracks[0].isPlaying)
        {
            GameManager.Instance.audioManager.StopWithFade(0, 1f);
        }
    }
    
    private IEnumerator DelayedLoadScene(string sceneName)
{
    yield return SceneManager.LoadSceneAsync(sceneName);
    PlayBGMForScene(sceneName); 
}


    // 可选：手动触发播放某个场景的 BGM
    public void PlayBGMForSceneName(string sceneName)
    {
        PlayBGMForScene(sceneName);
    }
    
    
    private void ForceResumeBGM()
{
    var track = GameManager.Instance.audioManager.tracks[0];

    if (track != null && track.clip != null && !track.isPlaying)
    {
        Debug.LogWarning($"[SceneBGMManager] Track 0 is not playing. Forcing resume...");
        track.Play();
    }
}
    
    private IEnumerator AutoCheckTrack(int index, string clipName, bool loop, float checkInterval = 1f)
{
    while (true)
    {

        if (_playerControl != null && _playerControl.isplayerDead)
        {
            Debug.Log("[AutoCheckTrack] Scene or player invalid. Stopping.");
            yield break;
        }
        
        
        var track = AudioManager.Instance.tracks[index];

        if (track == null || track.clip == null || !track.isPlaying)
        {
            Debug.LogWarning($"[AutoCheckTrack] Track {index} stopped unexpectedly. Restarting...");
            AudioManager.Instance.PlayWithFade(index, clipName, loop);
        }

        yield return new WaitForSeconds(checkInterval);
    }
}

    


    
}
