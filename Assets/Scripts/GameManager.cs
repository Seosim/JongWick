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
            Debug.LogError("�̹� ������ GameManager�� �����մϴ�.");
            return;
        }

        gameManager = GetComponent<GameManager>();
    }
}
