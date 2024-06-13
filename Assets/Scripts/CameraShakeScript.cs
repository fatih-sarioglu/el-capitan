using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        camAnim.SetTrigger("shake");
    }
}
