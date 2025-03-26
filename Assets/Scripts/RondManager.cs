using UnityEngine;
using UnityEngine.InputSystem;


public class RondManager : MonoBehaviour
{
    public Rond[] rondColl;
    public int ID;


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

        // if (Input.GetMouseButtonDown(1)) // 0 = Left Click
        // {
        //     ID += 1;
        //     if(ID > rondColl.Length - 1)
        //     {
        //         ID = 0;
        //     }
        // }

    }

    private void OnSwitch(InputValue inputValue)
    {
        if(inputValue.isPressed)
            ID = (ID + 1) % rondColl.Length;
    }

    private void OnInteract(InputValue inputValue)
    {
        if(inputValue.isPressed)
            rondColl[ID].Rotate();
    }

    
}
