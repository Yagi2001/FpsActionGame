using UnityEngine;

public class GunMechanic : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private Camera _fpsCam;

    private void Update()
    {
        if (Input.GetButtonDown( "Fire1" ))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast( _fpsCam.transform.position, _fpsCam.transform.forward, out hitInfo, _range ))
        {
            Debug.Log( hitInfo.transform.name );
        }

    }
}
