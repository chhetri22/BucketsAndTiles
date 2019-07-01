using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneration : MonoBehaviour
{
    public string folderName;
    // Start is called before the first frame update
    void Start()
    {
        var tileTemplate = GameObject.Find("tileTemplate");
        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        object[] sprites = Resources.LoadAll(folderName, typeof(Sprite));
        Debug.Log(sprites.Length);
        Debug.Log(sprites);
        for (int i = 0; i < sprites.Length; i++) {
            GameObject newTile;
            newTile = Instantiate(tileTemplate);            
            newTile.transform.SetParent(gridLayoutGroup.transform);

            Image imageComponent = newTile.transform.GetChild(0).GetComponent<Image>();
            imageComponent.sprite = (Sprite)sprites[i];
            var name = sprites[i].ToString().Substring(0,2);
            newTile.transform.GetChild(0).name = "img_"+name;
            newTile.name = name;
        }
        tileTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
