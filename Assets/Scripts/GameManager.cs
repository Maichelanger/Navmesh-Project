using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static int difficulty = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int getDifficulty()
    {
        return difficulty;
    }

    public void EasyDifficulty()
    {
        difficulty = 0;
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
    }

    public void MediumDifficulty()
    {
        difficulty = 1;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void HardDifficulty()
    {
        difficulty = 2;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
