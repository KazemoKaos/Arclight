using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    Transform lookAt;
    Vector3 staySpot;
    [SerializeField] Vector3 lookOffset;
    [SerializeField] Transform textObj;

    [SerializeField] float destroyTime;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 randomOffset;
    [SerializeField] Color textColor;

    TextMeshProUGUI damageText;

    bool follow;

    private void Update()
    {
        // Redraw the UI to keep the element in the same spot
        if (follow)
        {
            textObj.transform.position = Camera.main.WorldToScreenPoint(staySpot);
        }
    }

    void Display()
    {
        // Set the initial position to be at what was damaged 
        textObj.transform.position = Camera.main.WorldToScreenPoint(lookAt.position + lookOffset);
        
        // Get a random offset so the numbers don't overlap
        textObj.transform.position += new Vector3(
            Random.Range(-randomOffset.x, randomOffset.x), 
            Random.Range(-randomOffset.y, randomOffset.y), 
            Random.Range(-randomOffset.z, randomOffset.z));

        // Set the font to be larger if a crit
        //damageText.fontSize = 35f;

        // Get the new position of the damage number. Since it's now screen space, convert it back to world space
        staySpot = Camera.main.ScreenToWorldPoint(textObj.position);

        // Tell the UI to always redraw the UI
        follow = true;
    }

    public void Initialize(float damageValue, Transform pos)
    {
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = damageValue.ToString();
        // Set the color to be different if a crit hit
        // damageText.color = textColor;

        // Get the position of the damaged object
        lookAt = pos;

        // Position the UI
        Display();

        // Destroy the UI element after a certain amount of time
        Destroy(gameObject, destroyTime);
    }
}
