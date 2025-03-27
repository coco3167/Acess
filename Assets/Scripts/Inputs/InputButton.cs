using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Inputs
{
    public class InputButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText, descriptionText;
        [SerializeField] private RawImage image;
        
        private InputAction m_action;

        public void Init(InputAction inputAction)
        {
            buttonText.text = inputAction.GetBindingDisplayString();
            descriptionText.text = inputAction.name;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate {RemapButton(inputAction);});
            image.enabled = false;
            
            inputAction.Enable();

            Debug.Log("Init");
        }
        
        private void RemapButton(InputAction action)
        {
            action.Disable();
            m_action = action;
                
            image.enabled = true;
            
            InputActionRebindingExtensions.RebindingOperation rebindOperation =
                new InputActionRebindingExtensions.RebindingOperation()
                    .OnMatchWaitForAnother(0.1f)
                    .OnApplyBinding(OnApplyBinding)
                    .WithControlsExcluding("Mouse")
                    .OnComplete(OnRebindingComplete);
            rebindOperation.Start();
        }

        private void OnApplyBinding(InputActionRebindingExtensions.RebindingOperation arg1, string arg2)
        {
            m_action.ApplyBindingOverride(arg2);
        }

        private void OnRebindingComplete(InputActionRebindingExtensions.RebindingOperation rebindingOperation)
        {
            Init(m_action);
            rebindingOperation.Dispose();
        }
    }
}
