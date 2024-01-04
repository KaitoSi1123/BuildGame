using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBuilding : MonoBehaviour
{

    [HideInInspector] public bool isBuild = true; 
    public string nameBuilding;
    public int needWodens;
    public int needMetals;
    public int needNails;
    public int needSkins; 
    public string descriptionBuilding; 

    void OnTriggerEnter(Collider other)
    {
        isBuild = false;
    }

    void OnTriggerExit(Collider other)
    {
        isBuild = true;
    }
}
