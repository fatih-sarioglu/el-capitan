using UnityEngine;

public class MapManagerDownScript : MonoBehaviour
{
    [SerializeField]
    private Transform cameraForPos;

    [SerializeField]
    private GameObject mapMeshes;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraForPos.position.z - 80);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NotStabbableSurface")
        {
            other.gameObject.transform.parent.gameObject.SetActive(false);
            mapMeshes.transform.GetChild(other.gameObject.transform.parent.gameObject.transform.GetSiblingIndex()).gameObject.SetActive(false);
        }
    }
}
