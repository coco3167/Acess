using UnityEngine;
using UnityEngine.InputSystem;

public class Rond : MonoBehaviour
{
    public bool selected;
    public float speed = 2f;
    public float targetRot;
    public Rond[] influencedRond;
    private Vector3 startScale;
    private bool m_isRotating;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
        targetRot = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, 0, targetRot),Time.deltaTime*5);

        if(selected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,startScale*1.3f,Time.deltaTime*5);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale,startScale,Time.deltaTime*5);
        }
    }

    public void Rotate()
    {
        foreach(Rond rond in influencedRond)
        {
            rond.targetRot += 45;
        }
    }
}
