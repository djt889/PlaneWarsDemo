using UnityEditor;
using UnityEngine;

public class AudioManagerChecker : EditorWindow
{
    [MenuItem("Tools/AudioManager Checker")]
    public static void ShowWindow()
    {
        GetWindow<AudioManagerChecker>("AudioManager Checker");
    }

    private void OnGUI()
    {
        GUILayout.Label("AudioManager Status", EditorStyles.boldLabel);

        if (GUILayout.Button("Check AudioManager"))
        {
            CheckStatus();
        }
    }


    private void CheckStatus()
    {
        var audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("❌ AudioManager instance is null!");
            return;
        }

        Debug.Log("✅ AudioManager exists.");

        for (int i = 0; i < audioManager.tracks.Length; i++)
        {
            var track = audioManager.tracks[i];

            if (track == null)
            {
                Debug.LogWarning($"⚠️ Track {i} is null!");
                continue;
            }

            if (track.volume <= 0f && track.isPlaying)
            {
                Debug.LogWarning($"⚠️ Track {i} is playing but volume is 0. Fixing...");
                track.volume = 1f;
            }

            Debug.Log($"Track {i}: Playing={track.isPlaying}, Volume={track.volume}, Clip={track.clip?.name}");
        }
    }
}
