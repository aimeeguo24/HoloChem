using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class pouring : MonoBehaviour
{
    public Color origColor = Color.white;
    public Color targetColor = Color.yellow;
    public float fillSpeed = 0.001f;
    public float sinkFactor = 0.001f;
    public float initialLevel = 0.1f;
    public float filledVol = 0.0f;

    private LiquidVolume lv;
    private GameObject spot;
    private Rigidbody rb;





    private void Awake()
    {
        // Get the cimponent "LiquidVolume" of the GameObject
        lv = gameObject.transform.Find("LiquidVolume").gameObject.GetComponent<LiquidVolume>();
        spot = transform.Find("FillingRenderer").gameObject;
        rb = transform.Find("LiquidSurfaceCollider").gameObject.GetComponent<Rigidbody>();

        // Change the color and level
        lv.liquidColor2 = origColor;
        lv.level = initialLevel;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateColliderPos();
    }

    // Update is called once per frame
    void Update()
    {
        Transform bottle_trans = transform.transform;
        Transform spot_trans = spot.transform;
        if ((Mathf.Abs(bottle_trans.position.x - spot_trans.position.x) < 0.01f) && (Mathf.Abs(bottle_trans.position.z - spot_trans.position.z) < 0.01f))
        {
            if (lv.level < 0.8f)
            {
                lv.level += fillSpeed;
                filledVol += fillSpeed;
                lv.liquidColor2 = Color.Lerp(origColor, targetColor, filledVol);
            }
        }

    }


    void OnParticleCollision(GameObject other)
    {
        UpdateColliderPos();
    }

    void UpdateColliderPos()
    {
        Vector3 pos = new Vector3(transform.position.x, lv.liquidSurfaceYPosition - transform.localScale.y * 0.5f - sinkFactor, transform.position.z);
        rb.position = pos;
        if (lv.level >= 1f)
        {
            transform.localRotation = Quaternion.Euler(Random.value * 30 - 15, Random.value * 30 - 15, Random.value * 30 - 15);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }


}

