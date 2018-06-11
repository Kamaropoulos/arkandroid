using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockInfo {
    public int lives;
    public string color;
}

public class GridLineInfo {
    public BlockInfo[] blocks;

    public GridLineInfo(int blocksCount) {
        blocks = new BlockInfo[blocksCount];
        for (int i = 0; i < blocksCount; i++) {
            blocks[i] = new BlockInfo();
        }
    }
}

[Serializable]
public class GridData {
    public int linesCount;
    public int blocksCount;
    public GridLineInfo[] lines;

    public GridData(string filename) {
        // Load level file
        TextAsset levelRaw = Resources.Load(filename) as TextAsset;

        // Split into lines
        string[] linesRaw = levelRaw.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // Get number of grid lines and blocks per line
        int[] counts = linesRaw[0].Split(' ').Select(int.Parse).ToArray();
        linesCount = counts[0];
        blocksCount = counts[1];

        lines = new GridLineInfo[linesCount];

        for (int i = 1; i <= linesCount; i++) {
            lines[i - 1] = new GridLineInfo(blocksCount);

            // Split line into array of integers
            string[] array = linesRaw[i].Split(' ').ToArray();
            
            for (int j = 0; j < blocksCount; j++) {
                int currentLives = Int32.Parse(array[j][0].ToString());

                if (currentLives > 0) {
                    string[] data = array[j].Split(',').ToArray();
                    string blockColor = data[1];
                    lines[i - 1].blocks[j].color = blockColor;
                    lines[i - 1].blocks[j].lives = currentLives;
                } else {
                    lines[i - 1].blocks[j].lives = currentLives;
                }
            }
        }
    }
}

public class GridController : MonoBehaviour {
    public GameObject BlockPrefab;

    public string level;

    private Vector3 GridStartPosition;

    float spriteWidth;
    float spriteHeight;

    float viewportWidth;

    private void Awake() {
        viewportWidth = Math.Abs(Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x - Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).x);

        // Instantiate object off to the distance
        GameObject newBlock = Instantiate(BlockPrefab, new Vector3(-1000, -1000, 0), new Quaternion(0, 0, 0, 0));

        // Get sprite width and height
        spriteWidth = (float)newBlock.GetComponent<Renderer>().bounds.size.x;
        spriteHeight = (float)newBlock.GetComponent<Renderer>().bounds.size.y;

        // We got all the info we need, now destory it
        Destroy(newBlock);

        GridStartPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        GridStartPosition.y -= spriteHeight;
        GridStartPosition.z = 0;
    }

    // Use this for initialization
    void Start () {
        GridData grid = new GridData(level);

        // Calculate margin
        float margin = (viewportWidth - (grid.blocksCount * spriteWidth)) / 2;

        GridStartPosition.x += margin;
        Vector3 position = GridStartPosition;
        
        for (int i = 0; i < grid.linesCount; i++) {
            for (int j = 0; j < grid.blocksCount; j++) {
                if (grid.lines[i].blocks[j].lives > 0) {
                    SpawnNewBlock(grid.lines[i].blocks[j].color, grid.lines[i].blocks[j].lives, position, new Quaternion(0, 0, 0, 0));
                }
                position.x += spriteWidth;
            }
            position.x = GridStartPosition.x;
            position.y -= spriteHeight;
        }
    }

    GameObject SpawnNewBlock(string color, int lives, Vector3 position, Quaternion rotation) {
        // Instantiate object off to the distance
        GameObject newBlock = Instantiate(BlockPrefab, new Vector3(-1000, -1000, 0), rotation);

        // Move to the new location based on the objects width
        newBlock.GetComponent<BlockController>().Initialize(color, lives);
        newBlock.transform.position = new Vector3(position.x + (spriteWidth / 2), position.y + (spriteHeight / 2), position.z);
        return newBlock;
    }
 }
