using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCanvas : MonoBehaviour
{

    [SerializeField] private Player _player; 
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


    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        isCreateBuilding = false;
        _transform = _player.gameObject.transform;
        foreach(GameObject child in _prefabs)
        {
            GameObject newButton = Instantiate(_buttonExample);
            InitialBuilding _initialBuilding = child.GetComponent<InitialBuilding>();
            newButton.transform.SetParent(_gameObjectContent.transform, false);
            newButton.GetComponentInChildren<Text>().text = $"{_initialBuilding.nameResourse}\nWoodens: {_initialBuilding.needWodens}\nMetals: {_initialBuilding.needMetals}";
            newButton.GetComponent<Button>().onClick.AddListener(delegate{CreateMeshGhostOfBuild(child);});
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
        if(Input.GetKeyDown(KeyCode.M))
        {
            isCreateBuilding = false;
            if(_newBuilding)
            {
                Destroy(_newBuilding);
            }
            else
            {
                _player.MovementController();
                _canvas.enabled = !_canvas.enabled;
            }
        }
    }

    private void CreateBuild()
    {
        if(isCreateBuilding && _newBuilding)
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

    private void CreateMeshGhostOfBuild(GameObject _prefabInstantiate)
    {
        _newBuilding = Instantiate(_prefabInstantiate, _transform.position+(_transform.forward*2), _transform.rotation*Quaternion.Euler(_rotationOfBuild[0], _rotationOfBuild[1], _rotationOfBuild[2]), _player.transform);
        _prefab = _prefabInstantiate;
        _player.MovementController();
        isCreateBuilding = true;
        _canvas.enabled = false;
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