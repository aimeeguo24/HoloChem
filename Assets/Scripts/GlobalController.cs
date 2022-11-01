using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;


public class GlobalController : MonoBehaviour


{
    public GameObject tableObject;
    public GameObject chemObjectsContainer;
    // public List<GameObject> handleObjects;
    public GameObject[] handleObjects;

    public List<GameObject> chemObjects;
    public List<GameObject> onTableObjects;


    public bool isPouring = false;
    public GameObject pouringObject;
    public GameObject targetObject;

    private Bounds tableBounds;

    //public Vector3 _spillPosition;

    void Awake()
    {
        GlobalValues.chemObjectsContainer = GameObject.Find("/MixedRealityPlayspace/ContainerCollection");
        GlobalValues.tableObject = GameObject.Find("/MixedRealityPlayspace/ObjectsContainer/Table/PBR_Table");
    }

    // Start is called before the first frame update
    void Start()
    {
        // chemObjectsContainer = GameObject.Find("/MixedRealityPlayspace/ObjectsContainer/ChemObjectsContainer");
        // tableObject = GameObject.Find("/MixedRealityPlayspace/ObjectsContainer/Table/PBR_Table");
        chemObjectsContainer = GlobalValues.chemObjectsContainer;
        tableObject = GlobalValues.tableObject;
        FindChemObjets();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateOnTableObjects();

        handleObjects = GameObject.FindGameObjectsWithTag("holding");

        isPouring = GlobalValues.isPouring;
        pouringObject = GlobalValues.pouringObject;
        targetObject = GlobalValues.targetObject;

        //_spillPosition = GlobalValues.spillPosition;

        UpdateTargetObject();


    }

    private void UpdateTargetObject()
    {
        //Debug.Log("GlobalValues.isPouring: " + GlobalValues.isPouring);
        if (GlobalValues.isPouring)
        {
            GameObject pouringObject = GlobalValues.pouringObject;

            foreach (GameObject obj in onTableObjects)
            {
                if (obj != GlobalValues.pouringObject)
                {
                    Vector3 targetMin = obj.GetComponent<Collider>().bounds.min;
                    Vector3 targetMax = obj.GetComponent<Collider>().bounds.max;

                    //Debug.Log("targetMin: " + targetMin + "; spillPosition: " + GlobalValues.spillPosition);

                    //if (Mathf.Abs(targetMin.x - GlobalValues.spillPosition.x) < 0.1f && Mathf.Abs(targetMin.z - GlobalValues.spillPosition.z) < 0.1f && (GlobalValues.spillPosition.y - targetMin.y) > 0.001f)
                    if (Mathf.Abs(targetMin.x - GlobalValues.spillPosition.x) < 0.12f && Mathf.Abs(targetMin.z - GlobalValues.spillPosition.z) < 0.12f)

                    {
                        //Debug.Log("(x, z): " + "(" + Mathf.Abs(targetMin.x - GlobalValues.spillPosition.x) + "," + Mathf.Abs(targetMin.z - GlobalValues.spillPosition.z) + ")");

                        GlobalValues.targetObject = obj;
                        //Debug.Log("targetObject: " + GlobalValues.targetObject.name);

                        FillTargetObject();

                    }


                }
            }

            //FillTargetObject();
        }
    }

    private void FillTargetObject()
    {
        GameObject targetGO = GlobalValues.targetObject;
        if (targetGO != null)
        {
            LiquidVolume lv = targetGO.transform.Find("LiquidVolume").gameObject.GetComponent<LiquidVolume>();
            lv.level += GlobalValues.spillAmount;
            if ((pouringObject.name == "lead_nitrate" && targetObject.name == "potassium_iodide") || (pouringObject.name == "potassium_iodide" && targetObject.name == "lead_nitrate"))  
            {
                lv.liquidColor1 = Color.yellow;
                lv.liquidColor2 = Color.white;
            }
            if ((pouringObject.name == "tap_water" && targetObject.name == "sulfuric_acid") || (pouringObject.name == "sulfuric_acid" && targetObject.name == "tap_water"))
            {
                lv.liquidColor1 = Color.white;
                lv.murkiness = 1;
                lv.liquidColor2 = Color.white;
                lv.smokeEnabled = true;
            }

            if (lv.level > 1.0f)
            {
                lv.level = 0.99f;
            }

            Debug.Log("targetObject: " + GlobalValues.targetObject.name + " lv.level: " + lv.level);
        }

    }

    private void FindChemObjets()
    {
        foreach (Transform child in chemObjectsContainer.transform)
        {
            chemObjects.Add(child.gameObject);
        }
    }

    private bool IsOnTable(GameObject go)
    {
        tableBounds = GetBounds(tableObject);
        Vector3 tableMax = tableBounds.max;
        Vector3 tableMin = tableBounds.min;
        // Debug.Log("tableMax: " + tableMax + " tableMin: " + tableMin);

        Bounds goBounds = GetBounds(go);
        Vector3 goMax = goBounds.max;
        Vector3 goMin = goBounds.min;
        // Debug.Log("goMax: " + goMax + " goMin: " + goMin);

        if (goMin.x < tableMax.x && goMin.x > tableMin.x && (goMin.z < tableMax.z && goMin.z > tableMin.z) && ((goMin.y - tableMax.y )> -0.01f && (goMin.y - tableMax.y) < 0.01f))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private Bounds GetBounds(GameObject go)
    {
        Bounds bounds = go.GetComponent<Collider>().bounds;
        return bounds;
    }

    private void UpdateOnTableObjects()
    {
        onTableObjects.Clear();
        foreach (GameObject go in chemObjects)
        {
            if (go.activeSelf)
            {
                if (IsOnTable(go))
                {
                    // go.tag="on_table";
                    onTableObjects.Add(go);
                }
            }
        }
    }

}
