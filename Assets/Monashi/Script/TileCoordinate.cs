using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinate : MonoBehaviour
{

    [System.Serializable]
    public struct Coordinate
    {
       public int x,y;
       public bool Close;
    }
   
    public Coordinate coordinate;
}
