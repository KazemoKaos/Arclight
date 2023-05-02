using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour
{
    [SerializeField] CanvasGroup hitIndicator;
    public float timeDelay = .1f;     // How long hit image is on screen before starting to fade

    [SerializeField] float lightDamageAlpha = .2f;
    [SerializeField] float medDamageAlpha = .35f;
    [SerializeField] float heavyDamageAlpha = .7f;

    private void Update()
    {
        if(hitIndicator.alpha >= 0f)
        {
            hitIndicator.alpha -= Time.deltaTime * timeDelay;
        }
    }

    void PlayerHit()
    {
        hitIndicator.alpha = lightDamageAlpha;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerHit += PlayerHit;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerHit -= PlayerHit;
    }
}
