using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartLogic : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        switch (gameManager.getDifficulty())
        {
            case 0:
                for (int i = 1; i < enemies.Length; i++)
                {
                    Destroy(enemies[i]);
                }
                break;
            case 1:
                for (int i = 3; i < enemies.Length; i++)
                {
                    Destroy(enemies[i]);
                }
                break;
            case 2:
                break;
        }
    }
}
