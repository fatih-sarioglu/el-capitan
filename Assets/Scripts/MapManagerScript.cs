using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagerScript : MonoBehaviour
{
    public Transform cameraTransformForMapManager;

    [SerializeField]
    private GameObject mapMeshes;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraTransformForMapManager.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            if (other.transform.parent.gameObject.transform.GetSiblingIndex() > 1)
            {
                other.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(other.transform.parent.gameObject.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
                other.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(other.transform.parent.gameObject.transform.GetSiblingIndex() - 1).gameObject.SetActive(true);

                mapMeshes.transform.GetChild(other.transform.parent.gameObject.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
                mapMeshes.transform.GetChild(other.transform.parent.gameObject.transform.GetSiblingIndex() - 1).gameObject.SetActive(true);
            }
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(other.transform.parent.gameObject.transform.GetSiblingIndex() - 1).gameObject.SetActive(false);
        }
    }*/
}
