using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{
    private List<string> loadedBanks = new List<string>();

    public enum Scene
    {
        PlatformerScene = 1,
        ProtoEnigmeScene = 2,
        NarrationScene = 3,
        End = 4
    }
    private Scene currentScene;
    public Scene CurrentScene
    {
        get => currentScene;
        set
        {
            currentScene = value;
            switch (currentScene)
            {
                case Scene.PlatformerScene:
                    LoadBank("PhaseOne");
                    UnloadAllBanksExcept("General");
                    InstantiateDinoRun(FMODEvent_Loader.instance.dinoRun);
                    Debug.Log("Loading first bank");

                    break;
                case Scene.ProtoEnigmeScene:
                    LoadBank("PhaseTwo");
                    UnloadAllBanksExcept("General");
                    FinishDinoRun();
                    Debug.Log("Loading second bank");

                    break;
                case Scene.NarrationScene:
                    LoadBank("PhaseThree");
                    UnloadAllBanksExcept("General");
                    SetMusicParameter("PhaseThree", 1);
                    FinishDinoRun();
                    Debug.Log("Set music parameter to 1");
                    break;
                case Scene.End:
                    UnloadAllBanksExcept("General");
                    FinishDinoRun();
                    break;
            }
        }
    }
    public static Audio_Manager instance { get; private set; }

    public EventInstance dinoRun;
    public EventInstance chickenFly;
    public EventInstance musicInstance;
    public EventInstance monoInstance;
    
    private bool m_dinoRunning;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void Stop(EventReference sound)
    {
        EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
    }

    public void LoadBank(string bankName)
    {
        try
        {
            FMODUnity.RuntimeManager.LoadBank(bankName, true);
            if (!loadedBanks.Contains(bankName))
            {
                loadedBanks.Add(bankName);
            }
        }
        catch (BankLoadException e)
        {
            RuntimeUtils.DebugLogException(e);
        }
        FMODUnity.RuntimeManager.WaitForAllSampleLoading();
    }

    public void UnloadAllBanksExcept(string bankName)
    {
        foreach (var bank in loadedBanks)
        {
            if (!bank.Contains(bankName))
            {
                FMODUnity.RuntimeManager.UnloadBank(bank);
            }
        }
        loadedBanks.RemoveAll(b => !b.Contains(bankName));
    }

    public void InstantiateDinoRun(EventReference sound)
    {
        dinoRun = RuntimeManager.CreateInstance(sound);
        //dinoRun.start();
    }
    public void PlayDinoRun()
    {
        dinoRun.start();
        m_dinoRunning = true;
    }

    public void PauseDinoRun()
    {
        if(dinoRun.isValid() && m_dinoRunning)
            dinoRun.setPaused(true);
    }

    public void ResumeDinoRun()
    {
        if(dinoRun.isValid() && m_dinoRunning)
            dinoRun.setPaused(false);
    }
    
    public void StopDinoRun()
    {
        if (dinoRun.isValid())
        {
            m_dinoRunning = false;
            dinoRun.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
    public void FinishDinoRun()
    {
        if (dinoRun.isValid())
        {
            StopDinoRun();
            dinoRun.release();
        }
    }
    public void InstantiateChickenFly(EventReference sound)
    {
        dinoRun = RuntimeManager.CreateInstance(sound);
    }
    public void PlayChickenFly()
    {
        chickenFly.start();
    }

    public void StopChickenFly()
    {
        if (chickenFly.isValid())
        {
            chickenFly.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //dinoRun.release();
        }
    }
    public void InstantiateMusic(EventReference music)
    {
        musicInstance = RuntimeManager.CreateInstance(music);
        musicInstance.start();
    }
    public void SetMusicParameter(string parameter, int value)
    {
        musicInstance.setParameterByName(parameter, value);
    }
    public void MakeMono()
    {
        Debug.Log("Switching to Mono");
        SetMonoParameter("Mono", 1);
    }
    public void MakeStereo()
    {
        Debug.Log("Switching to Stereo");
        SetMonoParameter("Mono", 0);
    }

    public void InstantiateMono(EventReference mono)
    {
        monoInstance = RuntimeManager.CreateInstance(mono);
        monoInstance.start();
    }

    public void SetMonoParameter(string parameter, int state)
    {
        if (monoInstance.isValid())
        {
            monoInstance.setParameterByName(parameter, state);
            Debug.Log($"Set {parameter} to {state} on valid monoInstance.");
        }
        else
        {
            InstantiateMono(FMODEvent_Loader.instance.mono);
            monoInstance.setParameterByName(parameter, state);
            Debug.Log($"Instantiated monoInstance and set {parameter} to {state}.");
        }
    }

    public void ToggleMonoStereo()
    {
        float value = 0;
        monoInstance.getParameterByName("Mono", out value);
        Debug.Log($"Current Mono value: {value}");
        if (value == 0)
        {
            MakeMono();
        }
        else
        {
            MakeStereo();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentScene = Scene.PlatformerScene;
        InstantiateMusic(FMODEvent_Loader.instance.music);
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnSceneChange(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        FinishDinoRun();
        CurrentScene = (Scene)SceneManager.GetActiveScene().buildIndex;
    }




    // Update is called once per frame
    void Update()
    {

    }
    
    
}
