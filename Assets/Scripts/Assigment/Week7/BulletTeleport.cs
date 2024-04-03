using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTeleport : MonoBehaviour
{
    public float delay = 5f; 
    public float radius = 5f; 
    public float teleportHeight = 4f;
    public GameObject Effect;
    public AudioSource Sound;

    void Start()
    {
        StartCoroutine(ActivationDelay());
    }

    IEnumerator ActivationDelay()
    {
        
        yield return new WaitForSeconds(delay);
        ActivateEffect();
    }

    void ActivateEffect()
    {
       
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, radius);
        Sound.Play();
        Instantiate(Effect, transform.position, transform.rotation);

        foreach (var objCollider in objectsInRadius)
        {
            Rigidbody rb = objCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 directionToCenter = (transform.position - rb.transform.position).normalized;
                rb.AddForce(directionToCenter * 10); 
                rb.transform.position += Vector3.up * teleportHeight;
                
            }
        }

        
        Destroy(gameObject);
    }
}
