using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> windows;
   [SerializeField] private TMP_Text _dominantHandText;

   private void Awake()
   {
       Handed handedness = (Handed)PlayerPrefs.GetInt("handedness");
       if (handedness == Handed.Left)
       {
           _dominantHandText.text = "Left";
       }
       else
       {
           _dominantHandText.text = "Right";
       }
   }

   public void OpenWindow(int index)
    {
        CloseAllWindos();
        windows[index].SetActive(true);
    }

    public void CloseMenu()
    {
        CloseAllWindos();
        Invoke(nameof(Deactivate), 0.025f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void CloseAllWindos()
    {
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
    }

    
}
