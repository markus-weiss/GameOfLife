using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GOLManager : MonoBehaviour
{


    public int rows = 100;
    public int columns = 100;

    public Tilemap map;

    public TileBase black;
    public TileBase white;

    public bool[,] cellArray;

    public float generationTime = 0.1f;

    IEnumerator GOLUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationTime);
            Generation();
        }
    }

    private void Start()
    {
        Create();
        StartCoroutine(GOLUpdate());
    }

    public void Create()
    {

        //less = 2;
        //more = 3;
        //live = 3;

        cellArray = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {

            for (int a = 0; a < columns; a++)
            {
                cellArray[i, a] = Random.Range(0, 2) == 0;

            }
        }

        DrawTileMap(cellArray);
    }

    public void Clear()
    {
        map.ClearAllTiles();
    }

    void DrawTileMap(bool[,] cellArray)
    {
        for (int i = 0; i < rows; i++)
        {

            for (int a = 0; a < columns; a++)
            {
                if (cellArray[i, a])
                {

                    map.SetTile(new Vector3Int(i, a, 0), white);
                }
                else
                {
                    map.SetTile(new Vector3Int(i, a, 0), black);

                }
            }
        }
    }

    public bool rand;

    public void ChangeRandom(bool b)
    {
        rand = b;
    }

    public void Generation()
    {

        if (rand)
        {
            less = Random.Range(1, 6);
            more = Random.Range(1, 6);
            live = Random.Range(0, 6);
        }

        bool[,] tempArray = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {

            for (int a = 0; a < columns; a++)
            {

                tempArray[i, a] = ShouldaCellLive(cellArray[i, a], GetAliveCellCount(GetNeighbours(i, a)));

            }
        }

        cellArray = tempArray;

        DrawTileMap(cellArray);
    }

    public int less = 2;
    public int more = 3;
    public int live = 3;


    public void ChangeLess(float l)
    {
        less = (int)l;
    }
    public void ChangeMore(float l)
    {
        more = (int)l;
    }
    public void ChangeAlive(float l)
    {
        live = (int)l;
    }

    bool ShouldaCellLive(bool isMainCellAlive, int countOfLivingCells)
    {

        if (isMainCellAlive)
        {
            if (countOfLivingCells < less || countOfLivingCells > more)
            {
                //Debug.Log("Rules 1");

                return false;

            }
            //Debug.Log("Rules 2");

        }
        else
        {
            if (countOfLivingCells == live)
            {
                //Debug.Log("Rules 3");

                return true;

            }
        }

        return isMainCellAlive;
    }
    bool[] GetNeighbours(int x, int y)
    {


        List<bool> neighboursState = new List<bool>();

        //Debug.Log("Neighbors of: " + x + ":" + y);

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int a = y - 1; a <= y + 1; a++)
            {
                if ((i == x && a == y))
                    continue;

                int newI = i;
                int newA = a;

                if (i < 0)
                    newI = rows - 1;

                if (a < 0)
                    newA = columns - 1;

                if (i >= rows)
                    newI = 0;

                if (a >= columns)
                    newA = 0;

                neighboursState.Add(cellArray[newI, newA]);
            }
        }

        return neighboursState.ToArray();
    }

    private int GetAliveCellCount(bool[] neighbours)
    {
        int count = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i])
            {
                count += 1;
            }
        }

        return count;
    }

}
