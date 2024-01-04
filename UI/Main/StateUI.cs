using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : MonoBehaviour
{
    
    [SerializeField] protected Canvas _inventoryList;
    [SerializeField] protected Canvas _indicatorsOfPlayer;
    [SerializeField] protected Canvas _buildingInterface; 
    public Player player;

    public void ChangeState(bool _value)
    {
        player.MovementController(_value);
    }
    public void ChangeState()
    {
        player.MovementController();
    }

    public Canvas[] GiveCanvas()
    { 
        return this.GetComponentsInChildren<Canvas>();
    }

}
