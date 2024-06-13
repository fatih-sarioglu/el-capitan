using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedRockController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(this.gameObject);
        }
    }
}
