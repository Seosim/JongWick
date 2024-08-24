using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public Inventory inventory;

    

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("�̹� ������ GameManager�� �����մϴ�.");
            return;
        }

        Instance = GetComponent<GameManager>();
    }
}
