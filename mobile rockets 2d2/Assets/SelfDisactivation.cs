﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisactivation : MonoBehaviour {
public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}