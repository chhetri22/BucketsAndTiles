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

    GameObject findGameObject(string name) {
        return GameObject.Find(name);
    }

    void Start()
    {
        var bucketTemplate = findGameObject("bucketTemplate");
        var buttonTemplate = findGameObject("TopicChoiceButton");
        var panel = findGameObject("TopicPanel");
        var bucketGrid = findGameObject("BucketGrid");
        Dictionary<string, List<string>>.KeyCollection allTopics = DummyControlUnit.topicsToTilesMapping.Keys;

        for (int i = 0; i<4; i++) {
            //create a new bucket
            GameObject newBucket = createObject(bucketTemplate, bucketGrid.transform, "bucket"+i);
            //change the label
            Text someText = newBucket.transform.Find("labelTemplate").GetComponent<Text>();
            someText.text = "Choose One";

            //turn off the template button
            var newPanel = newBucket.transform.Find("SelectButton").transform.Find("TopicPanel");
            newPanel.transform.Find("TopicChoiceButton").gameObject.SetActive(false);

            foreach (string topic in allTopics) {
                GameObject newButton = createObject(buttonTemplate, newPanel.transform, topic+"Button");
                Text choiceText = newButton.transform.Find("TopicText").GetComponent<Text>();
                choiceText.text = topic;
            }

            //disable the initial choice buttons
            GameObject choice = newBucket.transform.Find("SelectButton").transform.Find("TopicPanel").gameObject;
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
