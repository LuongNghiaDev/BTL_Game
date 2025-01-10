using Sirenix.OdinInspector;
using UnityEngine;

public partial class LobbyManager : SerializedMonoBehaviour
{

    private static LobbyManager instance;

    [SerializeField] private new Camera camera;

    public static LobbyManager Ins { get => instance; set => instance = value; }

    [Space]
    [BoxGroup("Level")]
    [SerializeField] private LevelConstraint levelConstraint;

    private IDataLevel dataLevel;
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Profile = new Profile();

        /*dataLevel = PlayerDataManager.GetDataLevel(levelConstraint);
        dataLevel.LevelConstraint = levelConstraint;*/
        Debug.Log(dataLevel);
    }

    private void Start()
    {
        //AddGoldGame(200);
    }

    public void AddGoldGame(int goldBonus)
    {
        int _count = GetGoldGame() + goldBonus;
        PlayerDataManager.Instance.SetGold(_count);
    }

    public void ReloadGoldGame()
    {
        PlayerDataManager.Instance.SetGold(8000);
    }

    public int GetGoldGame()
    {
        return PlayerDataManager.Instance.GetGold();
    }

    public void DeductGoldGame(int goldBonus)
    {
        int _count = 0;
        if (GetGoldGame() >= 0)
        {
            _count = GetGoldGame() - goldBonus;
        }
        PlayerDataManager.Instance.SetGold(_count);
    }

}
