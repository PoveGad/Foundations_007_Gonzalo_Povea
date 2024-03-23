using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectionAssigment : MonoBehaviour
{
    [SerializeField] private TinCanToss manager;
    [SerializeField] private GameObject Effects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Can"))
        {
            manager.CanDetected();
            DeleteAnimation(other);
            
        }

        if (other.gameObject.CompareTag("Grabbable"))
        {
            StartCoroutine(BallDetected());
            DeleteAnimation(other);
        }
    }

    private void DeleteAnimation(Collider other)
    {
        Instantiate(Effects, other.gameObject.transform.position, other.gameObject.transform.rotation);
        Destroy(other.gameObject);
    }

    IEnumerator BallDetected()
    {
        yield return new WaitForSeconds(3); 
        manager.BallDetected();
    }
}
