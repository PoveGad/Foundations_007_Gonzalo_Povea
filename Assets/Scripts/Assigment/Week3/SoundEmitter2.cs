using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter2 : MonoBehaviour
{
    [SerializeField] private float _soundRadius = 5f;
    [SerializeField] private float _impulseThreshold=2f;
    private AudioSource _audioSource;

    private float CloseRobot=100f;
    private EnemyController _enemyController;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.impulse.magnitude > _impulseThreshold || other.gameObject.CompareTag("Player"))
        {
            
            _audioSource.Play();
            Collider[] _colliders = Physics.OverlapSphere(transform.position, _soundRadius);
            foreach (var col in _colliders)
            {
                if (col.TryGetComponent(out EnemyController enemyController))
                {
                    if(enemyController._state == EnemyController.EnemyState.GettingHelp ||enemyController._state == EnemyController.EnemyState.Investigate) return;
                    float disToTargetHead = Vector3.Distance(transform.position, col.gameObject.transform.position);
                     
                    if (disToTargetHead < CloseRobot)
                    {
                        CloseRobot = disToTargetHead;
                        _enemyController = enemyController;

                    }
                }
            }
            _enemyController.CheckForHelp(transform);
            CloseRobot = 100f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,_soundRadius);
    }
}
