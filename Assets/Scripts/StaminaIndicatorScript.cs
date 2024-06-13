using UnityEngine;
using UnityEngine.UI;

public class StaminaIndicatorScript : MonoBehaviour
{
    [SerializeField]
    private Transform character;

    [SerializeField]
    private Image staminaIndicatorArrow;

    public float smoothness;

    void Update()
    {
        if (FindClosestStamina() != null)
        {
            Vector3 desiredPosition = character.transform.position + new Vector3(0, 0, 3f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
            transform.position = smoothedPosition;

            var direction = FindClosestStamina().transform.position - transform.position;
            var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            var eulerAngles = staminaIndicatorArrow.transform.eulerAngles;
            staminaIndicatorArrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, angle - 90);

            if (direction.magnitude <= 7f || direction.magnitude > 25f)
            {
                staminaIndicatorArrow.gameObject.SetActive(false);
            }
            else if(direction.magnitude > 7f || direction.magnitude <= 25f)
            {
                staminaIndicatorArrow.gameObject.SetActive(true);
            }
        }
    }

    public GameObject FindClosestStamina()
    {
        GameObject[] activeStaminaPotions;
        activeStaminaPotions = GameObject.FindGameObjectsWithTag("StaminaPotionParent");

        GameObject closestStamina = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject sP in activeStaminaPotions)
        {
            Vector3 difference = sP.transform.position - position;
            float currentDistance = difference.sqrMagnitude;

            if (currentDistance < distance)
            {
                closestStamina = sP;
                distance = currentDistance;
            }
        }

        return closestStamina;
    }
}
