using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {
    public static int highScore = 0;
    public static int percentage = 0;

    Text finalMessage;

    // Start is called before the first frame update
    void Start () {

        finalMessage = GetComponent<Text> ();

        finalMessage.text = "Your score is: " + ScoreScript.scoreValue;

        if (highScore < ScoreScript.scoreValue) {
            finalMessage.text += "\n" + "You beat your old high score of " + highScore + "!";

            highScore = ScoreScript.scoreValue;
        }

        int foreverMax = FinalScore.calculateMaxScore ();

        percentage = (ScoreScript.scoreValue * 100) / foreverMax;

        finalMessage.text += "\n" + "You earned " + percentage + "% of the maximum possible score: " + foreverMax + "!";

        finalMessage.text += "\n" + "Keep working hard!";

        ScoreScript.scoreValue = 0;

    }

    // Update is called once per frame
    void Update () {

    }
    public static int calculateMaxScore () {
        //string[][] allValues = DummyControlUnit.values;

        int maxPossScore = 0;

        int numRow = DummyControlUnit.values.Length;
        int numCol = DummyControlUnit.values[0].Length;

        //highest possible score per tile
        int columnMax = 0;

        for (int i = 2; i < numCol; i++) {

            for (int j = 1; j < numRow; j++) {
                //score earned if tile matches with bucket
                int matchScore = (int.Parse (DummyControlUnit.values[j][1]) * int.Parse (DummyControlUnit.values[j][i]));

                if (columnMax < matchScore) {
                    columnMax = matchScore;
                }
            }

            maxPossScore += columnMax;

            columnMax = 0;
        }

        return maxPossScore;
    }
}