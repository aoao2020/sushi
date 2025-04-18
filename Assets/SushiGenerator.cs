using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiGenerator : MonoBehaviour
{
    public GameObject sushi1Prefab;
    public GameObject sushi2Prefab;
    float span = 1.0f;
    float delta = 0;

    GameObject selectPrefab(int n)
    // nに応じたsushiPrefabを返す
    // 要改善：配列を使用など
    {
        if (n == 0)
        {
            return sushi1Prefab;
        }
        else
        {
            return sushi2Prefab;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            // n は 0と1からランダムに選択
            int n = Random.Range(0, 2);

            // nに応じてprefabを選択
            GameObject sushiPrefab = selectPrefab(n);

            // 本通りに配置
            GameObject go = Instantiate(sushiPrefab);
            int px = Random.Range(-6, 7);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
