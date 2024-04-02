using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotExplotion : MonoBehaviour
{
    
    [SerializeField] private GameObject RobotParts;
    [SerializeField] private Transform RobotPosition;
    
    

    public void ExploteRobot()
    {
        
        gameObject.SetActive(false);
        ExploteIteraction();
    }

    private void ExploteIteraction()
    {
        
        GameObject robot = Instantiate(RobotParts, RobotPosition.position, Quaternion.identity);
        foreach (Transform part in robot.transform)
        {
            Rigidbody rb = part.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                rb.AddExplosionForce(1000f, part.position + Random.insideUnitSphere * 5f, 10f);
                Debug.Log("Exploto");
            }

            
            Destroy(part.gameObject, 3f);
        }
    }
}
