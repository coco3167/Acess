using UnityEngine;

public class RondManager : MonoBehaviour
{
    public Rond[] rondColl;
    public int ID;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentPos = 0;
        foreach(Rond rond in rondColl)
        {
            if(currentPos == ID)
            {
                rond.selected = true;
            }
            else
            {
                rond.selected = false;
            }
            currentPos += 1;
        }

        if (Input.GetMouseButtonDown(1)) // 0 = Left Click
        {
            ID += 1;
            if(ID > rondColl.Length - 1)
            {
                ID = 0;
            }
        }

    }
}
