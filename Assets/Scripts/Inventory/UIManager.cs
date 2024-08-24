using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_UI;

    private void Start()
    {
        PlayerInputController inputController = GameManager.Instance.player.GetComponent<PlayerInputController>();
        inputController.aTabKeyDown += ShowUI;

        foreach (GameObject ui in m_UI)
        {
            ui.SetActive(false);
        }
    }
    private void ShowUI()
    {
        foreach (GameObject ui in m_UI) 
        {
            ui.SetActive(!ui.activeSelf);
        }
    }
}
