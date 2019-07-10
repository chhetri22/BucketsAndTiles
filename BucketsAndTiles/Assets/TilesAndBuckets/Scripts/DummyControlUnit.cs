using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the scoring based on drag and drop events;
/// Generates array of correct answers and scoresbased on CSV
/// </summary>
public class DummyControlUnit : MonoBehaviour {
    
    //this dict stores correct answers for each bucket
    public static Dictionary<string, List<string>> topicsToTilesMapping = new Dictionary<string, List<string>> ();

    //adding variable score functionality
    public static Dictionary<string, string> bucketToScoreMapping = new Dictionary<string, string> ();
    public static Dictionary<string, int> bucketToOccupancyMapping = new Dictionary<string, int> ();

    private Dictionary<int, string> indexToTile = new Dictionary<int, string> ();

    //values holds the mapping array; defined outside and public bc FinalScore uses it
    public static string[][] values = null;

    public string fileName;

    public string graphicsFolderName;

    void Start () {
        var path1 = "Assets/TilesAndBuckets/data/" + fileName;

        Object[] x1 = Resources.LoadAll ("another", typeof (TextAsset));

        TextAsset SourceFile = (TextAsset) Resources.Load ("CSVFiles/" + fileName, typeof (TextAsset));
        string textContent = SourceFile.text;
        List<string[]> listLines = new List<string[]> ();

        string[] lines = textContent.Split ('\n');

        foreach (string line in lines) {
            if (line != "") {
                listLines.Add (line.Split (','));
            }
        }

        values = listLines.ToArray ();

        for (int i = 2; i < values[0].Length; i++) {
            indexToTile[i] = values[0][i];
        }

        for (int j = 1; j < values.Length; j++) {
            topicsToTilesMapping[values[j][0]] = new List<string> ();
            bucketToScoreMapping[values[j][0]] = values[j][1];
            bucketToOccupancyMapping[values[j][0]] = 0;

            for (int k = 2; k < values[0].Length; k++) {
                if (values[j][k].Equals ("1")) {
                    topicsToTilesMapping[values[j][0]].Add (indexToTile[k]);
                }
            }
        }
    }

    /// <summary>
    /// Operate all drag and drop requests and events from children cells
    /// </summary>
    /// <param name="desc"> request or event descriptor </param>
    void OnSimpleDragAndDropEvent (DragAndDropCell.DropEventDescriptor desc) {
        // Get control unit of source cell
        DummyControlUnit sourceSheet = desc.sourceCell.GetComponentInParent<DummyControlUnit> ();
        // Get control unit of destination cell
        DummyControlUnit destinationSheet = desc.destinationCell.GetComponentInParent<DummyControlUnit> ();

        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd) {

            string destinationCellName = desc.destinationCell.ToString ().Split (' ') [0];
            string sourceCellName = desc.sourceCell.ToString ().Split (' ') [0];

            if (topicsToTilesMapping.ContainsKey (destinationCellName)) {

                if (topicsToTilesMapping[destinationCellName].Contains (sourceCellName)) {
                    ScoreScript.scoreValue += int.Parse (bucketToScoreMapping[destinationCellName]);
                } else {
                    ScoreScript.scoreValue -= 1;
                }
            }

            var destBucket = desc.destinationCell.gameObject;
            var gameObjectToDestroy = destBucket.transform.Find ("img_" + sourceCellName);
            Destroy (gameObjectToDestroy.gameObject);

            if (bucketToOccupancyMapping[destinationCellName] < 5) {
                bucketToOccupancyMapping[destinationCellName] += 1;
                //increase the level fill of the bucket
                object[] sprites = Resources.LoadAll ("bucket/incremental", typeof (Sprite));
                Image imageComponent = destBucket.transform.GetChild (0).GetComponent<Image> ();
                imageComponent.sprite = (Sprite) sprites[bucketToOccupancyMapping[destinationCellName]];
            }
        }

    }

    /// <summary>
    /// Add item in first free cell
    /// </summary>
    /// <param name="item"> new item </param>
    public void AddItemInFreeCell (DragAndDropItem item) {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell> ()) {
            if (cell != null) {
                if (cell.GetItem () == null) {
                    cell.AddItem (Instantiate (item.gameObject).GetComponent<DragAndDropItem> ());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Remove item from first not empty cell
    /// </summary>
    public void RemoveFirstItem () {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell> ()) {
            if (cell != null) {
                if (cell.GetItem () != null) {
                    cell.RemoveItem ();
                    break;
                }
            }
        }
    }
}