using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class filling : MonoBehaviour
{
    public Color origColor = Color.white;
    public Color targetColor = Color.white;
    public float fillingSpeed = 0.001f;
    public float initialLevel = 0.0f;
    public float filledLevel = 0.0f;
    public bool isFilling = false;

    private LiquidVolume lv;
    private GameObject pouringRenderer;
    private Rigidbody rb;
    private Vector3 spillPosition;





    private void Awake()
    {
        // Get the cimponent "LiquidVolume" of the GameObject
        lv = gameObject.transform.Find("LiquidVolume").gameObject.GetComponent<LiquidVolume>();
        //pouringRenderer = transform.Find("PouringRenderer").gameObject;
        rb = gameObject.GetComponent<Rigidbody>();

        // Change the color and level
        origColor = lv.liquidColor2;
        initialLevel = lv.level;
    }


    // Start is called before the first frame update
    void Start()
    {
        //UpdateColliderPos();
    }

    // Update is called once per frame
    void Update()
    {
        isFilling = !lv.GetSpillPoint(out spillPosition);
        if (isFilling)
        {
            pouringRenderer.SetActive(true);
            if (lv.level > 0.0f)
            {
                lv.level += fillingSpeed;
                Debug.Log("isFilling: " + isFilling);
            }
            else{
                pouringRenderer.SetActive(false);
            }
        }
        else{
            pouringRenderer.SetActive(false);
        }
    }




    void OnParticleCollision(GameObject other)
    {
        //UpdateColliderPos();
    }

    //void UpdateColliderPos()
    //{
    //    Vector3 pos = new Vector3(transform.position.x, lv.liquidSurfaceYPosition - transform.localScale.y * 0.5f - sinkFactor, transform.position.z);
    //    rb.position = pos;
    //    if (lv.level >= 1f)
    //    {
    //        transform.localRotation = Quaternion.Euler(Random.value * 30 - 15, Random.value * 30 - 15, Random.value * 30 - 15);
    //    }
    //    else
    //    {
    //        transform.localRotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}


}
