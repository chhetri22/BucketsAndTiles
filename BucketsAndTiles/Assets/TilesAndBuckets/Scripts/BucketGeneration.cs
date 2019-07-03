using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketGeneration : MonoBehaviour
{
    //public string folderName;
    // Start is called before the first frame update
    void Start()
    {
        var bucketTemplate = GameObject.Find("bucketTemplate");
        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        Dictionary<string, List<string>> tilesToBucketMapping = DummyControlUnit.bucketToTilesMapping;
        Dictionary<string, List<string>>.KeyCollection keys = tilesToBucketMapping.Keys;
        foreach (string key in keys) {
            GameObject newBucket;
            newBucket = Instantiate(bucketTemplate);            
            newBucket.transform.SetParent(gridLayoutGroup.transform);
            newBucket.name = key;

            Text someText = newBucket.transform.GetChild(1).GetComponent<Text>();
            someText.text = key + " ("+DummyControlUnit.bucketToScoreMapping[key]+")";
        }
        bucketTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
