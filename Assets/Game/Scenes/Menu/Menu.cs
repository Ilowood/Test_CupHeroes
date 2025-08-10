using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Untils;

public class Menu : MonoBehaviour
{
    [SerializeField] private RectTransform _saveArea;

    private void Awake()
    {
        // UI.SaveArea(_saveArea);
    }

    public void Game()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
