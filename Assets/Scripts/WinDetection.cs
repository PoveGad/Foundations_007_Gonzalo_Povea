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

    void Update()
    {
        // Verifica si se ha presionado la tecla espacio
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Llama a la función que quieres ejecutar
            WinButton();
        }
    }

    public void WinButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
