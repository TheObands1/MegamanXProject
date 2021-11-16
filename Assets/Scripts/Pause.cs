using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public void Setup(bool pause)
    {
        gameObject.SetActive(pause);
    }
}
