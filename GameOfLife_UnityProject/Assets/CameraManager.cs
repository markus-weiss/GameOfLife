using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera main;
    public int[] resolution;

    public GameObject golController;


    // Start is called before the first frame update
    void Start()
    {
        main = GetComponentInChildren<Camera>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
