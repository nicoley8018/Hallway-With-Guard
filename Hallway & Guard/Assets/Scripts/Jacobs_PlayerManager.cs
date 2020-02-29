using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacobs_PlayerManager : MonoBehaviour
{
    #region Singleton

    public static Jacobs_PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
}
