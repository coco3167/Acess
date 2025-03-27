using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject openOptions;
    
    [HideInInspector] public float gameSpeed = 1;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        OpenOptions();
    }

    public void ChangeGameSpeed(float value)
    {
        gameSpeed = value;
    }

    public void GoBackToGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);
        
        optionsUI.SetActive(false);
        openOptions.SetActive(true);
        Time.timeScale = gameSpeed;
    }

    public void OpenOptions()
    {
        optionsUI.SetActive(true);
        openOptions.SetActive(false);
        Time.timeScale = 0;
    }
}
