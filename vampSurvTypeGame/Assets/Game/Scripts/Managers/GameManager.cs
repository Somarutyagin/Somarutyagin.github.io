using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    [HideInInspector] public float Border {  get; private set; }
    private int _valueScore;
    public int Score
    {
        get
        {
            return _valueScore;
        }
        set
        {
            _valueScore = value;
            if (_valueScore >= 20)
            {
                EndGame(true);
            }

            if (_valueScore % 5 == 0 && activeGameStatus == GameStatus.Play)
            {
                UIManager.Instance.UIUpgradeDisplayActivate();

                activeGameStatus = GameStatus.Pause;
            }
        }
    }

    private GameStatus _valueGameStatus;
    private const GameStatus _valueGameStatusDefault = GameStatus.Menu;
    public GameStatus activeGameStatus
    {
        get
        {
            return _valueGameStatus;
        }
        set
        {
            _valueGameStatus = value;
            if (_valueGameStatus == GameStatus.Play)
            {
                Time.timeScale = 1f;

                SpawnManager.Instance.StartSpawn();
            }
            else
            {
                Time.timeScale = 0f;

                SpawnManager.Instance.StopSpawn();
            }
            UIManager.Instance.UISwitch(_valueGameStatus);
        }
    }
    private PlayerConfig _playerConfig;

    public void Init()
    {
        _valueScore = 0;
        activeGameStatus = _valueGameStatusDefault;
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
        Init();

        _playerConfig = GameObject.Find("Player").GetComponent<PlayerConfig>();
        GameObject map = GameObject.Find("Map");
        if (map != null)
            Border = map.transform.GetChild(0).position.x - 1;
    }

    public void Lose()
    {
        EndGame(false);
    }
    private void EndGame(bool isWin)
    {
        activeGameStatus = GameStatus.Pause;
        ResetGame();
        if (isWin)
            UIManager.Instance.UIWinDisplayActivate();
        else
            UIManager.Instance.UILoseDisplayActivate();
    }
    public void ResetGame()
    {
        Score = 0;

        SpawnManager.Instance.Reset();
        _playerConfig.Reset();
    }
}
