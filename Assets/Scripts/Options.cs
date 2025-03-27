using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public static Options Instance;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject openOptions;
    
    [HideInInspector] public float gameSpeed = 1;
    [HideInInspector] public bool highContrast = false;
    
    private void Awake()
    {
        Instance = this;
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

    public void HighContrast(bool value)
    {
        highContrast = value;
    }
}
