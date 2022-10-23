using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Diagnostics;
using UnityEngine;
public class DialogTimerScript : MonoBehaviour
{
    /// <summary>
    /// This class is used as an example controller to show how to instantiate and launch two different kind of Dialog.
    /// Each one of the public methods are called by the buttons in the scene at the OnClick event.
    /// </summary>

    Stopwatch stopWatch = new Stopwatch();

    [SerializeField]
    [Tooltip("Assign DialogLarge_192x192.prefab")]
    private GameObject dialogPrefabMedium;

    ///private static Timer aTimer;
    /// <summary>
    /// Large Dialog example prefab to display
    /// </summary
    public GameObject DialogPrefabMedium
    {
        get => dialogPrefabMedium;
        set => dialogPrefabMedium = value;
    }
    /// <summary>
    /// Opens confirmation dialog example
    /// </summary>
    public void OpenConfirmationDialogMedium()
    {
        ///Color redColor = new Color(255, 0, 0, 1);
        ///MeshRenderer gameObjectRenderer = DialogPrefabMedium.GetComponent<MeshRenderer>();
        ///Material newMaterial = new Material(Shader.Find(""));
        ///newMaterial.color = redColor;
        ///gameObjectRenderer.material = newMaterial;
        Dialog.Open(DialogPrefabMedium, DialogButtonType.OK, "Please read the instructions carefully", "This is vital for the safety of the lab", true);
    }

    /// <summary>
    /// Opens choice dialog example
    /// </summary>

    // Specify what you want to happen when the Elapsed event is raised.
    public void StartTimer()
    {
        stopWatch.Start();
    }
    public void OpenChoiceDialogMedium()
    {
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        float elapsedTime = ts.Seconds;
        if (elapsedTime < 10)
        {
            OpenConfirmationDialogMedium();
        }
        
        //OpenConfirmationDialogLarge();
    }

    /// <summary>
    /// Opens confirmation dialog example
    /// </summary>


    private void OnClosedDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            UnityEngine.Debug.Log(obj.Result);
        }
    }
}

