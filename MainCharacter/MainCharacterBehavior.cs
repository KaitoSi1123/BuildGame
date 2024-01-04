using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonCharacter = EasyCharacterMovement.ThirdPersonCharacter;
using ThirdPersonCameraController = EasyCharacterMovement.ThirdPersonCameraController;

public class Player : ThirdPersonCharacter
{

    private float _playerHP = 100f;
    private float _playerSatiety = 40f;
    private float _playerThirst = 70f;
    private float _playerHPMax = 100;
    private float _playerSatietyMax = 100;
    private float _playerThirstMax = 100;



    [HideInInspector] public List<Items> itemsInInventoryPlayer = new List<Items>();
    [HideInInspector] public int playerStrengthAttribute = 20;

    [HideInInspector] public float playerHP { get {return _playerHP;} protected set {_playerHP = value;} }
    [HideInInspector] public float playerSatiety { get {return _playerSatiety;} protected set {_playerSatiety = value;} }
    [HideInInspector] public float playerThirst { get {return _playerThirst;} protected set {_playerThirst = value;} }
    [HideInInspector] public float playerHPMax { get {return _playerHPMax;} protected set {_playerHPMax = value;} }
    [HideInInspector] public float playerSatietyMax { get {return _playerSatietyMax;} protected set {_playerSatietyMax = value;} }
    [HideInInspector] public float playerThirstMax { get {return _playerThirstMax;} protected set {_playerThirstMax = value;} }
    [HideInInspector] public float playerSpeedCollectionResourses = 10f;

    [HideInInspector] public GameObject whatIsItemForPickUp { get; private set; }
    [HideInInspector] public bool isCollectedResourse = false;
    [HideInInspector] protected bool isPlayerInZoneForPickUp = false;
    [HideInInspector] private float timer = 0.0f;
    [HideInInspector] private float waitTime = 60.0f;

    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private IndicatorsOfPlayer _indicatorsOfPlayer;
    //[SerializeField] private GameObject _gameObjectForAnim;

    private ResourseBehavior _resourseBehavior;
    private InitializeItems _initializeItems;
    private Items _itemStruct;



    protected override void Start()
    {

        base.Start();

    }

    protected override void OnTriggerEnter(Collider collider)
    {

        base.OnTriggerEnter(collider);
        IsTouchingZoneForItem(collider);

    }

    protected override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        isPlayerInZoneForPickUp = false;
    }

    protected override void Update()
    {

        base.Update();
        IsTimerForMath();
        IsCollectionResourse();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCollectionResourses();
        }

        //for (int i = 0; i < _skinnedMesh.sharedMesh.blendShapeCount; i++)
        //   _skinnedMesh.SetBlendShapeWeight(i, 100);
    }

    void OnGUI()
    {

        if (isPlayerInZoneForPickUp)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), "Press E, to collect item");
        }

        if (isCollectedResourse)
        {
            GUI.Box(new Rect(800, 10, (_resourseBehavior.resourseHPMax - _resourseBehavior.resourseHP) * 15, 25), "Collecting tree...");
            GUI.Box(new Rect(800, 10, 300, 25), "");
        }

    }

    protected override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKeyDown(KeyCode.E))
        {
            IsAddInInventoryToItem();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            maxWalkSpeed = 2.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            maxWalkSpeed = 5f;
        }
    }

    private void IsTouchingZoneForItem(Collider collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Environment")
        {
            isPlayerInZoneForPickUp = true;
            whatIsItemForPickUp = collision.gameObject;
        }
    }






    public void StopMovement()
    {
        maxWalkSpeed = 0f;
        handleInput = false;
    }

    public void StartMovement()
    {
        maxWalkSpeed = 5f;
        handleInput = true;
    }

    public void MovementController(bool _toggleController)
    {
        if (_toggleController)
        {
            StartMovement();
        }
        else
        {
            StopMovement();
        }
        CursorLocked(_toggleController);
    }

    public void MovementController()
    {
        if(!handleInput)
        {
            StartMovement();
        }
        else
        {
            StopMovement();
        }
        CursorLocked();
    }

    public void CursorLocked()
    {
        this.camera.GetComponent<ThirdPersonCameraController>().lockCursor = !this.camera.GetComponent<ThirdPersonCameraController>().lockCursor;
    }

    public void CursorLocked(bool toggleCursor)
    {
        this.camera.GetComponent<ThirdPersonCameraController>().lockCursor = toggleCursor;
    }

    public void SetHP(float _playerHPForFunc)
    {
        _playerHP += _playerHPForFunc;
        _indicatorsOfPlayer.UpdateIndicators();
    }

    public void SetSatiety(float _playerSatietyForFunc)
    {
        _playerSatiety += _playerSatietyForFunc;
        _indicatorsOfPlayer.UpdateIndicators();
    }

    public void SetThirst(float _playerThirstForFunc)
    {
        _playerThirst += _playerThirstForFunc;
        _indicatorsOfPlayer.UpdateIndicators();
    }

    private void IsTimerForMath()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            timer = timer - waitTime;
            playerSatiety--;
            playerThirst--;
            _indicatorsOfPlayer.UpdateIndicators();
        }
    }



    private void IsCollectionResourse()
    {
        if (isCollectedResourse)
        {
            Vector3 targetPostition = new Vector3(whatIsItemForPickUp.transform.position.x, this.transform.position.y, whatIsItemForPickUp.transform.position.z);
            _resourseBehavior.resourseHP -= Time.deltaTime * playerSpeedCollectionResourses;
            this.transform.LookAt(targetPostition);
            if (_resourseBehavior.resourseHP <= 0)
            {
                StopCollectionResourses();
                itemsInInventoryPlayer.Add(_itemStruct);
                Destroy(whatIsItemForPickUp);
            }
        }
    }

    private void StopCollectionResourses()
    {
        if (isCollectedResourse)
        {
            isCollectedResourse = false;
            StartMovement();
        }
    }

    private void IsAddInInventoryToItem()
    {
        if (isPlayerInZoneForPickUp && whatIsItemForPickUp)
        {
            _initializeItems = whatIsItemForPickUp.GetComponent<InitializeItems>();
            ItemBehavior _itemBevahior = _initializeItems.itemBehavior;
            _itemStruct = _itemBevahior.inventoryStruct[_initializeItems.NameItem];
            _itemStruct.PickUp(this);
            if (_itemStruct.isObjectWithoutWait)
            {
                isPlayerInZoneForPickUp = false;
                Destroy(whatIsItemForPickUp);
            }
            else
            {
                _resourseBehavior = whatIsItemForPickUp.GetComponent<ResourseBehavior>();
            }
        }
    }
}
