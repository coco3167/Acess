using FMODUnity;
using UnityEngine;

public class FMODEvent_Loader : MonoBehaviour
{
    [field: Header("Phase One")]
    [field: SerializeField] public EventReference dinoJump { get; private set; }
    [field: SerializeField] public EventReference dinoRun { get; private set; }
    [field: SerializeField] public EventReference obstacle { get; private set; }

    [field: Header("Phase Two")]
    [field: SerializeField] public EventReference cogMove { get; private set; }
    [field: SerializeField] public EventReference cogSelect { get; private set; }
    [field: SerializeField] public EventReference cogSuccess { get; private set; }
    [field: SerializeField] public EventReference dinoRunAstral { get; private set; }
    [field: SerializeField] public EventReference puzzleSuccess { get; private set; }
    [field: SerializeField] public EventReference dinoPicore { get; private set; }

    [field: Header("Phase Three")]
    [field: SerializeField] public EventReference flySingle { get; private set; }
    [field: SerializeField] public EventReference flyLoop { get; private set; }
    [field: SerializeField] public EventReference uiNext { get; private set; }

    [field: Header("Phase General")]
    [field: SerializeField] public EventReference uiMove { get; private set; }
    [field: SerializeField] public EventReference uiBack { get; private set; }
    [field: SerializeField] public EventReference uiConfirm { get; private set; }
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference mono { get; private set; }









    public static FMODEvent_Loader instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
