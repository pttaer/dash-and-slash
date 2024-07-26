using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDDamagePlayerView : MonoBehaviour
{
    // Script attach to any gameobject to damage the player when it collide
    // Need a collider

    [SerializeField] int m_DamageToPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Run here DAMAGE");
            MDDamageController.Api.DamagePlayer(m_DamageToPlayer);
        }
    }
}
