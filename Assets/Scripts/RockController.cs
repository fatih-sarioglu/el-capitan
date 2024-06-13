using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    [SerializeField]
    private Image exclamationMarkPrefab;

    private Image exclamationMarkObject;

    void Start()
    {
        exclamationMarkObject = Instantiate(exclamationMarkPrefab, FindObjectOfType<Canvas>().transform);
        exclamationMarkObject.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        transform.Rotate(new Vector3(45f, 45f, 45f) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 1.17f, transform.position.z);

        if (exclamationMarkObject.gameObject != null && exclamationMarkObject.GetComponent<Image>().enabled == true)
        {
            //Vector3 myDifferentPos = new Vector3(transform.position.x, transform.position.z, transform.position.y);

            //Vector3 excMarkScreenPos = Camera.main.WorldToScreenPoint(myDifferentPos);
            exclamationMarkObject.gameObject.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, 
                exclamationMarkObject.transform.position.y, exclamationMarkObject.transform.position.z);

            //Debug.Log(excMarkScreenPos.x);
            //exclamationMarkObject.gameObject.transform.position = new Vector3(excMarkScreenPos.x, exclamationMarkObject.gameObject.transform.position.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            exclamationMarkObject.GetComponent<Image>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            exclamationMarkObject.GetComponent<Image>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            Destroy(this.gameObject);
            Destroy(exclamationMarkObject.gameObject);
        }
        else if (collision.gameObject.tag == "Character")
        {
            Destroy(exclamationMarkObject.gameObject);
        }
    }
}
