using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiController : MonoBehaviour
{
    GameObject player;
    //float fallSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        //当たり判定
        Vector2 p1 = transform.position;//寿司の中心座標
        Vector2 p2 = this.player.transform.position; //プレイヤーの中心座標
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f; //寿司の半径
        float r2 = 1.0f; //プレイヤの半径

        if (d < r1 + r2)
        {
            //衝突した場合は寿司を消す
            Destroy(gameObject);
        }
    }
}
