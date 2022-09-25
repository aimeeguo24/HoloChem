using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class AddDialogToInstr : MonoBehaviour
{
    [Tooltip("Euler angles by which the object should be rotated by.")]
    [SerializeField]
    private Vector3 RotateByEulerAngles = Vector3.zero;

    [Tooltip("Rotation speed factor.")]
    [SerializeField]
    private float speed = 1f;

    /// <summary>
    /// Rotate game object based on specified rotation speed and Euler angles.
    /// </summary>
    private GameObject dialogPrefabLarge;

    public GameObject DialogPrefabLarge
    {
        get => dialogPrefabLarge;
        set => dialogPrefabLarge = value;
    }
    public void SpawnToolTip()
    {
        Dialog.Open(DialogPrefabLarge, DialogButtonType.OK, "Confirmation Dialog, Large, Far", "This is an example of a large dialog with only one button, placed at far interaction range", false);
    }
}
