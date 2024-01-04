using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeItems : MonoBehaviour
{

    [SerializeField] public string NameItem;
    [SerializeField] public ItemBehavior itemBehavior;
    [SerializeField] private float _weightOfItem;
    [SerializeField] private float _heathItem;
    [SerializeField] private float _satietyItem;
    [SerializeField] private float _thirstItem;
    [SerializeField] private bool _isObjectWithoutWait;
    [SerializeField] private bool _isResourse;
    [SerializeField] private string _description;
    private Items Item;

    void Start()
    {
        if(!itemBehavior.inventoryStruct.ContainsKey(NameItem))
        {
            Items Item = new Items(_weightOfItem, NameItem, this.gameObject, _heathItem, _satietyItem, _thirstItem, _isObjectWithoutWait, _isResourse, _description);
            itemBehavior.inventoryStruct.Add(NameItem, Item);
        }
    }
}  
