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
    public static Dictionary<string, List<string>> topicsToTilesMapping = new Dictionary<string, List<string>>();

    //adding variable score functionality
    public static Dictionary<string, string> bucketToScoreMapping = new Dictionary<string, string>();
    public static Dictionary<string, string> bucketToTopicMapping = new Dictionary<string, string>();

    public static Dictionary<string, int> bucketToOccupancyMapping = new Dictionary<string, int>();

    private Dictionary<int, string> indexToTile = new Dictionary<int, string>();
    public string fileName;

    private static string pathMeghana2 = "Assets/TilesAndBuckets/data/mappingMatrixFormat.csv";

    

    void Start()
    {
        var path1 = "Assets/TilesAndBuckets/data/"+fileName;
        Debug.Log(fileName);
        Debug.Log(path1);
        using (var reader = new StreamReader(@path1))
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
                topicsToTilesMapping[values[j][0]] = new List<string>();
                bucketToScoreMapping[values[j][0]] = values[j][1];
                bucketToOccupancyMapping[values[j][0]] = 0;

                for (int k = 2; k<values[0].Length;k++)
                {
                    if(values[j][k].Equals("1"))
                    {
                        topicsToTilesMapping[values[j][0]].Add(indexToTile[k]);
                    }
                }
            }
        }
    }

    // void Start() {
    // }


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

            if (topicsToTilesMapping.ContainsKey(destinationCellName))
            {

                if (topicsToTilesMapping[destinationCellName].Contains(sourceCellName))
                {
                    Debug.Log("Correct match");
                    ScoreScript.scoreValue += int.Parse(bucketToScoreMapping[destinationCellName]);
                } else
                {
                    Debug.Log("Incorrect match");
                    ScoreScript.scoreValue -= 1;
                }
            }

            var destBucket = desc.destinationCell.gameObject;
            var gameObjectToDestroy = destBucket.transform.Find("img_"+sourceCellName);
            Destroy(gameObjectToDestroy.gameObject);

            if (bucketToOccupancyMapping[destinationCellName] < 5) {
                bucketToOccupancyMapping[destinationCellName] += 1;
                //increase the level fill of the bucket
                object[] sprites = Resources.LoadAll("bucket/incremental", typeof(Sprite));
                Image imageComponent = destBucket.transform.GetChild(0).GetComponent<Image>();
                imageComponent.sprite = (Sprite)sprites[bucketToOccupancyMapping[destinationCellName]];
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
