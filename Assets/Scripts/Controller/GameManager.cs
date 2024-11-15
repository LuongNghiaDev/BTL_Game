using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public partial class GameManager : SerializedMonoBehaviour
{
    private static GameManager instance;

    [FoldoutGroup("Persistant Component", false)]
    [SerializeField] private UIManager uiManager;
    [FoldoutGroup("Persistant Component")]
    [SerializeField] private Camera mainCamera;

    public event Action GamePaused;
    public event Action GameResumed;

    public GameFSM GameStateController { get; private set; }
    public Camera MainCamera => mainCamera;
    public UIManager UiManager => uiManager;

    [Space]
    [BoxGroup("Level")]
    [SerializeField] private LevelConstraint levelConstraint;

    private IDataLevel dataLevel;
    public bool IsLevelLoading { get; private set; }
    public ILevelInfo DataLevel => dataLevel;
    public int CurrentLevel => DataLevel.GetCurrentLevel();
    private LevelManager currentLevelManager;
    public LevelManager LevelManager
    {
        get => currentLevelManager;
        private set => currentLevelManager = value;
    }
    public PlayerDataManager PlayerDataManager => PlayerDataManager.Instance;
    public Profile Profile { get; private set; }
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        GameStateController = new GameFSM(this);
        Profile = new Profile();

        //DOTween.Init().SetCapacity(200, 125);

        dataLevel = PlayerDataManager.GetDataLevel(levelConstraint);
        dataLevel.LevelConstraint = levelConstraint;
        Debug.Log(dataLevel);
    }

    private void Start()
    {
        //dataLevel.SetLevel(LevelType.Normal, 1);
        //uiManager.Init();
        //LoadLevel();
    }

    /// <summary>
    /// Load level mới và xóa level hiện tại
    /// </summary>
    public void LoadLevel()
    {
        
    }


    /// <summary>
    /// Đưa game về state Lobby và khởi tạo lại các giá trị cần thiết cho mỗi level mới.
    /// <remarks>
    /// LevelManager khi đã load xong thì PHẢI gọi hàm này.
    /// </remarks>
    /// </summary>
    /// <param name="levelManager"></param>
    public void RegisterLevelManager(LevelManager levelManager)
    {
        LevelManager = levelManager;
        GameStateController.ChangeState(GameState.LOBBY);
        //uiManager.OpenLoading(false);
        IsLevelLoading = false;
    }

    /// <summary>
    /// Bắt đầu level, đưa game vào state <see cref="GameState.IN_GAME"/>
    /// </summary>
    public void StartLevel()
    {
        //Analytics.LogTapToPlay();
        GameStateController.ChangeState(GameState.IN_GAME);
    }

    /// <summary>
    /// Kết thúc game sau một khoảng thời gian
    /// </summary>
    /// <param name="result"></param>
    /// <param name="delayTime"></param>
    public void DelayedEndgame(LevelResult result, float delayTime = 1.5f)
    {
        StartCoroutine(DelayedEndgameCoroutine(result, delayTime));
    }

    private IEnumerator DelayedEndgameCoroutine(LevelResult result, float delayTime)
    {
        yield return Yielders.Get(delayTime);
        EndLevel(result);
    }

    /// <summary>
    /// Kết thúc game
    /// </summary>
    /// <param name="result"></param>
    public void EndLevel(LevelResult result)
    {
        GameStateController.ChangeState(GameState.END_GAME);
        Debug.Log("OnEnter EndLevel");

        if (result == LevelResult.Win)
        {
            IncreaseLevel();
            Debug.Log(dataLevel);
        }
    }

    /// <summary>
    /// Tăng level
    /// </summary>
    public void IncreaseLevel()
    {
        dataLevel.IncreaseLevel();
    }


    /// <summary>
    /// Hồi sinh
    /// </summary>
    public void Revive()
    {
        LevelManager.ResetLevelState();
        // TODO: Revive code
    }

    public void NextLevel()
    {
        LevelManager.Instance.ResetLevelState();
        LoadLevel();
        StartLevel();
        // TODO: Revive code
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GamePaused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameResumed?.Invoke();
    }

    public void AddGoldGame(int _coin)
    {
        Profile.AddGold(_coin);
    }

    public int GetGoldGame()
    {
        return Profile.GetGold();
    }

    private void Update()
    {
        if (!IsLevelLoading)
            GameStateController.Update();
    }

    private void FixedUpdate()
    {
        if (!IsLevelLoading)
            GameStateController.FixedUpdate();
    }

    private void LateUpdate()
    {
        if (!IsLevelLoading)
            GameStateController.LateUpdate();
    }
}
