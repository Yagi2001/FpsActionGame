using UnityEngine;

public class GunMechanic : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _fireRate = 15f;
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private Camera _fpsCam;

    private float nextTimeToFire =0f;

    private void Update()
    {
        if (Input.GetButtonDown( "Fire1" ) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        _muzzleFlash.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast( _fpsCam.transform.position, _fpsCam.transform.forward, out hitInfo, _range ))
        {
            Debug.Log( hitInfo.transform.name );

            EnemyDamage enemyDamage = hitInfo.transform.GetComponent<EnemyDamage>();
            if (enemyDamage != null)
                enemyDamage.TakeDamage( _damage );
        }
        if (_hitEffect != null)                 //Need to find an effect for here
        {
            GameObject hitEffectGO =  Instantiate( _hitEffect, hitInfo.point, Quaternion.LookRotation( hitInfo.normal ) );
            Destroy( hitEffectGO, 2f );
        }

    }
}
