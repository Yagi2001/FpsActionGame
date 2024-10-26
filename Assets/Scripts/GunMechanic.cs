using UnityEngine;
using System.Collections;

public class GunMechanic : MonoBehaviour
{
    [Header( "Weapon Settings" )]
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _fireRate = 15f;
    [SerializeField]
    private int _maxAmmo = 10;
    private int _currentAmmo;
    [SerializeField]
    private float _reloadTime = 1f;
    private bool _isReloading = false;


    [Header( "Visual Effects" )]
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private GameObject _hitEffect;

    [Header( "Sound Effects" )]
    [SerializeField]
    private AudioSource _fireSound;
    [SerializeField]
    private AudioSource _reloadSound;

    [Header("References")]
    [SerializeField]
    private Camera _fpsCam;
    [SerializeField]
    private Animator _anim;

    private float nextTimeToFire =0f;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if (_isReloading)
            return;
        if (_currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButtonDown( "Fire1" ) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        _fireSound.Play();
        _anim.SetTrigger( "hitTrigger" );
        _currentAmmo--;
        _muzzleFlash.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast( _fpsCam.transform.position, _fpsCam.transform.forward, out hitInfo, _range ))
        {
            EnemyDamage enemyDamage = hitInfo.transform.GetComponent<EnemyDamage>();
            if (enemyDamage != null)
                enemyDamage.TakeDamage( _damage,hitInfo.point );
        }
        if (_hitEffect != null)                 //Need to find an effect for here
        {
            GameObject hitEffectGO =  Instantiate( _hitEffect, hitInfo.point, Quaternion.LookRotation( hitInfo.normal ) );
            Destroy( hitEffectGO, 2f );
        }

    }

    private IEnumerator Reload()
    {
        _reloadSound.Play();
        _isReloading = true;
        Debug.Log( "Reloading..." );
        _anim.SetBool( "isReloading", true );
        yield return new WaitForSeconds( _reloadTime );
        _anim.SetBool( "isReloading", false );
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }
}
