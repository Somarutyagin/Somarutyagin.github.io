using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerConfig _playerConfig;

    private GameObject _uIMenu;
    private GameObject _uIGame;

    private GameObject _uIUpgradeDisplay;
    private GameObject _uILoseDisplay;
    private GameObject _uIPauseDisplay;
    private GameObject _uIWinDisplay;

    private Button _startBtn;
    private Button _menuBtn1, _menuBtn2;
    private Button _upgradeBtn1, _upgradeBtn2, _upgradeBtn3;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Initialize()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        _playerConfig = GameObject.Find("Player").GetComponent<PlayerConfig>();

        _uIMenu = canvas.GetChild(0).gameObject;
        _uIGame = canvas.GetChild(1).gameObject;
        _startBtn = canvas.GetChild(0).GetChild(1).GetComponent<Button>();
        _uIUpgradeDisplay = canvas.GetChild(1).GetChild(0).gameObject;
        _uIWinDisplay = canvas.GetChild(1).GetChild(1).gameObject;
        _uILoseDisplay = canvas.GetChild(1).GetChild(2).gameObject;
        _uIPauseDisplay = canvas.GetChild(1).GetChild(3).gameObject;
        _menuBtn1 = canvas.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<Button>();
        _menuBtn2 = canvas.GetChild(1).GetChild(2).GetChild(0).GetChild(1).GetComponent<Button>();
        _upgradeBtn1 = canvas.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>();
        _upgradeBtn2 = canvas.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>();
        _upgradeBtn3 = canvas.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>();

        _startBtn.Add(OnPlayButtonPressed);
        _menuBtn1.Add(OnMenuButtonPressed);
        _menuBtn2.Add(OnMenuButtonPressed);

        _upgradeBtn1.Add(OnUpgradeButtonPressed1);
        _upgradeBtn2.Add(OnUpgradeButtonPressed2);
        _upgradeBtn3.Add(OnUpgradeButtonPressed3);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.activeGameStatus == GameStatus.Play && !_uIPauseDisplay.activeSelf)
        {
            OnPauseButtonPressed();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.activeGameStatus == GameStatus.Pause && _uIPauseDisplay.activeSelf)
        {
            OnContinueButtonPressed();
        }
    }

    public void OnUpgradeButtonPressed1()
    {
        _playerConfig.DamageScaler += 0.5f;

        CloseDisplay();
        GameManager.Instance.activeGameStatus = GameStatus.Play;
    }
    public void OnUpgradeButtonPressed2()
    {
        _playerConfig.SpeedScaler += 0.5f;

        CloseDisplay();
        GameManager.Instance.activeGameStatus = GameStatus.Play;
    }
    public void OnUpgradeButtonPressed3()
    {
        _playerConfig.FullHeal();

        CloseDisplay();
        GameManager.Instance.activeGameStatus = GameStatus.Play;
    }
    public void OnPlayButtonPressed()
    {
        GameManager.Instance.activeGameStatus = GameStatus.Play;
    }
    public void OnMenuButtonPressed()
    {
        GameManager.Instance.activeGameStatus = GameStatus.Menu;

        CloseDisplay();
    }
    public void OnPauseButtonPressed()
    {
        UIPauseDisplayActivate();
        GameManager.Instance.activeGameStatus = GameStatus.Pause;
    }
    public void OnContinueButtonPressed()
    {
        CloseDisplay();
        GameManager.Instance.activeGameStatus = GameStatus.Play;
    }
    public void UIUpgradeDisplayActivate()
    {
        _uIUpgradeDisplay.SetActive(true);
    }
    public void UILoseDisplayActivate()
    {
        _uILoseDisplay.SetActive(true);
    }
    public void UIWinDisplayActivate()
    {
        _uIWinDisplay.SetActive(true);
    }
    public void UIPauseDisplayActivate()
    {
        _uIPauseDisplay.SetActive(true);
    }

    public void UISwitch(GameStatus activeStatus)
    {
        if (activeStatus == GameStatus.Menu)
            GOSwitch(_uIMenu, _uIGame);
        else if (activeStatus == GameStatus.Play)
            GOSwitch(_uIGame, _uIMenu);
    }
    private void GOSwitch(GameObject enable, GameObject disable)
    {
        enable.SetActive(true);
        disable.SetActive(false);
    }
    private void CloseDisplay()
    {
        _uIUpgradeDisplay.SetActive(false);
        _uIWinDisplay.SetActive(false);
        _uILoseDisplay.SetActive(false);
        _uIPauseDisplay.SetActive(false);
    }

}
