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

    [SerializeField] private bool m_Test;
    public bool IsTest
    {
        get
        {
            return m_Test;
        }
    }

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

    #region PUBLIC_METHODS

    [ContextMenu("StartGameplay")]
    public void StartGameplay()
    {
        m_PointsCurrent = 0;
        UiManager.Instance.SetPoint(m_PointsCurrent);

        m_PeopleModels.Shuffle();

        try
        {
            for (int i = 0; i < m_Enemys.Count; i++)
            {
                m_Enemys[i].SetPeople(m_PeopleModels[i]);
                m_Enemys[i].SetSpeed();
            }
        }
        catch
        {
            StartGameplay();
        }

        IsGameplay = true;
    }

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
        UiManager.Instance.SetPoint(m_PointsCurrent);
        print("POINT CURRENT:: " + m_PointsCurrent);
    }

    public void SetHighscore()
    {
        int highscore = PlayerPrefs.GetInt(Constants.HIGHSCORE_PLAYERPREFS);

        if(m_PointsCurrent > highscore)
        {
            PlayerPrefs.SetInt(Constants.HIGHSCORE_PLAYERPREFS, m_PointsCurrent);
            UiManager.Instance.SetHighscore(m_PointsCurrent);
            print("New highscore:: " + m_PointsCurrent);
        }
    }

    #endregion
}