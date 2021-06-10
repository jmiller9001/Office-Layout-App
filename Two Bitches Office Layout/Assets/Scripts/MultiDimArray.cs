using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDimArray : MonoBehaviour
{

    [SerializeField] GameObject gameObject;
    [SerializeField] int xArray = 4;
    [SerializeField] int yArray = 2;
    [SerializeField] Texture circle;
    [SerializeField] Texture bench;

    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        Zone zone1 = new Zone(2, 2, 0, 0, Color.red, "U", "Desk");
        Zone zone2 = new Zone(2, 1, 3, 3, Color.blue, "U", "Desk");
        Zone zone3 = new Zone(2, 2, 5, 5, Color.green, "U", "Desk");
        Zone zone4 = new Zone(3, 2, 3, 7, Color.red, "U", "Table");
        Zone zone5 = new Zone(3, 2, 3, 7, Color.red, "U", "Table");

        Zone[] zones = new Zone[5] { zone1, zone2, zone3, zone4, zone5 };
        Zone[] placedElements = new Zone[5];



        GameObject[,] goArray = new GameObject[xArray, yArray];

        for (int i = 0; i < xArray; i++)
        {
            for (int j = 0; j < yArray; j++)
            {
                // Debug.Log(array[i, j]); 
                goArray[i, j] = Instantiate(gameObject, new Vector2(i * 1.02f, j * 1.02f), gameObject.transform.rotation);

                for (int k = 0; k < zones.Length; k++)
                {
                    Zone z = zones[k];
                    if ((i >= z.startX && i < z.startX + z.groupSizeX) && (j >= z.startY && j < z.startY + z.groupSizeY))
                    {
                        for (int m = 0; m < placedElements.Length; m++)
                        {
                            if(m == k){
                                break;
                            }
                            if (placedElements[m] != null && z.startY == placedElements[m].startY)
                            {
                                zones[k].startY = z.startY + placedElements[m].groupSizeY;
                                z = zones[k];
                            }
                        }

                        goArray[i, j].GetComponent<Renderer>().material.color = z.col;
                        goArray[i, j].GetComponent<SquareProperties>().direction = z.direction;
                        goArray[i, j].GetComponent<SquareProperties>().type = z.type;
                        goArray[i, j].GetComponent<SquareProperties>().groupSizeX = z.groupSizeX;
                        goArray[i, j].GetComponent<SquareProperties>().groupSizeY = z.groupSizeY;
                        goArray[i, j].GetComponent<SquareProperties>().startX = z.startX;
                        goArray[i, j].GetComponent<SquareProperties>().startY = z.startY;
                    }

                    placedElements[k] = zones[k];
                }

            }
        }

        for (int k = 0; k < zones.Length; k++)
        {
            Zone z = zones[k];
            if (z.type == "Table")
            {
                //Delete squares on either side of the chair
                Destroy(goArray[z.startX, z.startY]);
                Destroy(goArray[z.startX + 2, z.startY]);


                //add chair texture to chair square
                Material m_mat = new Material(mat);
                m_mat.mainTexture = circle;
                goArray[z.startX + 1, z.startY].GetComponent<Renderer>().material = m_mat;


                //add bench tex to bench squares
                for (int i = 0; i < 3; i++)
                {

                    Material m_mat1 = new Material(mat);
                    m_mat1.mainTexture = bench;
                    goArray[z.startX + i, z.startY + 1].GetComponent<Renderer>().material = m_mat1;
                    goArray[z.startX + i, z.startY + 1].GetComponent<Renderer>().material.color = z.col;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Zone
{

    public int groupSizeX;
    public int groupSizeY;
    public int startX;
    public int startY;
    public Color col;
    public string direction, type;


    public Zone(int gX, int gY, int sX, int sY, Color c, string d, string t)
    {
        groupSizeX = gX;
        groupSizeY = gY;
        startX = sX;
        startY = sY;
        col = c;
        direction = d;
        type = t;
    }
}
