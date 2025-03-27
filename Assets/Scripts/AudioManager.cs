using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    private List<string> loadedBanks = new List<string>();

    public enum Scene
    {
        Scene1 = 1,
        Scene2 = 2,
        Scene3 = 3,
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
                case Scene.Scene1:
                    LoadBank("PhaseOne");
                    UnloadAllBanksExcept("General");
                    InstantiateDinoRun(FMODEvent_Loader.instance.dinoRun);
                    break;
                case Scene.Scene2:
                    LoadBank("PhaseTwo");
                    UnloadAllBanksExcept("General");
                    break;
                case Scene.Scene3:
                    LoadBank("PhaseThree");
                    UnloadAllBanksExcept("General");
                    InstantiateChickenFly(FMODEvent_Loader.instance.flyLoop);

                    break;
                case Scene.End:
                    UnloadAllBanksExcept("General");
                    break;
            }
        }
    }
    public static Audio_Manager instance { get; private set; }

    public EventInstance dinoRun;
    public EventInstance chickenFly;
    public EventInstance musicInstance;
    public EventInstance monoInstance;


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
    }

    public void StopDinoRun()
    {
        if (dinoRun.isValid())
        {
            dinoRun.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            //dinoRun.release();
        }
    }
    public void InstantiateChickenFly(EventReference sound)
    {
        dinoRun = RuntimeManager.CreateInstance(sound);
    }
    public void PlayChickenFly()
    {
        dinoRun.start();
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
        CurrentScene = Scene.Scene1;
        InstantiateMusic(FMODEvent_Loader.instance.music);
    }

    

    // Update is called once per frame
    void Update()
    {

    }
    
    
}
