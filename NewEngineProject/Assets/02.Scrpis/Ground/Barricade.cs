using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barricade : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate;
    public List<GameObject> objectsToActivate;
    public List<TextMeshPro> textsToActivate;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        DeactivateObjects();
        ActivateObjects();
        ActivateTexts();
    }

    void DeactivateObjects()
    {
        if (objectsToDeactivate.Count > 0)
        {
            GameObject objToDeactivate = objectsToDeactivate[0];
            Debug.Log("1");
            objToDeactivate.SetActive(false);
            objectsToDeactivate.Remove(objToDeactivate);
        }
    }

    void ActivateObjects()
    {
        if (objectsToActivate.Count > 0)
        {
            GameObject objToActivate = objectsToActivate[0];
            objToActivate.SetActive(true);
            objectsToActivate.Remove(objToActivate);
        }
    }

    void ActivateTexts()
    {
        if (textsToActivate.Count > 0)
        {
            TextMeshPro textToActivate = textsToActivate[0];
            textToActivate.gameObject.SetActive(true);
            textsToActivate.Remove(textToActivate);
        }
    }
}
