using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDimArray : MonoBehaviour
{
    
    [SerializeField] GameObject gameObject;
    [SerializeField] int xArray = 4;
    [SerializeField] int yArray = 2;

    
    // Start is called before the first frame update
    void Start()
    {

        int groupSizeX = 3;
        int groupSizeY = 2;
        int startX = 2;
        int startY = 4;
        string col = "Red";
        
        GameObject[,] goArray = new GameObject[xArray, yArray]; 
    
        for(int i = 0; i < xArray; i++)
        {
            for(int j = 0; j < yArray; j++)
            {
                // Debug.Log(array[i, j]); 
                goArray[i, j] = Instantiate(gameObject, new Vector2(i * 1.02f, j * 1.02f), transform.rotation);
                if ((i >= startX && i < startX + groupSizeX) && (j >= startY && j < startY + groupSizeY))
                {
                    goArray[i, j].GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
                    goArray[i, j].GetComponent<SquareProperties>().name = col;
                    goArray[i, j].GetComponent<SquareProperties>().groupSizeX = groupSizeX;
                    goArray[i, j].GetComponent<SquareProperties>().groupSizeY = groupSizeY;
                    goArray[i, j].GetComponent<SquareProperties>().startX = startX;
                    goArray[i, j].GetComponent<SquareProperties>().startY = startY;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
