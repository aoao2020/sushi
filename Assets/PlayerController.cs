using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Gage sushiGauge; //Gageのスクリプトを参照

    // Start is called before the first frame update
    void Start()

    {
        
        Application.targetFrameRate = 60;   
    }



    // Update is called once per frame
    void Update()
    {
     //左矢印が押された時
     if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-3, 0, 0);//左に３動かす
        }
        //右矢印が押された時
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0); //右に３動かす
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.CompareTag("GoodSushi"))
        {
            FindObjectOfType<Gage>().IncreaseGage();
            Destroy(other.gameObject);
        }
      else if (other.CompareTag("BadSushi"))
        {
            FindObjectOfType<Gage>().DecreaseGage();
            Destroy(other.gameObject);
        }
    }







}
