using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    [Header("Image Echo VFX")]
    [Range(.01f, .2f)]
    [SerializeField] private float imageEchoInterval = 0.5f;
    [SerializeField] private GameObject imageEchoPrefab;
    private Coroutine imageEchoCoroutine;

    public void DoImageEchoEffect(float duration)
    {
        if (imageEchoCoroutine != null)
            StopCoroutine(imageEchoCoroutine);

        imageEchoCoroutine = StartCoroutine(ImageEchoEffectRoutine(duration));

    }

    private IEnumerator ImageEchoEffectRoutine(float duration)
    {
        float timeTracker = 0;

        while (timeTracker < duration)
        {
            CreateImageEcho();

            yield return new WaitForSeconds(imageEchoInterval);
            timeTracker = timeTracker + imageEchoInterval;
        }
    }

    private void CreateImageEcho()
    {
        GameObject imageEcho = Instantiate(imageEchoPrefab, transform.position, transform.rotation);
        imageEcho.GetComponentInChildren<SpriteRenderer>().sprite = sr.sprite;
    }
        /* So thinking for player would need one method to do it in super form and one for seceret form unless we say you cant dash in seceret form.. which i dont like.
        but then it can grab wich ever animation. also thinking this effect would only play for a specific architype(s)
        would need to look at how this would work if you have a custom outfit and stuf...
        If i dont use it for the player (if the outfit customization is to complicated to get it working) then could jsut use if on a specific boss or certain fast enemies?
        */
 
}
