using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup(bool active)
    {
        gameObject.SetActive(active);
    }
}
