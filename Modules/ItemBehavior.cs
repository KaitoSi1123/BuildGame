using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    [HideInInspector] public Dictionary<string, Items> inventoryStruct = new Dictionary<string, Items>();
    [SerializeField] public GameManager gameManager;

}


public struct Items
{

    public float weightOfItem { get; private set; }
    public string name { get; private set; }
    public float heathItem { get; private set; }
    public float satietyItem { get; private set; }
    public float thirstItem { get; private set; }
    public bool isObjectWithoutWait { get; private set; }
    public GameObject typeOfItem { get; private set; }
    public bool isResourse { get; private set; }
    public string descrionItem { get; private set; }

    public Items(float _weightOfItem, string _name, GameObject _typeOfItem, float _heathItem, float _satietyItem, float _thirstItem, bool _isObjectWithoutWait, bool _isResourse, string _descrionItem)
    {
        this.weightOfItem = _weightOfItem;
        this.name = _name;
        this.typeOfItem = _typeOfItem;
        this.heathItem = _heathItem;
        this.satietyItem = _satietyItem;
        this.thirstItem = _thirstItem;
        this.isObjectWithoutWait = _isObjectWithoutWait;
        this.isResourse = _isResourse;
        this.descrionItem = _descrionItem;
    }

    public void Use(Player _player)
    {
        if(_player.itemsInInventoryPlayer.Contains(this) && !isResourse)
        {
            _player.SetHP(heathItem);
            _player.SetSatiety(satietyItem);
            _player.SetThirst(thirstItem);
            _player.itemsInInventoryPlayer.Remove(this);
        }
    }

    public void PickUp(Player _player)
    {
        if (!isObjectWithoutWait)
        {
            _player.isCollectedResourse = true;
            _player.StopMovement();
        }
        else
        {
            _player.itemsInInventoryPlayer.Add(this);
        }
    }
}