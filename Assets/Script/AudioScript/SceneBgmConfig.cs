using UnityEngine;

[CreateAssetMenu(fileName = "SceneBGMConfig", menuName = "Audio/Scene BGM Config")]
public class SceneBgmConfig : ScriptableObject
{
    public string sceneName;       // 场景名（如 "_01_StartGame"）
    public string bgmClipName;     // AudioManager 中定义的 BGM 名称（如 "bgmWelcome"）
    public bool shouldStopPrevious = true; // 是否在切换到该场景时停止上一个 BGM
}
