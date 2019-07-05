using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TopicChoiceManager : MonoBehaviour
{
    public void PickTopic() {
        string chosenText = this.gameObject.transform.Find("TopicText").GetComponent<Text>().text;
        string chosenTopic = chosenText.Split(' ')[0];
        
        // var panel = this.gameObject.transform.parent;
        // var SelectButton = panel.gameObject.transform.parent;
        //Text labelText = SelectButton.gameObject.transform.parent.gameObject.GetComponent<Text>();
        var bucket = this.gameObject.transform.parent.parent.parent;
        // var something = bucket.GetChild(1);
        var gameObject = bucket.gameObject;

        Text labelText = gameObject.transform.Find("labelTemplate").GetComponent<Text>();
        labelText.text = chosenText;
        // gameObject.transform.Find("labelTemplate").GetComponent<Text>().text = chosenText.text;

        //change the name of the bucket to the chosen name
        gameObject.name = chosenTopic;

        //remove this option from other buckets
        foreach(Transform child in bucket.transform.parent)
        {
            Debug.Log(child.name);
            if (child.name != chosenTopic && child.name != "bucketTemplate"){
                var newPanel = child.transform.Find("SelectButton").transform.Find("TopicPanel");
                if (newPanel) {
                    var buttonToDisable = newPanel.transform.Find(chosenTopic+"Button");
                    buttonToDisable.gameObject.SetActive(false);
                }

            }
        }

        Destroy(this.gameObject.transform.parent.gameObject);

    }
}