using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WaitForSeconds = UnityEngine.WaitForSeconds;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        FP,
        VR,
    }
    [Header("Accessibility")] 
    public Handed _handedness;
    public GameMode gameMode;
    
    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _succesPanel;
    [SerializeField] private float _canvasFadeLine = 2f;
    [SerializeField] private Material _skyboxMaterial;

    [Header("Audio")] 
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _caughtMusic;
    [SerializeField] private AudioClip _successMusic;
    
    private PlayerInput _playerInput;
    private bool _isFadingIn = false;
    private float _fadeLevel = 0;
    private FirstPersonController _fpController;
    private bool _isGoalReached = false;

    private Color _initialSkyBoxColor;
    private float _initialSkyBoxAtmosphereThickness;
    private float _initialSkyBoxExposure;
    
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
        ResetShaderValues();
        _initialSkyBoxAtmosphereThickness = _skyboxMaterial.GetFloat("_AtmosphereThickness");
        _initialSkyBoxColor = _skyboxMaterial.GetColor("_SkyTint");
        _initialSkyBoxExposure = _skyboxMaterial.GetFloat("_Exposure");
        

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
        _failedPanel.SetActive(true);
        if (gameMode == GameMode.FP)
        {
            _isFadingIn = true;
            
            _fpController.CinemachineCameraTarget.transform.LookAt(enemyThatFoundPlayer);
        }
        else
        {
           
            StartCoroutine(GameOverFadeOutSaturation(1f));
            
        }
       
        DeactivateInput();
        PlayBGM(_caughtMusic);
    }

    private IEnumerator GameOverFadeOutSaturation(float _startDelay = 0f)
    {
        yield return new WaitForSeconds(_startDelay);
        Time.timeScale = 0;
        float fade = 0f;
        while (fade < 1)
        {
            fade += Time.unscaledDeltaTime / _canvasFadeLine;
            Shader.SetGlobalFloat("_AllWhite", fade);
            _skyboxMaterial.SetFloat("_AtmosphereThickness" ,Mathf.Lerp( _initialSkyBoxAtmosphereThickness, 0.7f, fade));
            _skyboxMaterial.SetColor("_SkyTint", Color.Lerp(_initialSkyBoxColor, Color.white, fade));
            _skyboxMaterial.SetFloat("_Exposure",Mathf.Lerp(_initialSkyBoxExposure, 8, fade));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);
        RestartScene();
    }

    private void OnDestroy()
    {
        ResetShaderValues();
    }

    private void ResetShaderValues()
    {
        Shader.SetGlobalFloat("_AllWhite", 0);
        _skyboxMaterial.SetFloat("_AtmosphereThickness" , _initialSkyBoxAtmosphereThickness);
        _skyboxMaterial.SetColor("_SkyTint", _initialSkyBoxColor);
        _skyboxMaterial.SetFloat("_Exposure",_initialSkyBoxExposure);
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
        ResetShaderValues();
        Time.timeScale = 1;
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
