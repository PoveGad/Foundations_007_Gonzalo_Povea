using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    
    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
    }

    public void WinButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
