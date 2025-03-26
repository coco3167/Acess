using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

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
                    break;
                case Scene.End:
                    UnloadAllBanksExcept("General");
                    break;
            }
        }
    }
    public static Audio_Manager instance { get; private set; }

    public EventInstance dinoRun;

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
        dinoRun.start();
    }
    public void StopDinoRun()
    {
        if (dinoRun.isValid())
        {
            dinoRun.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            dinoRun.release();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
}
