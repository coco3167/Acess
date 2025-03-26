using UnityEngine;
using UnityEngine.InputSystem;


public class RondManager : MonoBehaviour
{
    public Rond[] rondColl;
    public int ID;
    public bool win;


    // Update is called once per frame
    void Update()
    {
        int index = 0;
        int validCount = 0;

        foreach(Rond rond in rondColl)
        {
            if(index == ID)
            {
                rond.selected = true;
            }
            else
            {
                rond.selected = false;
            }
            index += 1;

            if (rond.validRotation)
            {
                validCount += 1;
            }
        }

        if (validCount >= rondColl.Length)
        {
            win = true;
        }
        else
        {
            win = false;
        }
        



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
