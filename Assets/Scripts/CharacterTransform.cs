using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTransform : MonoBehaviour
{
    [SerializeField]
    private Transform character;

    public float smoothness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = character.transform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
