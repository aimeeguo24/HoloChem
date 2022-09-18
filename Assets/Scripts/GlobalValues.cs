using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalValues
{
    public static bool isPouring = false;
    public static GameObject pouringObject;
    public static GameObject targetObject;

    public static Vector3 spillPosition;
    public static float spillAmount;

    public static List<GameObject> chemObjects;
    public static List<GameObject> onTableObjects;

    public static GameObject tableObject;
    public static GameObject chemObjectsContainer;
}
