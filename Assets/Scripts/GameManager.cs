using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerMovement PlayerMovement;
    [SerializeField]
    private GameObject _god;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private DialogueVoices _dialogueVoices; // Later on I can turn this into array for other wave talks
    private AudioSource _audioSource;
    private GameObject _gun;


    private void Start()
    {
        _gun = GameObject.Find( "Gun" );// There should be a better way to do it. But currently Im keeping it like this.
        PlayerMovement = _player.GetComponent<PlayerMovement>();
        _audioSource = _player.AddComponent<AudioSource>();
        StartCoroutine( TalkWithGod(_dialogueVoices.duration) );
    }

    private IEnumerator TalkWithGod(float dialogueTime)
    {
        _gun.SetActive( false );
        //yield return new WaitForSeconds( 1f );
        //Transform originalRotation = _player.transform;
        PlayerMovement.enabled = false;
        float elapsedTime = 0f;
        float rotationTime = 1f;
        /*Vector3 directionToGod = (_god.transform.position - _player.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation( directionToGod );
        Quaternion startRotation = _player.transform.rotation;
        while (elapsedTime < rotationTime)
        {
            _player.transform.rotation = Quaternion.Slerp( startRotation, targetRotation, elapsedTime / rotationTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }*/

        StartCoroutine( PlayAudioClips() );
        //_player.transform.rotation = targetRotation;
        yield return new WaitForSeconds( dialogueTime );
        //_player.transform.rotation = originalRotation.rotation;
        PlayerMovement.enabled = true;
        _gun.SetActive( true );
    }

    private IEnumerator PlayAudioClips()
    {
        foreach (var clip in _dialogueVoices.audioClips)
        {
            _audioSource.clip = clip; 
            _audioSource.Play();
            
            while (_audioSource.isPlaying)
            {
                yield return null; 
            }

        }
    }
}
