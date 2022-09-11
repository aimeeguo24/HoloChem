using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

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
        isPouring = lv.GetSpillPoint(out spillPosition);
        currentLevel = lv.level;
        if (isPouring)
        {
            if (lv.level > 0.0f)
            {
                pouringRenderer.SetActive(true);
                pouringRenderer.transform.position = spillPosition;
                lv.level -= pouringSpeed;
                pouredLevel += pouringSpeed;
                Debug.Log("isPouring: " + isPouring);
            }
            else
            {
                pouringRenderer.SetActive(false);
            }
        }
        else
        {
            pouringRenderer.SetActive(false);
        }
    }
}

