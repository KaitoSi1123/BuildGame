using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorsOfPlayer : MonoBehaviour
{

    [SerializeField] private Player _player;

    void Start()
    {
        UpdateIndicators();
    }

    public void UpdateIndicators()
    {
        this.GetComponent<TextMeshProUGUI>().text = $"Player HP: {_player.playerHP}\nPlayer Satiety: {_player.playerSatiety}\nPlayer Thirst: {_player.playerThirst}";
    }

}
