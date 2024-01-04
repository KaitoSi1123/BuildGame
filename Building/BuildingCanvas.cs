using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuildingCanvas : MonoBehaviour
{

    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField] private List<Material> _materialTrue;
    [SerializeField] private List<Material> _materialFalse;
    [SerializeField] private GameObject _gameObjectContent;
    [SerializeField] private GameObject _buttonExample;
    private bool isCreateBuilding;
    private Canvas _canvas;
    private GameObject _newBuilding;
    private Transform _transform;
    private GameObject _prefab;
    private Vector3 _rotationOfBuild = new Vector3(0f, 0f, 0f);
    private Canvas[] _canvasArray;
    private StateUI _stateUI;
    private Player _player; 
    private GameObject _selectedButton;
    private List<GameObject> _listWithButtons = new List<GameObject>();

    void Start()
    {
        _stateUI = this.GetComponentInParent<StateUI>();
        _canvas = this.GetComponent<Canvas>();
        _canvasArray = _stateUI.GiveCanvas();
        _player = _stateUI.player;
        _canvas.enabled = false;
        isCreateBuilding = false;
        _transform = _player.gameObject.transform;
        foreach(GameObject child in _prefabs)
        {
            GameObject newButton = Instantiate(_buttonExample);
            InitialBuilding _initialBuilding = child.GetComponent<InitialBuilding>();
            newButton.transform.SetParent(_gameObjectContent.transform, false);
            newButton.GetComponentInChildren<Text>().text = $"{_initialBuilding.nameBuilding}";
            newButton.GetComponent<Button>().onClick.AddListener(delegate{CreateMeshGhostOfBuild(child, newButton);});
            _listWithButtons.Add(newButton);
        }
    }

    void Update()
    {
        OnMenuBuildingIsOpen();
        CreateBuild();
        UpdateRotationOfGhostBuild();
    } 

    private void OnMenuBuildingIsOpen()
    {
        if(Input.GetKeyDown(KeyCode.M) || ((Input.GetKeyDown(KeyCode.Escape) && _canvas.enabled)))
        {
            isCreateBuilding = false;
            if(_newBuilding)
            {
                Destroy(_newBuilding);
            }
            else
            {
                CloseHoverButton();
                _selectedButton = null;
                this.GetComponentsInChildren<Text>().Last().text = "";
                _stateUI.ChangeState(_canvas.enabled);
                _canvasArray[1].GetComponent<InventoryList>().SortInventoryList("isResourse");
                _canvasArray[1].enabled = !_canvas.enabled;
                _canvas.enabled = !_canvas.enabled;
            }
        }
    }

    private void CreateBuild()
    {
        if(isCreateBuilding && _newBuilding && !_canvasArray[1].enabled)
        {
            if(!_newBuilding.TryGetComponent<InitialBuilding>(out InitialBuilding isBuild))
            {
                _newBuilding.AddComponent<InitialBuilding>();
            }
            InitialBuilding _checkCollider = _newBuilding.GetComponent<InitialBuilding>();
            MeshRenderer _meshRenderer = _newBuilding.GetComponent<MeshRenderer>();
            if (_checkCollider.isBuild && !_player.IsFalling())
            {
                _meshRenderer.SetMaterials(_materialTrue);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CreateMeshGhostOfBuild();
                }
            }
            else
            {
                _meshRenderer.SetMaterials(_materialFalse);
            }
        }
    }

    private void CreateMeshGhostOfBuild(GameObject _prefabInstantiate, GameObject _button)
    {
        CloseHoverButton();
        if(_selectedButton == _button)
        {
            _newBuilding = Instantiate(_prefabInstantiate, _transform.position+(_transform.forward*2), _transform.rotation*Quaternion.Euler(_rotationOfBuild[0], _rotationOfBuild[1], _rotationOfBuild[2]), _player.transform);
            _prefab = _prefabInstantiate;
            isCreateBuilding = true;
            _stateUI.ChangeState(true);
            _canvas.enabled = false;
            _canvasArray[1].enabled = false;
        }
        else
        {
            this.GetComponentsInChildren<Text>().Last().text = $"Description: {_prefabInstantiate.GetComponent<InitialBuilding>().descriptionBuilding}";
        }
        _button.GetComponent<Image>().enabled = true;
        _selectedButton = _button;
    }

    private void CloseHoverButton()
    {
        foreach(GameObject child in _listWithButtons)
        {
            child.GetComponent<Image>().enabled = false;
        }
    }
    private void CreateMeshGhostOfBuild()
    {
        Instantiate(_prefab, _transform.position+(_transform.forward*2), _transform.rotation*Quaternion.Euler(_rotationOfBuild[0], _rotationOfBuild[1], _rotationOfBuild[2]));
    }

    private void UpdateMeshGhostOfBuild()
    {
        _newBuilding.transform.rotation = _transform.rotation*Quaternion.Euler(_rotationOfBuild[0], _rotationOfBuild[1], _rotationOfBuild[2]);
    }

    private void UpdateRotationOfGhostBuild()
    {
        if(Input.GetKeyDown(KeyCode.Q) && isCreateBuilding && _newBuilding)
        {
            _rotationOfBuild += new Vector3(0f, -10f, 0f);
            UpdateMeshGhostOfBuild();
        }
        if(Input.GetKeyDown(KeyCode.E) && isCreateBuilding && _newBuilding)
        {
            _rotationOfBuild += new Vector3(0f, 10f, 0f);
            UpdateMeshGhostOfBuild();
        }
    }

    private void CountResourses()
    {
        foreach(Items child in _player.itemsInInventoryPlayer)
        {
            if(child.isResourse)
            {
                
            }
        }
    }
}