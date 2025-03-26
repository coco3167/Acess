using UnityEngine;

public class Rond : MonoBehaviour
{
    public bool selected;
    public float speed = 2f;
    public float targetRot;
    public Rond[] influencedRond;
    private Vector3 startScale;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
        targetRot = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selected) // 0 = Left Click
        {
            foreach(Rond rond in influencedRond)
            {
                rond.targetRot += 45;
            }
            
        }

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
}
