using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VARIABLES

    private int m_Speed;
    private Vector3 m_PositionInitial;
    private PeopleModel peopleModel;

    [SerializeField] private TextMeshProUGUI m_PlayerFakeNameText;

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Start()
    {
        m_PositionInitial = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * (Time.deltaTime * m_Speed));
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnBecameInvisible()
    {
        PoolSimpleSystem();
        SetSpeed();
    }

    private void PoolSimpleSystem()
    {
        transform.position = m_PositionInitial;
    }

    #endregion

    #region PUBLIC_METHODS

    public void SetSpeed()
    {
        m_Speed = Random.Range(5, 10);
    }

    public void SetPeople(PeopleModel people)
    {
        peopleModel = people;
        string playerFakeNameFull = $"{people.name}\n{people.surname}";
        m_PlayerFakeNameText.text = playerFakeNameFull;
    }

    public PeopleModel GetPeopleModel()
    {
        return peopleModel;
    }

    public void Reset()
    {
        PoolSimpleSystem();
        m_Speed = 0;
    }

    #endregion
}