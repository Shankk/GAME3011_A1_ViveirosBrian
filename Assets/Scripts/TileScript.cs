using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour, IPointerClickHandler
{
    public int TileID;
    public bool IsHidden = true;
    public Color HiddenColor;
    public Color RevealColor;
    public MinigameScript mgs;
    
    void Start()
    {
        mgs = gameObject.GetComponentInParent<MinigameScript>();
        HiddenColor = new Color(0f, 0f, 0f, 1f);
        RevealColor = new Color(1f, 0f, 0f, 1f);
    }

    public void SetHiddenColor()
    {
        HiddenColor = new Color(0f, 0f, 0f, 1f);
        gameObject.GetComponent<Image>().color = HiddenColor;
    }

    public void SetRevealColor()
    {
        gameObject.GetComponent<Image>().color = RevealColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mgs.gameOn == false)
            return;
        if( mgs.ScanEnabled && mgs.NumOfScans > 0)
        {
            Debug.Log("Object Name: " + gameObject.name + "Tile ID: " + TileID);
            mgs.ScanTiles(TileID);
        }

        if(mgs.ExtractEnabled && mgs.NumOfExtracts > 0)
        {
            
            Debug.Log("Object Name: " + gameObject.name + "Tile ID: " + TileID);
            mgs.ExtractTiles(TileID);
            
        }
    }
}
