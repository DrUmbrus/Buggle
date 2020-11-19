using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Gun
{
    /// <summary>
    /// Saloperie de boomerang
    /// </summary>
    
    //Si le joueur meurt, on annule le cooldown.
    public override void Pattern()
    {
        if (ownerChar.life <= 0)
        {
            NoCD();
        }
    }

    //Quand on tire, l'arme devient invisible ("jetée"), et on lance la coroutine.
    public override void ShootingPattern()
    {
        sr.color = Color.clear;

        StartCoroutine(PrepareVisual());
    }

    //Si on change d'arme, le boomerang lancé est détruit.
    private void OnDestroy()
    {
        Destroy(shootyObj);
    }

    //Le "cooldown"
    IEnumerator PrepareVisual()
    {
        yield return new WaitForSeconds(1);
        Visual();
    }
}
