using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBooleanPI : MonoBehaviour
{
    // Start is called before the first frame update
    public void onLookAt()
    {
        GlobalValues.isLookedAtPI = true;
    }
    public void onLookAway()
    {
        GlobalValues.isLookedAtPI = false;
    }
}
