using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 


public class CoinDisplay : MonoBehaviour
{ 
public TextMeshProUGUI coinTextMeshPro; // TextMeshProを使用する場合、インスペクタで設定

void Start()
{
    // CoinManagerのインスタンスが見つからない場合のエラー処理
    if (CoinManager.instance == null)
    {
        Debug.LogError("CoinManagerのインスタンスが見つかりません。");
        // coinTextMeshPro.text = "CoinManager Error"; // エラー表示など
        if (coinTextMeshPro != null) coinTextMeshPro.text = "Error";
        // if (coinText != null) coinText.text = "Error";
        return;
    }
    UpdateCoinText(); // 初期表示
}

void Update()
{
    // 毎フレーム更新するのは効率が悪い場合があるので、
    // コイン数が変更された時だけ更新する仕組み（イベントなど）が理想ですが、
    // まずは簡単なUpdateでの更新で実装してみましょう。
    if (CoinManager.instance != null)
    {
        UpdateCoinText();
    }
}

// コイン数をUIに表示するメソッド
public void UpdateCoinText()
{
    if (CoinManager.instance != null)
    {
        int currentCoins = CoinManager.instance.GetCurrentCoins();
        if (coinTextMeshPro != null)
        {
            coinTextMeshPro.text = "Coins: " + currentCoins.ToString();
        }
        // else if (coinText != null) // 標準UI Textの場合
        // {
        //     coinText.text = "Coins: " + currentCoins.ToString();
        // }
    }
}
}
