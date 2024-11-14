using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,75,0);
    public float timeToFade = 1f;
    public float timeElapsed = 0f;
    private Color startColor;
    RectTransform textTranform;
    TextMeshProUGUI textMeshPro;

    private void Awake(){

        textTranform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update(){

        textTranform.position += moveSpeed*Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed<timeToFade){
            float fadeAlpha = startColor.a * (1 - timeElapsed/timeToFade);
            textMeshPro.color = new Color(startColor.r,startColor.g,startColor.b,fadeAlpha);
        }
        else{
            Destroy(gameObject);
        }
    }
}
