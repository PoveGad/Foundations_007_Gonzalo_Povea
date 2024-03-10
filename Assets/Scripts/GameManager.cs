using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _succesPanel;
    [SerializeField] private float _canvasFadeLine = 2f;

    [Header("Audio")] 
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _caughtMusic;
    [SerializeField] private AudioClip _successMusic;
    
    private PlayerInput _playerInput;
    private bool _isFadingIn = false;
    private float _fadeLevel = 0;
    private FirstPersonController _fpController;
    private bool _isGoalReached = false;
    
    void Start()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.onInvestigate.AddListener(EnemyInvestigating);
            enemy.onPlayerFound.AddListener(PLayerFound);
            enemy.onReturnToPatrol.AddListener(EnemyReturnToPatrol);
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player)
        {
            _playerInput = player.GetComponent<PlayerInput>();
            _fpController = player.GetComponent<FirstPersonController>();
        }
        else
        {
            Debug.Log("There is no player");
        }

        _canvasGroup.alpha = 0;
        _failedPanel.SetActive(false);
        _succesPanel.SetActive(false);

    }

    public void GoalRechead()
    {
        
        _isFadingIn = true;
        _isGoalReached = true;
        _succesPanel.SetActive(true);
        DeactivateInput();
        PlayBGM(_successMusic);
    }
    

    private void EnemyReturnToPatrol()
    {
        
    }

    private void PLayerFound(Transform enemyThatFoundPlayer)
    {
        if (_isGoalReached) return;
        _isFadingIn = true;
        _failedPanel.SetActive(true);
        _fpController.CinemachineCameraTarget.transform.LookAt(enemyThatFoundPlayer);
        DeactivateInput();
        PlayBGM(_caughtMusic);
    }

    private void DeactivateInput()
    {
        _playerInput.DeactivateInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayBGM(AudioClip newGBM) 
    {
        if (_bgmSource.clip== newGBM) return;
        _bgmSource.clip = newGBM;
        _bgmSource.Play();
    }

    private void EnemyInvestigating()
    {
         
    }

    public void RestartScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFadingIn)
        {
            if (_fadeLevel < 1f)
            {
                _fadeLevel += Time.deltaTime / _canvasFadeLine;
                
            }
        }
        else
        {
            if (_fadeLevel > 0f)
            {
                _fadeLevel -= Time.deltaTime / _canvasFadeLine;
            }
            
        }
        _canvasGroup.alpha = _fadeLevel;
    }
}
