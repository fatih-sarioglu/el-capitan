using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotionController : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Random.Range(-11.2f, 11.2f), 0.41f, transform.position.z);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180f) * Time.deltaTime);
    }
}
