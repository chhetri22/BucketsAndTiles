using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public static int maxScore = 0;
    Text finalMessage;

    // Start is called before the first frame update
    void Start()
    {
        finalMessage = GetComponent<Text>();

        finalMessage.text = "Your score is: " + ScoreScript.scoreValue;

        if (maxScore < ScoreScript.scoreValue)
        {
            finalMessage.text += "\n" + "You beat your old high score of " + maxScore + "!";


            maxScore = ScoreScript.scoreValue;
        }

        finalMessage.text += "\n" + "Keep working hard!";

        ScoreScript.scoreValue = 0;

    }

    // Update is called once per frame
    void Update()
    {


        


    }
}
