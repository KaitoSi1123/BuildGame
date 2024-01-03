using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBuilding : MonoBehaviour
{

    public bool isBuild = true; 
    public string nameResourse;
    public int needWodens;
    public int needMetals;
    public int needNails;
    public int needSkins; 

    void OnTriggerEnter(Collider other)
    {
        isBuild = false;
    }

    void OnTriggerExit(Collider other)
    {
        isBuild = true;
    }
}
