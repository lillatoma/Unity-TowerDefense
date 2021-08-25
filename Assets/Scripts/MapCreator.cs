using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreator : MonoBehaviour
{
    public int bgIndex = 0;

    public GameObject[] pathBlocks;
    public GameObject[] turretBlocks;

    private GameObject[,] Blocks;


    int GetTextureIndexOfBlock(int x, int y)
    {
        int count = 0;
        for (int i = 0; i < pathBlocks.Length; i++)
        {
            if (Blocks[x, y].GetComponent<SpriteRenderer>().sprite == pathBlocks[i].GetComponent<SpriteRenderer>().sprite)
                return count;
            count++;
        }
        for (int i = 0; i < turretBlocks.Length; i++)
        {
            if (Blocks[x, y].GetComponent<SpriteRenderer>().sprite == turretBlocks[i].GetComponent<SpriteRenderer>().sprite)
                return count;
            count++;
        }
        return -1;
    }
    GameObject GetBlockByIndex(int index)
    {
        GameObject placeable;

        if (index < 0)
            index = 0;

        if (index < pathBlocks.Length)
            placeable = pathBlocks[index];
        else
        {
            index -= pathBlocks.Length;
            if (index > turretBlocks.Length - 1)
                index = turretBlocks.Length - 1;

            placeable = turretBlocks[index];
        }

        return placeable;
    }

    GameObject GetTypedBlockByIndex(int index, int type)
    {
        GameObject placeable;

        if (index < 0)
            index = 0;

        if (type < pathBlocks.Length)
        {
            if (index < pathBlocks.Length)
                placeable = pathBlocks[index];
            else
            {
                index -= pathBlocks.Length;
                if (index > pathBlocks.Length - 1)
                    index = pathBlocks.Length - 1;
                placeable = pathBlocks[index];
            }
        }
        else
        {
            if (index < pathBlocks.Length)
                placeable = turretBlocks[index];
            else
            {
                index -= pathBlocks.Length;
                if (index > turretBlocks.Length - 1)
                    index = turretBlocks.Length - 1;
                placeable = turretBlocks[index];
            }
        }
        return placeable;
    }
    void SetupMap()
    {
        if (Blocks != null)
            Array.Clear(Blocks, 0, Blocks.Length);
        Blocks = new GameObject[32, 32];
    }
    void CreateBlock(GameObject placeable, int x, int y)
    {
        if (Blocks[x, y] != null)
            Destroy(Blocks[x, y]);
        GameObject gO = GameObject.Instantiate(placeable);
        gO.transform.parent = transform;
        gO.transform.localPosition = new Vector2(x - 15.5f, y - 15.5f);
        gO.GetComponent<WorldObjectMid>().SetPosToIntMiddle();
        gO.GetComponent<WorldObjectMid>().enabled = false;
        gO.GetComponent<Tile>().indexCoordinates = new Vector2Int(x, y);
        Blocks[x, y] = gO;
    }
    void GenerateBackground()
    {
        GameObject placeable = GetBlockByIndex(bgIndex);

        for (int x = 0; x < 32; x++)
            for (int y = 0; y < 32; y++)
            {
                CreateBlock(placeable, x, y);
            }
    }
    /// <summary>
    /// Changes the block at (x,y) from path to turret and vice versa
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void ChangeTileType(int x, int y)
    {
        for(int i = 0; i < pathBlocks.Length; i++)
            if(Blocks[x,y].GetComponent<SpriteRenderer>().sprite == pathBlocks[i].GetComponent<SpriteRenderer>().sprite)
            {
                Destroy(Blocks[x, y]);
                CreateBlock(turretBlocks[i], x, y);
                return;
            }
        for (int i = 0; i < turretBlocks.Length; i++)
            if (Blocks[x, y].GetComponent<SpriteRenderer>().sprite == turretBlocks[i].GetComponent<SpriteRenderer>().sprite)
            {
                Destroy(Blocks[x, y]);
                CreateBlock(pathBlocks[i], x, y);
                return;
            }

    }


    void AddCircle(GameObject placeable, float x, float y, float rad)
    {
        for(int _x = 0; _x < 32; _x++)
            for(int _y = 0; _y < 32; _y++)
            {
                if(new Vector2(x-_x,y-_y).magnitude < rad)
                {
                    Destroy(Blocks[_x, _y]);
                    CreateBlock(placeable, _x, _y);
                }
            }
    }

    void AddCircle(int index, float x, float y, float rad)
    {
        AddCircle(GetBlockByIndex(index), x, y, rad);
    }

    void AddBiomeCircle(int index, float x, float y, float rad)
    {
        for (int _x = 0; _x < 32; _x++)
            for (int _y = 0; _y < 32; _y++)
            {
                if (new Vector2(x - _x, y - _y).magnitude < rad)
                {
                    int texIndex = GetTextureIndexOfBlock(_x, _y);
                    CreateBlock(GetTypedBlockByIndex(index, texIndex), _x, _y);
                }
            }
    }

    void AddHorizontalLine(int index, int x, int y, int blockLen)
    {
        for(int i = 0; i < blockLen; i++)
        {
            if (x + i < 32)
                CreateBlock(GetBlockByIndex(index), x + i, y);

        }
    }

    void AddVerticalLine(int index, int x, int y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (y + i < 32)
                CreateBlock(GetBlockByIndex(index), x, y + i);

        }
    }

    void AddHorizontalDoubleLine(int index, int x, float y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (y + i < 32)
            {
                CreateBlock(GetBlockByIndex(index), x + i, (int)(y - 0.5f));
                CreateBlock(GetBlockByIndex(index), x + i, (int)(y + 0.5f));
            }
        }
    }

    void AddVerticalDoubleLine(int index, float x, int y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (y + i < 32)
            {
                CreateBlock(GetBlockByIndex(index), (int)(x - 0.5f), y+i);
                CreateBlock(GetBlockByIndex(index), (int)(x + 0.5f), y+i);
            }
        }
    }

    void AddHorizontalPath(int x, int y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (x + i < 32)
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock(x+i,y), 0), x + i, y);

        }
    }

    void AddVerticalPath(int x, int y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (y + i < 32)
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock(x, y+i), 0), x, y + i);

        }
    }

    void AddHorizontalDoublePath(int x, float y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (x + i < 32)
            {
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock(x+i, (int)(y-0.5f)), 0), x + i, (int)(y - 0.5f));
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock(x+i, (int)(y+0.5f)), 0), x + i, (int)(y + 0.5f));
            }
        }
    }

    void AddVerticalDoublePath(float x, int y, int blockLen)
    {
        for (int i = 0; i < blockLen; i++)
        {
            if (y + i < 32)
            {
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock((int)(x-0.5f), y+i), 1), (int)(x - 0.5f), y + i);
                CreateBlock(GetTypedBlockByIndex(GetTextureIndexOfBlock((int)(x+0.5f), y+i), 1), (int)(x + 0.5f), y + i);
            }
        }
    }

    IEnumerator MapCreation()
    {
        AddBiomeCircle(2, 0, 0, 11);
        AddBiomeCircle(2, 5, 1, 7.5f);
        AddBiomeCircle(2, 7, 4f, 4f);
        AddBiomeCircle(2, 10, 3, 4.7f);
        AddBiomeCircle(2, 5.36f, 7.11f, 4.17f);
        
        AddBiomeCircle(1, 27, 31, 9f);
        AddBiomeCircle(1, 23, 29, 6f);
        AddBiomeCircle(1, 32, 26, 8f);

        AddVerticalDoublePath(3.5f, 26, 6);
        AddHorizontalDoublePath(3, 26.5f, 16);
        AddVerticalDoublePath(18.5f, 17, 11);
        AddHorizontalDoublePath(18, 17.5f, 4);
        AddVerticalDoublePath(22.5f, 17, 6);
        AddHorizontalDoublePath(22, 23.5f, 7);
        AddVerticalDoublePath(27.5f, 11, 12);
        AddHorizontalDoublePath(10, 11.5f, 18);
        AddVerticalDoublePath(10.5f, 11, 9);
        AddHorizontalDoublePath(4, 18.5f, 6);
        AddVerticalDoublePath(4.5f, 7, 13);
        AddHorizontalDoublePath(4, 5.5f, 20);
        AddVerticalDoublePath(22.5f,0, 5);
        yield return null;
        //AddVerticalDoublePath()
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupMap();
        GenerateBackground();
        StartCoroutine(MapCreation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
