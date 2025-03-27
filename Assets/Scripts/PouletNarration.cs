using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PouletNarration : MonoBehaviour
{
    [SerializeField] private List<Dialog> dialogs;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject background;
    
    private Animator m_animator;
    private int m_index;
    private int m_intensityIndex;
    private bool m_canInteract;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_index = 0;
        m_canInteract = false;
        text.text = dialogs[m_index].text;
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed && m_canInteract)
        {
            if(m_index >= dialogs.Count -1)
                Application.Quit();
            
            m_index++;
            UpdateIntensity();
            
            if(dialogs[m_index].shouldFly)
                FlyPoulet();
            else
                UpdateDialog();
        }
    }

    private void FlyPoulet()
    {
        m_canInteract = false;
        m_animator.SetTrigger("Fly");
        Audio_Manager.instance.PlayOneShot(FMODEvent_Loader.instance.flySingle, transform.position);
    }

    private void UpdateDialog()
    {
        background.SetActive(true);
        text.text = dialogs[m_index].text;
        Audio_Manager.instance.PlayOneShot(FMODEvent_Loader.instance.uiNext, transform.position);
    }

    private void UpdateIntensity()
    {
        m_intensityIndex = 1;
        if (m_index > 16)
            m_intensityIndex = 2;
        
        if (m_index > 41)
            m_intensityIndex = 3;
        
        Debug.Log(m_index);
    }

    private void HideDialog()
    {
        background.SetActive(false);
    }

    private void EnableInteract()
    {
        m_canInteract = true;
    }
    
    [Serializable]
    private struct Dialog
    {
        [SerializeField, TextArea] public string text;
        [SerializeField] public bool shouldFly;
    }
}
