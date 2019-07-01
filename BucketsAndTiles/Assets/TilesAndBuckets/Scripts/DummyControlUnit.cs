using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Example of control application for drag and drop events handle
/// </summary>
public class DummyControlUnit : MonoBehaviour
{
    public static Dictionary<string, List<string>> bucketToTilesMapping = new Dictionary<string, List<string>>();

    //adding variable score Functionality
    private Dictionary<string, string> bucketToScoreMapping = new Dictionary<string, string>();

    private Dictionary<int, string> indexToTile = new Dictionary<int, string>();
    public GameObject cell;
    private static string path = "Assets/TilesAndBuckets/data/mapping.csv";
    private static string pathMeghana2 = "Assets/TilesAndBuckets/data/mappingMatrixFormat.csv";
    //private static int score = 0;

    

    public DummyControlUnit()
    {
        using (var reader = new StreamReader(@pathMeghana2))
        {
            List<string[]> lines = new List<string[]>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                lines.Add(line.Split(','));

            }

            string[][] values = lines.ToArray();
  
            for(int i = 2; i<values[0].Length; i++)
            {
                indexToTile[i] = values[0][i];
            }

            for(int j = 1;j<values.Length; j++)
            {
                bucketToTilesMapping[values[j][0]] = new List<string>();
                bucketToScoreMapping[values[j][0]] = values[j][1];

                for (int k = 2; k<values[0].Length;k++)
                {
                    if(values[j][k].Equals("1"))
                    {
                        bucketToTilesMapping[values[j][0]].Add(indexToTile[k]);
                        Debug.Log("yeehaw");
                    }
                }
            }
        }
    }

    void Start() {
        // GameObject g3 = (GameObject)(Instantiate (cell, transform.position, Quaternion.identity));
        // var foundObjects = FindObjectsOfType<Cell>();
        // var tiles = GameObject.Find("t1");
        // var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        // for (int i = 0; i < 2; i++) {
        //     GameObject btn;
        //     btn = Instantiate(tiles);
        //     Debug.Log(gridLayoutGroup);
        //     // btn.transform.SetParent(gridLayoutGroup.transform);
        // }
    }


    /// <summary>
    /// Operate all drag and drop requests and events from children cells
    /// </summary>
    /// <param name="desc"> request or event descriptor </param>
    void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        // Get control unit of source cell
        DummyControlUnit sourceSheet = desc.sourceCell.GetComponentInParent<DummyControlUnit>();
        // Get control unit of destination cell
        DummyControlUnit destinationSheet = desc.destinationCell.GetComponentInParent<DummyControlUnit>();
        //switch (desc.triggerType)                                               // What type event is?
        //{
        //    case DragAndDropCell.TriggerType.DropRequest:                       // Request for item drag (note: do not destroy item on request)
        //        Debug.Log("Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
        //        break;
        //    case DragAndDropCell.TriggerType.DropEventEnd:                      // Drop event completed (successful or not)
        //        if (desc.permission == true)                                    // If drop successful (was permitted before)
        //        {
        //            Debug.Log("Successful drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
        //        }
        //        else                                                            // If drop unsuccessful (was denied before)
        //        {
        //            Debug.Log("Denied drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
        //        }
        //        break;
        //    case DragAndDropCell.TriggerType.ItemAdded:                         // New item is added from application
        //        Debug.Log("Item " + desc.item.name + " added into " + destinationSheet.name);
        //        break;
        //    case DragAndDropCell.TriggerType.ItemWillBeDestroyed:               // Called before item be destructed (can not be canceled)
        //        Debug.Log("Item " + desc.item.name + " will be destroyed from " + sourceSheet.name);
        //        break;
        //    default:
        //        Debug.Log("Unknown drag and drop event");
        //        break;
        //}

        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd)
        {
            //Debug.Log("Lemme just destroy things");
            //desc.destinationCell.RemoveItem();
            Debug.Log("Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
            Debug.Log("HEYY Package from: " + desc.sourceCell + "to " + desc.destinationCell);

            //foreach (string key in tilesToBucketMapping.Keys)
            //{
            //    Debug.Log("Key: " + key.ToCharArray);
            //}
            string destinationCellName = desc.destinationCell.ToString().Split(' ')[0];
            string sourceCellName = desc.sourceCell.ToString().Split(' ')[0];

            Debug.Log(destinationCellName);

            if (bucketToTilesMapping.ContainsKey(destinationCellName))
            {

                if (bucketToTilesMapping[destinationCellName].Contains(sourceCellName))
                {
                    Debug.Log("Correct match");
                    ScoreScript.scoreValue += int.Parse(bucketToScoreMapping[destinationCellName]);
                } else
                {
                    Debug.Log("Incorrect match");
                    ScoreScript.scoreValue -= 1;
                }
            }
            Debug.Log("Your score is: " + ScoreScript.scoreValue);
            Debug.Log("Your item is: " + desc.item.name);
        }

    }

    /// <summary>
    /// Add item in first free cell
    /// </summary>
    /// <param name="item"> new item </param>
    public void AddItemInFreeCell(DragAndDropItem item)
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() == null)
                {
                    cell.AddItem(Instantiate(item.gameObject).GetComponent<DragAndDropItem>());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Remove item from first not empty cell
    /// </summary>
    public void RemoveFirstItem()
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() != null)
                {
                    cell.RemoveItem();
                    break;
                }
            }
        }
    }
}
