using FMODUnity;
using UnityEngine;

public class FMODEvent_Loader : MonoBehaviour
{
    [field: SerializeField] public EventReference dinoJump { get; private set; }
    [field: SerializeField] public EventReference dinoRun { get; private set; }



    public static FMODEvent_Loader instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
