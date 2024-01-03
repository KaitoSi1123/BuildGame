using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : MonoBehaviour
{

    [SerializeField] protected Canvas _inventoryList;
    [SerializeField] protected Canvas _indicatorsOfPlayer;
    [SerializeField] protected Canvas _buildingInterface;


    public void StateChange()
    {
        _indicatorsOfPlayer.enabled = !_inventoryList.enabled;
    }

}
