using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDWeaponView : MonoBehaviour
{
    [SerializeField] int m_NormalAttackDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<MDEnemyView>().TakeDamage(m_NormalAttackDamage);
            Debug.Log("Run here " + collision.tag);
        }
    }
}
