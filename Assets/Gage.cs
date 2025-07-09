using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//シーン管理

public class Gage : MonoBehaviour
{
    public Image gageImage;
    public Sprite[] gageSprites;
    // private int maxGauge = 13; // コメントアウトまたは削除検討
    private int currentGage = 0;

    public CoinManager coinManager; // インスペクタからも設定可能にしておくのは良い習慣です
    public int coinsToAwardOnClear = 10;

    void Start()
    {
        // まずCoinManagerのシングルトンインスタンスを探す
        if (CoinManager.instance != null)
        {
            coinManager = CoinManager.instance; // シングルトンインスタンスを代入
        }
        else
        {
            // シングルトンインスタンスが見つからない場合（CoinManagerがまだAwakeしていないか、シーンに存在しない可能性）
            // 念のためFindObjectOfTypeも試みる（ただし、CoinManagerが先に初期化される前提なら不要な場合も）
            Debug.LogWarning("CoinManager.instance が見つかりませんでした。FindObjectOfTypeで再検索します。");
            coinManager = FindObjectOfType<CoinManager>();
        }

        // 最終的にCoinManagerが見つかったか確認
        if (coinManager == null)
        {
            Debug.LogError("CoinManager のインスタンスが見つかりません。CoinManagerがシーンに正しく配置され、初期化されているか確認してください。");
        }

        UpdateGage(); // メソッド名を UpdateGageVisual() などに変更した場合はそちらを呼ぶ
    }

    // IncreaseGage, DecreaseGage, UpdateGage, IsGaugeFull, PerformGameClearSequence, ResetGauge メソッドは変更なし
    // (省略) //

    public void IncreaseGage()
    {
        Debug.Log("IncreaseGage() called");
        if (currentGage < gageSprites.Length - 1)
        {
            currentGage++;
            UpdateGage();
            Debug.Log("ゲージ増加" + currentGage);
        }
        //ゲージMaxならシーン移動
        if (currentGage == gageSprites.Length - 1)
        {
            // コイン獲得処理
            if (coinManager != null)
            {
                coinManager.AddCoins(coinsToAwardOnClear);
                Debug.Log(coinsToAwardOnClear + "コイン獲得！");
            }
            else
            {
                Debug.LogWarning("CoinManagerが設定されていないため、コインは獲得されませんでした。");
            }
            SceneManager.LoadScene("Gameclear");
        }
    }

    public void DecreaseGage()
    {
        if (currentGage > 0)
        {
            currentGage--;
            UpdateGage();
        }
    }

    private void UpdateGage() // メソッド名が UpdateGageVisual など別の名前に変わっていれば、そちらに合わせる
    {
        if (gageSprites != null && gageSprites.Length > 0 && currentGage >= 0 && currentGage < gageSprites.Length)
        {
            if (gageImage != null)
            {
                gageImage.sprite = gageSprites[currentGage];
            }
            else
            {
                Debug.LogError("gageImageが設定されていません。");
            }
        }
        else if (gageSprites == null || gageSprites.Length == 0)
        {
            Debug.LogError("gageSpritesが設定されていないか、空です。");
        }
    }
}
//public class Gage : MonoBehaviour
//{
//    public Image gageImage;
//    public Sprite[] gageSprites;
//    private int maxGauge = 13;
//    private int currentGage = 0;

//    public CoinManager coinManager; // CoinManagerの参照をインスペクタから設定
//    public int coinsToAwardOnClear = 10; // クリア時にもらえるコインの枚数



//    void Start()
//    {
//        if (coinManager == null)
//        {
//            // CoinManagerがインスペクタで設定されていない場合、シーン内から探す試み
//            // ただし、極力インスペクタで設定することを推奨します。
//            coinManager = FindObjectOfType<CoinManager>();
//            if (coinManager == null)
//            {
//                Debug.LogError("CoinManager が Gage スクリプトに設定されていません。インスペクタで設定するか、シーンに CoinManager が存在するか確認してください。");
//            }
//        }
//        UpdateGage();
//    }


//    public void IncreaseGage()
//    {
//        Debug.Log("Increase() called");
//        if(currentGage < gageSprites.Length - 1 )
//        {
//            currentGage++;
//            UpdateGage();
//            Debug.Log("ゲージ増加" + currentGage);
//        }
//        //ゲージMaxならシーン移動
//        if (currentGage == gageSprites.Length - 1)
//        {
//            // コイン獲得処理
//            if (coinManager != null)
//            {
//                coinManager.AddCoins(coinsToAwardOnClear);
//                Debug.Log(coinsToAwardOnClear + "コイン獲得！");
//            }
//            else
//            {
//                Debug.LogWarning("CoinManagerが設定されていないため、コインは獲得されませんでした。");
//            }
//            SceneManager.LoadScene("Gameclear");
//        }
//    }
//    public void DecreaseGage()
//    {
//        if (currentGage > 0)
//        {
//            currentGage--;
//            UpdateGage();
//        }
//    }


//    // Update is called once per frame
//    private void UpdateGage()
//    {
//        private void UpdateGage()
//        {
//            // 念のため、gageSpritesが空でないか、currentGageが範囲内かチェック
//            if (gageSprites != null && gageSprites.Length > 0 && currentGage >= 0 && currentGage < gageSprites.Length)
//            {
//                if (gageImage != null) // gageImageもnullチェック
//                {
//                    gageImage.sprite = gageSprites[currentGage];
//                }
//                else
//                {
//                    Debug.LogError("gageImageが設定されていません。");
//                }
//            }
//            else if (gageSprites == null || gageSprites.Length == 0)
//            {
//                Debug.LogError("gageSpritesが設定されていないか、空です。");
//            }
//            // else { // currentGageが範囲外の場合のログ (必要であれば)
//            //     Debug.LogWarning("currentGageの値が不正です。 currentGage: " + currentGage);
//            // }
//        }
//    }
//}