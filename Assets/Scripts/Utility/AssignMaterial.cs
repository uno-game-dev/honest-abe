using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssignMaterial : ScriptableWizard
{
    public Material theMaterial;

    public void OnWizardUpdate()
    {
        helpString = "Select Game Obects";
        isValid = (theMaterial != null);
    }

    public void OnWizardCreate()
    {
        var gos = Selection.gameObjects;
   
        foreach (var go in gos)
        {
            go.GetComponent<Renderer>().material = theMaterial;
        }
    }

    [MenuItem("Custom/Assign Material", false, 4)]
    static public void assignMaterial()
    {
        ScriptableWizard.DisplayWizard<AssignMaterial>("Assign Material", "Assign");
    }
}
