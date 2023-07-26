using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchActive : MonoBehaviour
{
    //public GameObject punchZone;
    public float punchDamage = 20f; // ความเสียหายที่จะก่อให้กับ Enemy
    public Collider punchZone;

    void Start()
    {
        punchZone.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            punchZone.enabled = true; // เมื่อกดคลิกเมาส์ซ้าย เปิดใช้งาน GameObject punchZone
        }
        else if (Input.GetMouseButtonUp(0))
        {
            punchZone.enabled = false; ; // เมื่อปล่อยเมาส์ซ้าย ปิดใช้งาน GameObject punchZone
        }
    }

    private void OnTriggerEnter(Collider Zone)
    {
        // เมื่อ punchZone เข้าสัมผัสกับ Collider ของ Enemy
        // ตรวจสอบก่อนว่า GameObject ที่สัมผัสคือ Enemy หรือไม่
        if (Zone.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("find Enemy");
            EnemyHealth enemyHealth = Zone.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // ส่งคำขอให้ EnemyHealth ลดเลือด
                enemyHealth.TakeDamage(punchDamage);
                Debug.Log("Enemy was hit! Damage: " + punchDamage);
            }
        }
    }
}
