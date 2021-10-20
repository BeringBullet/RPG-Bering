using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attribute
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] Health healthComponent;
        [SerializeField] RectTransform foreground;
        [SerializeField] Canvas rootCanvas;
        // Update is called once per frame
        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(), 1f) || 
                Mathf.Approximately(healthComponent.GetFraction(), 0f))
            {
                rootCanvas.enabled = false; 
                return;
            }
             rootCanvas.enabled = true; 
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}
