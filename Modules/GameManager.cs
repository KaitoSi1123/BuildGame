using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public List<GameObject> itemsInGameMap = new List<GameObject>();
    [HideInInspector] public ItemBehavior itemBehavior;

}
