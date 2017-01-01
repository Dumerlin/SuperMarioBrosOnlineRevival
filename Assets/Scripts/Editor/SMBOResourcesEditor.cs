using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SMBOResourcesEditor
{
    [MenuItem("SMBO/Open Design Doc")]
    public static void OpenDesignDoc()
    {
        Application.OpenURL("https://docs.google.com/document/d/1tyGA_aW-nBEJDy7ZwRpncU4XDI4A9s6xDVacchs9-Eo");
    }
}
