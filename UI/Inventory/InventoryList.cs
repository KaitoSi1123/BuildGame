using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryList : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private StateUI _stateUI;
    [SerializeField] private GameObject _gameObjectContent;
    [SerializeField] private GameObject _buttonExample;
    
    private Canvas _canvas;
    private List<GameObject> listOfButtons = new List<GameObject>();
    private List<Items> listOfItems = new List<Items>();
    private Text[] _slotsButtonText;
    private GameObject _selectedButton;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _canvas.enabled = !_canvas.enabled;
            _stateUI.StateChange();
            OnInventoryOpen();
        }

    }

    private void OnInventoryOpen()
    {
        if(_canvas.enabled)
        {
            _player.StopMovement();
            _player.CursorLocked();
            foreach(Items _item in _player.itemsInInventoryPlayer)
            {
                if(!listOfItems.Contains(_item))
                {
                    GameObject newButton = Instantiate(_buttonExample);
                    _slotsButtonText = newButton.GetComponentsInChildren<Text>();
                    newButton.transform.SetParent(_gameObjectContent.transform, false);
                    _slotsButtonText[0].text = $"{_item.name}";
                    _slotsButtonText[1].text = $"{_item.weightOfItem}"; 
                    _slotsButtonText[2].text = $"1"; 
                    if(!_item.isResourse)
                    {
                        newButton.GetComponent<Button>().onClick.AddListener(delegate{ClickSlotInventory(_item, newButton);});
                    }
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
        else
        {
            foreach(GameObject child in listOfButtons)
            {
                Destroy(child);
            }
            listOfButtons.Clear();
            listOfItems.Clear();
            _player.CursorLocked(!_canvas.enabled);
            _player.MovementController(!_canvas.enabled);
        }
    }

    public void ClickSlotInventory(Items _structOfItem, GameObject _button)
    {
        foreach(GameObject child in listOfButtons)
        {
            child.GetComponent<Image>().enabled = false;
        }
        if(_selectedButton == _button)
        {
            _structOfItem.Use(_player);
            Destroy(_button);
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
