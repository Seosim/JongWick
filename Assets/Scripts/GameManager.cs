using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject player;

    private void Awake()
    {
        if(gameManager != null)
        {
            Debug.LogError("이미 생성된 GameManager가 존재합니다.");
            return;
        }

        gameManager = GetComponent<GameManager>();
    }
}
