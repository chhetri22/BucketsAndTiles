using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var tiles = GameObject.Find("t1");
        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        for (int i = 0; i < 2; i++) {
            GameObject btn;
            btn = Instantiate(tiles);
            Debug.Log(gridLayoutGroup);
            btn.transform.SetParent(gridLayoutGroup.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
