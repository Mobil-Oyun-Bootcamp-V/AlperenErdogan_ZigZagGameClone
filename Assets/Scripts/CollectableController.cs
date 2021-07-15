using System;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
