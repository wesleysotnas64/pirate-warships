using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool Active { get; set; }

    private void Start() {
        Active = false;
    }
}
