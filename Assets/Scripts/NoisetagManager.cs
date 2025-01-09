using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisetagManager : MonoBehaviour
{
    void Awake()
    {
        if (GlobalReferences.Instance != null && !GlobalReferences.Instance.BCI)
        {
            gameObject.SetActive(false);
            return;

        }
    }
}
