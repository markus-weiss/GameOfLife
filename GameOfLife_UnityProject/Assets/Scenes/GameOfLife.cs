using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    // Start is called before the first frame update


    public int rows, columns = 10;

    //public bool[,] area;
    public bool[,] area = new bool[,] {

        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   true    ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   true    ,   false   ,   true    ,   true   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   true    ,   false   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   false   ,   true   ,   true   ,   false   ,   false   ,   false   ,   false   }   ,
        {   false   ,   false   ,   false   ,   false   ,   true   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
        {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,

    };



    public GameObject[,] cellArray;
    public GameObject cell;

    public int rowOffset = 2;
    public int columnOffset = 2;


    public float SecondsToWait;

    public bool randomize;
    private Coroutine golRoutine;

    public GameObject[] neighbourTestArray;

    void Start()
    {

        Create();

        Run();
    }

    public void Create()
    {
        //area = new bool[rows, columns];
        area = new bool[,] {

            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   true    ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   true    ,   false   ,   true    ,   true   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   true    ,   false   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   false   ,   true   ,   true   ,   false   ,   false   ,   false   ,   false   }   ,
            {   false   ,   false   ,   false   ,   false   ,   true   ,   false   ,   false   ,   false   ,   false   ,   true   }   ,
            {   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   ,   false   }   ,

        };


        cellArray = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int a = 0; a < columns; a++)
            {
                if (randomize)
                    area[i, a] = Random.Range(0, 2) == 0;


                cellArray[i, a] = Instantiate(cell, new Vector3(i, 0, a), Quaternion.identity, transform);
                cellArray[i, a].SetActive(area[i, a]);

            }
        }

        Debug.Log(GetAliveCellCount(GetNeighbours(1, 1)));
    }

    public void Destroy()
    {

        for (int i = transform.childCount - 1; i > 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }


    }

    internal void Stop()
    {
        StopCoroutine(golRoutine);
    }

    public void Run()
    {
        golRoutine = StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {

        while (true)
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSecondsRealtime(SecondsToWait);

            NextGeneration();
        }
    }

    public void NextGeneration()
    {
        Debug.Log(GetAliveCellCount(GetNeighbours(1, 1)));

        bool[,] tempArray = new bool[rows, columns];

        tempArray = area;

        for (int i = 0; i < rows; i++)
        {
            for (int a = 0; a < columns; a++)
            {
                tempArray[i, a] = ShouldaCellLive(area[i, a], GetAliveCellCount(GetNeighbours(i, a)));

            }
        }

        area = tempArray;

        for (int i = 0; i < rows; i++)
        {
            for (int a = 0; a < columns; a++)
            {
                cellArray[i, a].SetActive(area[i, a]);
            }
        }


    }


    private int GetAliveCellCount(bool[] GetNeighbours)
    {
        int count = 0;
        for (int i = 0; i < GetNeighbours.Length; i++)
        {
            if (GetNeighbours[i])
            {
                count += 1;
            }
        }

        return count;
    }


    bool ShouldaCellLive(bool isMainCellAlive, int countOfLivingCells)
    {

        if (countOfLivingCells < 2 || countOfLivingCells > 3)
        {
            //Debug.Log("Rules 1");

            return false;

        }
        else if (countOfLivingCells == 2 || countOfLivingCells == 3)
        {
            //Debug.Log("Rules 2");

            return true;
        }
        else if (isMainCellAlive == false && countOfLivingCells == 3)
        {
            //Debug.Log("Rules 3");

            return true;

        }
        else
            return false;
    }

    bool[] GetNeighbours(int x, int y)
    {
        List<bool> neighboursState = new List<bool>();


        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int a = y - 1; a <= y + 1; a++)
            {
                if (i < 0 || a < 0 || i >= rows || a >= columns || (i == x && a == y))
                {
                    // Ask on other side od Array
                }
                else
                {
                    neighboursState.Add(area[i, a]);


                }
            }
        }

        return neighboursState.ToArray();
    }
}
