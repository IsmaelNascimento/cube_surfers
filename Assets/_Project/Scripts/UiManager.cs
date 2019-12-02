using System.Collections.Generic;
using IsmaelNascimento;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region VARIABLES

    private static UiManager m_Instance;
    public static UiManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new GameObject("[UI_MANAGER_CREATED]").AddComponent<UiManager>();

            return m_Instance;
        }
    }

    [SerializeField] private List<GameObject> m_Screens;
    [Header("BUTTONS - MENU SCREEN")]
    [SerializeField] private Button m_PlayGameButton;
    [SerializeField] private Button m_QuitGameButton;
    [Header("BUTTONS - GAMEOVER SCREEN")]
    [SerializeField] private Button m_RestartGameButton;
    [SerializeField] private Button m_ShareGameButton;
    [SerializeField] private Button m_MenuGameButton;
    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI m_HighscoreText;
    [SerializeField] private TextMeshProUGUI m_PointsText;

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        m_PlayGameButton.onClick.AddListener(OnButtonPlayGameClicked);
        m_QuitGameButton.onClick.AddListener(OnButtonPlayGameClicked);
        m_RestartGameButton.onClick.AddListener(OnButtonRestartGameClicked);
        m_ShareGameButton.onClick.AddListener(OnButtonShareGameClicked);
        m_MenuGameButton.onClick.AddListener(OnButtonMenuGameClicked);
    }

    #endregion

    #region BUTTON_METHODS

    private void OnButtonPlayGameClicked()
    {
        ChangeScreen(1);
    }

    private void OnButtonQuitGameClicked()
    {
        Application.Quit();
    }

    private void OnButtonRestartGameClicked()
    {
        ChangeScreen(1);
        GameManager.Instance.ResetGameplay();
        GameManager.Instance.StartGameplay();
    }

    private void OnButtonShareGameClicked()
    {
        ScreenshotManager.Instance.OnButtonScreenshotClicked(ScreenshotManager.Instance.OnButtonShareScreenshotClicked);
    }

    private void OnButtonMenuGameClicked()
    {
        ChangeScreen(0);
        m_HighscoreText.text = string.Empty;
        m_PointsText.text = string.Empty;
    }

    #endregion

    #region AUXS

    public void ChangeScreen(int screenForActive)
    {
        m_Screens.ForEach(screen => screen.SetActive(false));
        m_Screens[screenForActive].SetActive(true);
    }

    public void SetPoint(int point)
    {
        m_PointsText.text = point.ToString();
    }

    #endregion
}