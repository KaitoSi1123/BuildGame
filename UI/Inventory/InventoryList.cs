using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryList : MonoBehaviour
{

    [SerializeField] private GameObject _gameObjectContent;
    [SerializeField] private GameObject _buttonExample;

    private StateUI _stateUI;
    private Canvas _canvas;
    private List<GameObject> listOfButtons = new List<GameObject>();
    private List<Items> listOfItems = new List<Items>();
    private Text[] _slotsButtonText;
    private GameObject _selectedButton;
    private Player _player;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _stateUI = this.GetComponentInParent<StateUI>();
        _player = _stateUI.player;
    }
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.I)) || ((Input.GetKeyDown(KeyCode.Escape) && _canvas.enabled)))
        {
            _canvas.enabled = !_canvas.enabled;
            if(!_stateUI.GiveCanvas()[3].enabled)
            {
                _stateUI.ChangeState(!_canvas.enabled);
            }
            this.GetComponentsInChildren<Text>().Last().text = "";
            OnInventoryOpen();
        }
    }

    public void SortInventoryList(string _valueToSort)
    {
        List<Items> _itemsForDisplay = new List<Items>();
        foreach(Items child in _player.itemsInInventoryPlayer)
        {
            var _value = child.GetType().GetField(_valueToSort).GetValue(child);
            if(_value is bool && (bool)_value)
            {
                _itemsForDisplay.Add(child);
            } 
        }
        CreateInventoryList(_itemsForDisplay);
    }

    public void CreateInventoryList(List<Items> _itemsForDisplay)
    {
        foreach(GameObject child in listOfButtons)
        {
            Destroy(child);
        }
        listOfButtons.Clear();
        listOfItems.Clear();
        foreach(Items _item in _itemsForDisplay)
        {
            if(!listOfItems.Contains(_item))
            {
                GameObject newButton = Instantiate(_buttonExample);
                _slotsButtonText = newButton.GetComponentsInChildren<Text>();
                newButton.transform.SetParent(_gameObjectContent.transform, false);
                _slotsButtonText[0].text = $"{_item.name}";
                _slotsButtonText[1].text = $"{_item.weightOfItem}";
                _slotsButtonText[2].text = $"1";
                newButton.GetComponent<Button>().onClick.AddListener(delegate{ClickSlotInventory(_item, newButton);});
                listOfButtons.Add(newButton);
                listOfItems.Add(_item);
            }
            else
            {
                GameObject _button = listOfButtons[listOfItems.IndexOf(_item)];
                _slotsButtonText = _button.GetComponentsInChildren<Text>();
                if (int.TryParse(_slotsButtonText[2].text, out int x))
                {
                    _slotsButtonText[2].text = $"{x+1}";
                    _slotsButtonText[1].text = $"{_item.weightOfItem*(x+1)}";
                }
            }
        }
    }

    private void OnInventoryOpen()
    {
        CreateInventoryList(_player.itemsInInventoryPlayer);
    }

    private void ClickSlotInventory(Items _structOfItem, GameObject _button)
    {
        foreach(GameObject child in listOfButtons)
        {
            child.GetComponent<Image>().enabled = false;
        }
        if(_selectedButton == _button && !_structOfItem.isResourse)
        {
            _structOfItem.Use(_player);
            OnInventoryOpen();
        }
        else
        {
            _button.GetComponent<Image>().enabled = true;
            Text[] _arrayText = _canvas.GetComponentsInChildren<Text>();
            _arrayText.Last().text = $"Descrtion: {_structOfItem.descrionItem}";
        }
        _selectedButton = _button;
    }
}
