using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneration : MonoBehaviour {
    public DummyControlUnit dummyControlUnit;
    // Start is called before the first frame update
    void Start () {
        var tileTemplate = GameObject.Find ("tileTemplate");
        var gridLayoutGroup = GetComponent<GridLayoutGroup> ();

        object[] sprites = Resources.LoadAll (dummyControlUnit.graphicsFolderName, typeof (Sprite));

        //random ordering of tiles
        List<int> randomList = new List<int> ();

        for (int i = 0; i < sprites.Length; i++) {
            randomList.Add (i);
        }

        randomList = Shuffle (randomList);

        foreach (int i in randomList) {
            GameObject newTile;
            newTile = Instantiate (tileTemplate);
            newTile.transform.SetParent (gridLayoutGroup.transform);

            Image imageComponent = newTile.transform.GetChild (0).GetComponent<Image> ();
            imageComponent.sprite = (Sprite) sprites[i];
            var name = sprites[i].ToString ().Split (' ') [0];
            newTile.transform.GetChild (0).name = "img_" + name;
            newTile.name = name;
        }
        tileTemplate.SetActive (false);
    }

    // Update is called once per frame
    void Update () {

    }

    private List<int> Shuffle (List<int> list) {
        var rng = new System.Random ();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next (n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

}