using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameScript : MonoBehaviour
{
    private System.Random rand = new System.Random();
    [SerializeField]
    public GameObject GridGroup;
    public GameObject DirtTile;
    public GameObject[] tiles;
    public bool gameOn = false;
    public bool ScanEnabled = false;
    public bool ExtractEnabled = false;
    public int NumOfScans;
    public int NumOfExtracts;
    int score = 0;
    public TextMeshProUGUI ResourcesText;

    void Start()
    {
        ResourcesText.text = "Resources: " + score;
        NumOfScans = 6;
        NumOfExtracts = 3;
        gameObject.SetActive(false);
        for(int i = 0; i < 256; i++)
        {
            tiles[i] = Object.Instantiate(DirtTile);
            tiles[i].transform.SetParent(GridGroup.transform);
            tiles[i].GetComponent<TileScript>().TileID = i;
        }
    }

    void CreateNewResource()
    {
        int Row = Random.Range(0, 11);
        int Base = Random.Range(0 + (Row * 16), 11 + (Row * 16));

        for(int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                if (r == 0 || r == 4)
                {
                    tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 0.5f, 0f);
                }
                if (r == 1 || r == 3)
                {
                    if (c == 0 || c == 4)
                        tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 0.5f, 0f);
                    else
                        tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 1f, 0f);
                }
                if (r == 2)
                {
                    if (c == 0 || c == 4)
                        tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 0.5f, 0f);
                    else if (c == 1 || c == 3)
                        tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 1f, 0f);
                    else
                        tiles[(Base + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(0f, 1f, 0f);
                }
            }
        }
    }

    public void ScanTiles(int TileID)
    {
        TileID = TileID - 17;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
               tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().SetRevealColor();
            }
        }
    }

    public void ExtractTiles(int TileID)
    {
        int extractedTile = TileID;

        if (tiles[(extractedTile)].GetComponent<TileScript>().RevealColor == new Color(0f, 1f, 0f))
            score += 2000;
        else if (tiles[(extractedTile)].GetComponent<TileScript>().RevealColor == new Color(1f, 1f, 0f))
            score += 1000;
        else if (tiles[(extractedTile)].GetComponent<TileScript>().RevealColor == new Color(1f, 0.5f, 0f))
            score += 500;

        //Setting the extracted Tile to red
        tiles[(extractedTile)].GetComponent<TileScript>().RevealColor = new Color(1f, 0f, 0f);

        TileID = TileID - 34;

        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                //If set to orange set it to red
                if (tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor == new Color(1f, 0.5f, 0f))
                {
                    tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 0f, 0f);
                    score += 500;
                }
                //If set to yellow set it to orange
                else if (tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor == new Color(1f, 1f, 0f))
                {
                    tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 0.5f, 0f);
                    score += 1000;
                }
                //If set to green set to yellow 
                else if (tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor == new Color(0f, 1f, 0f))
                {
                    tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().RevealColor = new Color(1f, 1f, 0f);
                    score += 2000;
                }
                tiles[(TileID + c) + (r * 16)].GetComponent<TileScript>().SetRevealColor();
                ResourcesText.text = "Resources: " + score;
            }
        }
        
    }
    public void ResetGame()
    {
        for (int i = 0; i < 256; i++)
        { 
            tiles[i].GetComponent<TileScript>().IsHidden = true;
            tiles[i].GetComponent<TileScript>().SetHiddenColor();
            tiles[i].GetComponent<TileScript>().RevealColor = new Color(1f, 0f, 0f, 1f);
        }
        for (int i = 0; i < 4; i++)
        {
            CreateNewResource();
        }
        score = 0;
        ResourcesText.text = "Resources: " + score;
    }

    public void EnableScanMode()
    {
        if (ScanEnabled == false)
        {
            ScanEnabled = true;
            ExtractEnabled = false;
        }
    }

    public void EnableExtractMode()
    {
        if (ExtractEnabled == false)
        {
            ScanEnabled = false;
            ExtractEnabled = true;
        }
    }

    public void Shuffle()
    {
        //for (int i = 0; i < tiles.Length; i++)
        //    shuffled[i] = tiles[i];
        //shuffled[8].SetActive(false);
        //int p = shuffled.Length;
        //for (int n = p - 1; n > 0; n--)
        //{
        //    int r = rand.Next(0, n);
        //    GameObject t = shuffled[r];
        //    shuffled[r] = shuffled[n];
        //    shuffled[n] = t;
        //}
        //for (int r = 0; r < 3; r++)
        //{
        //    for (int c = 0; c < 3; c++)
        //    {
        //        grid[r, c] = shuffled[(r * 3) + c];
        //        grid[r, c].GetComponent<TileScript>().SetGridIndices(r, c);
        //        grid[r, c].GetComponent<RectTransform>().localPosition = new Vector3((c - 1) * 182, (r - 1) * -182, 0);
        //    }
        //}
        //gameOn = true;
    }

    //public void SwapTiles(int r1, int c1, int r2, int c2)
    //{
    //    GameObject tempTile = grid[r1, c1];
    //    grid[r1, c1] = grid[r2, c2];
    //    grid[r2, c2] = tempTile;
    //    for (int r = 0; r < 3; r++)
    //    {
    //        for (int c = 0; c < 3; c++)
    //        {
    //            grid[r, c].GetComponent<TileScript>().SetGridIndices(r, c);
    //            grid[r, c].GetComponent<RectTransform>().localPosition = new Vector3((c - 1) * 182, (r - 1) * -182, 0);
    //        }
    //    }
    //}
}
