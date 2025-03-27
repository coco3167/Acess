using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputRebinder : MonoBehaviour
    {
        [SerializeField] private GameObject prefabInputButton;
        [SerializeField] private InputActionAsset inputActionAsset;
        
        private List<InputButton> m_inputButtons = new List<InputButton>();

        private void Awake()
        {
            foreach (InputAction inputAction in inputActionAsset.FindActionMap("Player"))
            {
                InputButton inputButton = Instantiate(prefabInputButton, transform).GetComponent<InputButton>();
                m_inputButtons.Add(inputButton);
                inputButton.Init(inputAction);
            }
        }
    }
}