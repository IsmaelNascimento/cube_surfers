using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

public class ApiManager : MonoBehaviour
{
    #region VARIABLES

    private static ApiManager m_Instance;
    public static ApiManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new GameObject("[API_MANAGER_CREATED]").AddComponent<ApiManager>();

            return m_Instance;
        }
    }

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        m_Instance = this;
    }

    #endregion

    #region PUBLIC_METHODS

    public void GetPeoples(Action<List<PeopleModel>> onSuccess, Action<string> onError = null)
    {
        RestClient.Get(Constants.ENDPOINT_GET_PEOPLE).Then(response =>
        {
            Debug.Log("response:: " + response.Text);
            onSuccess?.Invoke(JsonConvert.DeserializeObject<List<PeopleModel>>(response.Text));
        });
    }

    #endregion
}