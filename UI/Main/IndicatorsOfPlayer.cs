using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorsOfPlayer : MonoBehaviour
{

    [SerializeField] private Player _player;

    void Start()
    {
        UpdateIndicators();
    }

    public void UpdateIndicators()
    {
        Image[] _imageArray = this.GetComponentsInChildren<Image>();
        Text[] _textArray = this.GetComponentsInChildren<Text>();
        float _playerHP = _player.playerHP/_player.playerHPMax;
        float _playerThirst = _player.playerThirst/_player.playerThirstMax;
        float _playerSatiety = _player.playerSatiety/_player.playerSatietyMax;
        _imageArray[1].fillAmount = _playerHP;
        _imageArray[3].fillAmount = _playerThirst;
        _imageArray[5].fillAmount = _playerSatiety;
        _textArray[0].text = $"{(float)_playerHP*100}%";
        _textArray[1].text = $"{(float)_playerThirst*100}%";
        _textArray[2].text = $"{(float)_playerSatiety*100}%";
    }

}
