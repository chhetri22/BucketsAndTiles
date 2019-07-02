using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Handles the scoring based on drag and drop events;
/// Generates array of correct answers and scoresbased on CSV
/// </summary>
public class DummyControlUnit : MonoBehaviour
{
    //this dict stores correct answers for each bucket
    public static Dictionary<string, List<string>> bucketToTilesMapping = new Dictionary<string, List<string>>();

    //adding variable score functionality
    public static Dictionary<string, string> bucketToScoreMapping = new Dictionary<string, string>();


    private Dictionary<int, string> indexToTile = new Dictionary<int, string>();
    public GameObject cell;

    private static string pathMeghana2 = "Assets/TilesAndBuckets/data/mappingMatrixFormat.csv";

    

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
                    }
                }
            }
        }
    }

    void Start() {
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


        if (desc.triggerType == DragAndDropCell.TriggerType.DropEventEnd)
        {

            string destinationCellName = desc.destinationCell.ToString().Split(' ')[0];
            string sourceCellName = desc.sourceCell.ToString().Split(' ')[0];


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
