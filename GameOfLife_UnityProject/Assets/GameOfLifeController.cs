using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLifeController : MonoBehaviour
{

    public int grid_rows = 100;
    public int grid_columns = 100;

    public int[,] grid;


    public Texture2D tex;
    private Sprite mySprite;
    private SpriteRenderer sr;


    // Groﬂes Grid
    // Nur die Fragen die entweder leben oder nachbarn haben 

    void Awake()
    {
        grid = new int[grid_rows, grid_columns];

        for(int i = 0; i < grid_rows; i++)
        {
            sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
            
        }



        //transform.position = new Vector3(1.5f, 1.5f, 0.0f);
    }

    void Start()
    {
        mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        sr.sprite = mySprite;
    }


}

