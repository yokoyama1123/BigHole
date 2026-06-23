using System.Collections;
using TMPro;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    [Header("音声設定")]
    [Tooltip("再生する音声クリップ")]
    public AudioClip soundClip;

    [Header("遅延時間の範囲（秒）")]
    [Tooltip("最小遅延時間（整数）")]
    public int minDelay = 1;
    [Tooltip("最大遅延時間（整数）")]
    public int maxDelay = 3;

    [Header("表示の変更（変更するテキストと変更後の文）")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private string message = "何ｍ？";

    [Header("入力欄の表示")]
    [SerializeField] private GameObject textMeshPro;

    [Header("ランダム秒数の記録")]
    [SerializeField] private SaveData savedata;

    private AudioSource audioSource;
    private bool hasTriggered = false;
    private Coroutine currentCoroutine = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.clip = soundClip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone") && !hasTriggered)
        {
            hasTriggered = true;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(PlaySoundAfterRandomDelay());
        }
    }

    IEnumerator PlaySoundAfterRandomDelay()
    {
        // 整数のランダム秒数（minDelay ? maxDelay の範囲）
        int delay = Random.Range(minDelay, maxDelay + 1);

        Debug.Log($"Stoneに接触！ {delay}秒後に音を鳴らします");

        yield return new WaitForSeconds(delay);

        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
            Debug.Log($"遅延 {delay} 秒後に音を再生しました");

            //秒数を記録
            savedata.SetSecond = delay;

            SetText();
        }
        else
        {
            Debug.LogWarning("AudioSourceまたはAudioClipが設定されていません。");
        }

        currentCoroutine = null;
    }

    public void ResetTrigger()
    {
        hasTriggered = false;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    private void SetText()
    {
        textMeshPro.SetActive(true);

        if (messageText != null)
        {
            messageText.text = message;
        }
        Debug.Log(message);
    }
}