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
            Debug.LogError("이미 생성된 GameManager가 존재합니다.");
            return;
        }

        Instance = GetComponent<GameManager>();
    }
}
