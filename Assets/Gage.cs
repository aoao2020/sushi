using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//シーン管理

public class Gage : MonoBehaviour
{
    public Image gageImage;
    public Sprite[] gageSprites;
    private int maxGauge = 13;
    private int currentGage = 0;



    void Start()
    {
        UpdateGage();
    }


    public void IncreaseGage()
    {
        Debug.Log("Increase() called");
        if(currentGage < gageSprites.Length - 1 )
        {
            currentGage++;
            UpdateGage();
            Debug.Log("ゲージ増加" + currentGage);
        }
        //ゲージMaxならシーン移動
        if (currentGage == gageSprites.Length - 1)
        {
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


    // Update is called once per frame
    private void UpdateGage()
    {
        gageImage.sprite = gageSprites[currentGage];
    }
}
