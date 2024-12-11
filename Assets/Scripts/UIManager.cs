using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    // public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake(){
        gameCanvas = FindObjectOfType<Canvas>();
        CharacterEvent.characterDamaged += CharacterTookDamage;
    }
    
    private void OnEnable(){
        CharacterEvent.characterDamaged += CharacterTookDamage;
    }

    private void OnDisable(){
        CharacterEvent.characterDamaged -= CharacterTookDamage;
    }

    public void CharacterTookDamage(GameObject character,int damageReceived){
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab,spawnPosition,Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScene");
#endif
            
        }
    }
}
