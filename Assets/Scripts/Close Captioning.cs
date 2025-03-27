using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CloseCaptioning : MonoBehaviour
{
    public string rotateCaption;
    public string validCaption;
    public string finishedCaption;
    public float captionLifeTime;
    public GameObject caption;
    public TextMeshProUGUI text;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (captionLifeTime > 0)
        {
            captionLifeTime -= Time.deltaTime;
        }
        else
        {
            caption.SetActive(false);
        }
    }

    public void setCaption(string captionText)
    {
        captionLifeTime = 2f;
        caption.SetActive(true);
        text.text = captionText;

    } 
}
