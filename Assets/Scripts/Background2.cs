using UnityEngine;

public class Background2 : MonoBehaviour
{
    public bool active;
    public Material mat;

    public float speed = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat.SetFloat("_dissolve", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        float dissolveValue = mat.GetFloat("_dissolve"); 
        
        
        if (active && dissolveValue < 1)
        {
            mat.SetFloat("_dissolve", dissolveValue + Time.deltaTime*speed);
        }

        if (!active && dissolveValue > -0.5)
        {
            mat.SetFloat("_dissolve", dissolveValue - Time.deltaTime*speed);
        }

        dissolveValue = mat.GetFloat("_dissolve");
        mat.SetFloat("_dissolve", Mathf.Clamp(dissolveValue, -0.5f, 1));
    }  
}
