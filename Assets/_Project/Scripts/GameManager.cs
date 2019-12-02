using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region VARIABLES

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new GameObject("[GAME_MANAGER_CREATED]").AddComponent<GameManager>();

            return m_Instance;
        }
    }

    private List<PeopleModel> m_PeopleModels = new List<PeopleModel>();

    [SerializeField] private Transform m_Player;
    [SerializeField] private List<Enemy> m_Enemys;

    private int m_PointsCurrent;

    public bool IsGameplay { get; set; }

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        ApiManager.Instance.GetPeoples(peoples =>
        {
            m_PeopleModels = peoples;
            StartGameplay();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGameplay();
        }
    }

    #endregion

    #region PRIVATE_METHODS

    [ContextMenu("StartGameplay")]
    private void StartGameplay()
    {
        for (int i = 0; i < m_Enemys.Count; i++)
        {
            m_Enemys[i].SetPeople(m_PeopleModels[i]);
            m_Enemys[i].SetSpeed();
        }

        IsGameplay = true;
    }

    #endregion

    #region PUBLIC_METHODS

    public void ResetGameplay()
    {
        for (int i = 0; i < m_Enemys.Count; i++)
        {
            m_Enemys[i].Reset();
            m_Player.position = Vector3.zero;
        }
    }

    public void SetPoint()
    {
        m_PointsCurrent += Constants.INCREASE_POINT_COUNT;
    }

    public void SetHighscore()
    {
        int highscore = PlayerPrefs.GetInt(Constants.HIGHSCORE_PLAYERPREFS);

        if(m_PointsCurrent > highscore)
        {
            PlayerPrefs.SetInt(Constants.HIGHSCORE_PLAYERPREFS, m_PointsCurrent);
        }
    }

    #endregion
}