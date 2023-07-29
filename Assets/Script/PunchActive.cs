using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchActive : MonoBehaviour
{
    //public GameObject punchZone;
    public PlayerData PlayerData;
    private float punchDamage; // ความเสียหายที่จะก่อให้กับ Enemy
    public Collider punchZone;

    void Start()
    {
        punchDamage = PlayerData.baseDamage;
        punchZone.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            punchZone.enabled = true; // กดคลิกเมาส์ซ้าย เปิดใช้งาน GameObject punchZone
        }
        else if (Input.GetMouseButtonUp(0))
        {
            punchZone.enabled = false; ; // ปล่อยเมาส์ ปิดใช้งาน GameObject punchZone
        }
    }

    private void OnTriggerEnter(Collider Zone)        // เมื่อ punchZone ชนกับ Collider ของ Enemy
    {
        if (Zone.gameObject.CompareTag("Enemy"))// เช้กก่อนว่า GameObject ที่สัมผัสคือ Enemy หรือไม่
        {
            Debug.Log("found Enemy");
            IHealthBar enemyHealth = Zone.GetComponent<IHealthBar>();
            if (enemyHealth != null)
            {
                //ขอให้สคริป EnemyHealth ลดเลือด
                enemyHealth.TakeDamage(punchDamage);
                Debug.Log("Enemy was hit! Damage: " + punchDamage);
            }
        }
    }
}