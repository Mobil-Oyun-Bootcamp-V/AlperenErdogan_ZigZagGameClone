using System;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    //Destroy diamond
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
