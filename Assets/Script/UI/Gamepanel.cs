using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamepanel : MonoBehaviour
{
    public GameObject gameOverPanel;
    public PlayerControl playerControl;
    public Button restartButton;
    public Button winRestartButton;
    public GameObject wavepanel;
    public GameObject winpanel;
    public Text waveText;
    public EnemyGeneration enemyGeneration;

    [SerializeField] private float slowMotionScale = 0.6f; // 慢动作速度
    [SerializeField] private float slowMotionDuration = 2f; // 过渡时长
    private bool hasPlayedDeathSound = false;
    private bool hasPlayedWinSound = false;


    private void Start()
    {
        wavepanel.SetActive(true);
        gameOverPanel.SetActive(false);
        winpanel.SetActive(false);
        restartButton.onClick.AddListener(Restart);
        winRestartButton.onClick.AddListener(Restart);

        var player = GameManager.Instance.GetGenPlayer();
        if (player != null) playerControl = player.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (playerControl.hp <= 0 && !hasPlayedDeathSound)
        {
            GameManager.Instance.audioManager.StopAll();
            wavepanel.SetActive(false);
            gameOverPanel.SetActive(true);
            playerControl.gameObject.SetActive(false);
            playerControl.enabled = false;
            playerControl.isplayerDead = true;
            hasPlayedDeathSound = true;
            GameManager.Instance.audioManager.Play(8,"playerDead",false);
            StartCoroutine(SlowMotionToPause());
        }

        if (enemyGeneration.enemys.Length == 0
            && !hasPlayedWinSound && enemyGeneration.enemycount == enemyGeneration.enemyGenerateAll
            )
        {
            GameManager.Instance.audioManager.StopAll();
            wavepanel.SetActive(false);
            winpanel.SetActive(true);
            playerControl.gameObject.SetActive(false);
            playerControl.isplayerDead = true;
            playerControl.enabled = false;
            GameManager.Instance.audioManager.Play(8,"playerWin",false);
            hasPlayedWinSound = true;
            StartCoroutine(SlowMotionToPause());
        }
    }

    private IEnumerator SlowMotionToPause()
    {
        var elapsedTime = 0f;
        var startScale = Time.timeScale;
        var targetScale = 0f;

        while (elapsedTime < slowMotionDuration)
        {
            // 设置为慢动作
            Time.timeScale = Mathf.Lerp(startScale, slowMotionScale, elapsedTime / slowMotionDuration);
            elapsedTime += Time.unscaledDeltaTime; // 使用 unscaledDeltaTime 确保协程不受影响
            yield return null;
        }


        // 最后完全暂停
        Time.timeScale = targetScale;
    }

    public void Restart()
    {
        CancelInvoke();
        Time.timeScale = 1f;
        GameManager.Instance.audioManager.Play(5, "buttonclick", false);
        GameManager.Instance.LoadScene_01_StartGame();
    }
}