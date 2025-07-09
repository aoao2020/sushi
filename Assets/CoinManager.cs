using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理のために必要


public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private const string COIN_KEY = "PlayerTotalCoins";
    private int currentCoins = 0;

    public TextMeshProUGUI coinGetFeedbackText;
    public float feedbackDisplayDuration = 1.5f;
    public string sceneToShowFeedback = "Gameclear"; // ★ここに "GameClearScene" を設定

    // --- 追加: 直前に獲得したコイン数を一時的に保持する変数 ---
    private int lastEarnedAmount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // シーンロード時のイベント登録
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        LoadCoins();
        if (coinGetFeedbackText != null)
        {
            coinGetFeedbackText.gameObject.SetActive(false);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 現在ロードされたシーンが、フィードバックを表示したいシーンか確認
        if (scene.name == sceneToShowFeedback)
        {
            // 直前にコインを獲得した記録があればフィードバックを表示
            if (lastEarnedAmount > 0)
            {
                ShowCoinGetFeedback(lastEarnedAmount);
                lastEarnedAmount = 0; // 表示したらリセット
            }
        }
        else
        {
            // フィードバックを表示すべきシーン以外では、念のため非表示にしておく
            if (coinGetFeedbackText != null)
            {
                coinGetFeedbackText.gameObject.SetActive(false);
            }
        }
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("追加するコインの数が0以下です。Amount: " + amount);
            return;
        }
        currentCoins += amount;
        SaveCoins();
        Debug.Log(amount + "コイン獲得！ 現在の所持コイン: " + currentCoins);

        // --- 変更: すぐに表示せず、獲得量を記録するだけにする ---
        lastEarnedAmount = amount;
    }

    private void ShowCoinGetFeedback(int amount)
    {
        if (coinGetFeedbackText == null)
        {
            // もし coinGetFeedbackText が null (特にシーン遷移直後など) の場合、
            // 再度シーン内から探す試み (ただし、CoinManagerと同じシーンに永続化されているのが理想)
            GameObject feedbackTextObject = GameObject.Find("CoinGetFeedbackText"); // ★この名前はあなたのUIオブジェクト名に合わせてください
            if (feedbackTextObject != null)
            {
                coinGetFeedbackText = feedbackTextObject.GetComponent<TextMeshProUGUI>();
            }
        }

        if (coinGetFeedbackText != null)
        {
            coinGetFeedbackText.text = "+" + amount.ToString() + " Coins!";
            coinGetFeedbackText.gameObject.SetActive(true);
            StartCoroutine(HideFeedbackTextAfterDelay(feedbackDisplayDuration));
        }
        else
        {
            Debug.LogWarning("CoinGetFeedbackText の参照が見つかりません。フィードバックは表示されません。");
        }
    }

    private IEnumerator HideFeedbackTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (coinGetFeedbackText != null)
        {
            coinGetFeedbackText.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // イベント登録解除
    }

    // GetCurrentCoins, SaveCoins, LoadCoins, SpendCoins は変更なし
    public int GetCurrentCoins() { return currentCoins; }
    private void SaveCoins() { PlayerPrefs.SetInt(COIN_KEY, currentCoins); PlayerPrefs.Save(); }
    private void LoadCoins() { currentCoins = PlayerPrefs.GetInt(COIN_KEY, 0); Debug.Log("コインデータをロードしました。所持コイン: " + currentCoins); }
    public bool SpendCoins(int amountToSpend) { /* ... */ return false; }
}

