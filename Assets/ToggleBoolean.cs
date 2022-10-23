using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBoolean : MonoBehaviour
{
    // Start is called before the first frame update
    public void onLookAt()
    {
        GlobalValues.isLookedAtLN = true;
    }
    public void onLookAway()
    {
        GlobalValues.isLookedAtLN = false;
    }
}
