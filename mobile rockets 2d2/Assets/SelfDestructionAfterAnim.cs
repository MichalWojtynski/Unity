using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructionAfterAnim : MonoBehaviour {

	public void Destruct()
    {
        Destroy(gameObject);
    }
}
