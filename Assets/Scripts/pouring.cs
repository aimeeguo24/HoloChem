using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;
using Microsoft.MixedReality.Toolkit.UI;

public class pouring : MonoBehaviour
{
    public Color origColor = Color.white;
    public float pouringSpeed = 0.001f;
    public float currentLevel = 0.0f;
    public float pouredLevel = 0.0f;
    public bool isPouring = false;
    private LiquidVolume lv;
    private GameObject pouringRenderer;
    private Vector3 spillPosition;
    private float spillAmount;

    private bool isInHold = false;

    private float numDialog = 0;
    [SerializeField]
    [Tooltip("Assign DialogLarge_192x192.prefab")]
    private GameObject dialogPrefabMedium;
    public GameObject DialogPrefabMedium
    {
        get => dialogPrefabMedium;
        set => dialogPrefabMedium = value;
    }

    public void OpenConfirmationDialogMedium()
    {
        Dialog.Open(DialogPrefabMedium, DialogButtonType.OK, "Please pay attention when pouring chemicals", "This is vital for the safety of the lab", true);
    }

    private void Awake()
    {
        // Get the cimponent "LiquidVolume" of the GameObject
        lv = gameObject.transform.Find("LiquidVolume").gameObject.GetComponent<LiquidVolume>();
        pouringRenderer = transform.Find("PouringRenderer").gameObject;
        // Change the color and level
        origColor = lv.liquidColor2;
        currentLevel = lv.level;

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInHold)
        {
            isPouring = lv.GetSpillPoint(out spillPosition, out spillAmount);
            currentLevel = lv.level;
            if (isPouring) //if isPouring == true
            {
                GlobalValues.isPouring = true;
                SetAsPouring();
                if(GlobalValues.isLookedAtLN==false && GlobalValues.isLookedAtPI == false && numDialog==0)
                {
                    OpenConfirmationDialogMedium();
                    numDialog++;
                }
                //if there is liquid (liquid > 0) we pour it
                if (lv.level > 0.0f)
                {
                    // GlobalValues.spillAmount = 0.0f;
                    pouringRenderer.SetActive(true);
                    pouringRenderer.transform.position = spillPosition;
                    GlobalValues.spillPosition = spillPosition;
                    // lv.level -= pouringSpeed;
                    // pouredLevel += pouringSpeed;
                    lv.level -= spillAmount;
                    pouredLevel += spillAmount;
                    GlobalValues.spillAmount = spillAmount;
                    // Debug.Log("isPouring: " + isPouring + " currentLevel: " + currentLevel + " pouredLevel: " + pouredLevel + " spillAmount: " + spillAmount);
                }
                else // if there is no liquid but isPouring is true, we don't pour
                {
                    pouringRenderer.SetActive(false);
                }
            }
            else // if isPouring == false, we also don't pour / we do nothing
            {
                pouringRenderer.SetActive(false);
            }
        }

    }

    public void SetInHold()
    {
        isInHold = true;
    }

    public void UnSetInHold()
    {
        isInHold = false;
    }



    public void SetAsPouring()
    {
        GlobalValues.pouringObject = gameObject;
    }

    public void UnSetAsPouring()
    {
        GlobalValues.pouringObject = null;
        GlobalValues.isPouring = false;
        UnSetInHold();
    }
}
