using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerWeek6 : MonoBehaviour
{

    [SerializeField] private GameObject[] axes;
    [SerializeField] private GameObject canva;

    public void EndGame()
    {
        foreach (GameObject axe in axes)
        {
            axe.GetComponent<AxeAnimation>().speed = 0;
        }
        canva.SetActive(true);
        StartCoroutine(ResetLevel());
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
