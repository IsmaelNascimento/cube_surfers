using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region ENUMS

    private enum MoveHorizontalOrietation
    {
        Left,
        Right
    }

    private enum MoveVerticalOrietation
    {
        Up,
        Down
    }

    #endregion

    #region VARIABLES

    private float m_DistanceBase = 1.2f;
    private Vector2 m_StartingTouch;

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        InputForPC();
#else
        InputForMobile();
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You lose for:: " + other.gameObject.GetComponent<Enemy>().GetPeopleModel().name);
        GameManager.Instance.IsGameplay = false;
        GameManager.Instance.ResetGameplay();
    }

#endregion

#region PRIVATE_METHODS

    private void InputForPC()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    private void InputForMobile()
    {
        if (Input.touchCount == 1)
        {
            Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;

            diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

            if (diff.magnitude > 0.01f)
            {
                if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                {
                    if (diff.y < 0)
                    {
                        MoveDown();
                    }
                    else
                    {
                        MoveUp();
                    }
                }
                else
                {
                    if (diff.x < 0)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            m_StartingTouch = Input.GetTouch(0).position;
        }
    }

#region MOVEMENTS

    private void MoveRight()
    {
        Vector3 newPosition = MoveHorizontal(MoveHorizontalOrietation.Right);

        if (IsLimitHorizontal(newPosition))
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, m_DistanceBase);
        }
    }

    private void MoveLeft()
    {
        Vector3 newPosition = MoveHorizontal(MoveHorizontalOrietation.Left);

        if (IsLimitHorizontal(newPosition))
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, m_DistanceBase);
        }
    }

    private void MoveUp()
    {
        Vector3 newPosition = MoveVertical(MoveVerticalOrietation.Up);

        if (IsLimitVertical(newPosition))
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, m_DistanceBase);
        }
    }

    private void MoveDown()
    {
        Vector3 newPosition = MoveVertical(MoveVerticalOrietation.Down);

        if (IsLimitVertical(newPosition))
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, m_DistanceBase);
        }
    }

#endregion

#region AUXS

    private Vector3 MoveHorizontal(MoveHorizontalOrietation moveHorizontalOrietation)
    {
        switch (moveHorizontalOrietation)
        {
            case MoveHorizontalOrietation.Left:
                return new Vector3(transform.position.x - m_DistanceBase, transform.position.y, transform.position.z);
            case MoveHorizontalOrietation.Right:
                return new Vector3(transform.position.x + m_DistanceBase, transform.position.y, transform.position.z);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 MoveVertical(MoveVerticalOrietation moveVerticalOrietation)
    {
        switch (moveVerticalOrietation)
        {
            case MoveVerticalOrietation.Up:
                return new Vector3(transform.position.x, transform.position.y + m_DistanceBase, transform.position.z);
            case MoveVerticalOrietation.Down:
                return new Vector3(transform.position.x, transform.position.y - m_DistanceBase, transform.position.z);
            default:
                return Vector3.zero;
        }
    }

    private bool IsLimitHorizontal(Vector3 position)
    {
        if (position.x > m_DistanceBase || position.x < -m_DistanceBase)
            return false;

        return true;
    }

    private bool IsLimitVertical(Vector3 position)
    {
        if (position.y > m_DistanceBase || position.y < -m_DistanceBase)
            return false;

        return true;
    }

#endregion

#endregion
}