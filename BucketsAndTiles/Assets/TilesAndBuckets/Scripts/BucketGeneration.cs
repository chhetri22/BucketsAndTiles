using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketGeneration : MonoBehaviour
{
    //public string folderName;
    // Start is called before the first frame update

    GameObject createObject(GameObject template, Transform parent, string name) {
        GameObject newObject;
        newObject = Instantiate(template);            
        newObject.transform.SetParent(parent);
        newObject.name = name;
        return newObject;
    }

    void Start()
    {
        var bucketTemplate = GameObject.Find("bucketTemplate");
        var buttonTemplate = GameObject.Find("TopicChoiceButton");
        var panel = GameObject.Find("Panel");
        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        Dictionary<string, List<string>>.KeyCollection allTopics = DummyControlUnit.topicsToTilesMapping.Keys;
        for (int i = 0; i<4; i++) {
            //create a new bucket
            GameObject newBucket = createObject(bucketTemplate, gridLayoutGroup.transform, "bucket"+i);
            //change the label
            Text someText = newBucket.transform.Find("labelTemplate").GetComponent<Text>();
            someText.text = "Choose One";

            foreach (string topic in allTopics) {
                var newPanel = newBucket.transform.Find("SelectButton").transform.Find("Panel");
                GameObject newButton = createObject(buttonTemplate, newPanel.transform, topic+"Button");

                Text choiceText = newButton.transform.Find("Topic").GetComponent<Text>();
                choiceText.text = topic;
            }


            //disable the choice buttons initially
            GameObject choice = newBucket.transform.Find("SelectButton").transform.Find("Panel").gameObject;
            choice.SetActive(false);
        }
        bucketTemplate.SetActive(false);
        buttonTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
