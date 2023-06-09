using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMistSkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObject(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<EnemyMeleeDamage>().ApplyIceDamage(40);
            other.GetComponent<EnemyMeleeDamage>().ColorChange(new Color(0.6117647f, 0.9921569f, 1f));
            other.GetComponent<EnemyMeleeDamage>().ColorReset();

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "blue") {
                other.GetComponent<EnemyMoveHitBlue>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "green") {
                other.GetComponent<EnemyMoveHitGreen>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "red") {
                other.GetComponent<EnemyMoveHitRed>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "table") {
                other.GetComponent<EnemyMoveHit>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "iceboss") {
                other.GetComponent<EnemyMoveHitIce>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "windboss") {
                other.GetComponent<EnemyMoveHitWind>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "finalboss") {
                other.GetComponent<EnemyMoveHitBoss>().IceEffect();
            }
        } else if (other.gameObject.tag == "S_Enemy") {
            other.GetComponent<BossMeleeDamage>().ApplyIceDamage_B(40);
            other.GetComponent<BossMeleeDamage>().ColorChange_B(new Color(0.6117647f, 0.9921569f, 1f));
            other.GetComponent<BossMeleeDamage>().ColorReset_B();

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "blue") {
                other.GetComponent<EnemyMoveHitBlue>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "green") {
                other.GetComponent<EnemyMoveHitGreen>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "red") {
                other.GetComponent<EnemyMoveHitRed>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "table") {
                other.GetComponent<EnemyMoveHit>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "iceboss") {
                other.GetComponent<EnemyMoveHitIce>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "windboss") {
                other.GetComponent<EnemyMoveHitWind>().IceEffect();
            }

            if (other.GetComponent<EnemyColorCheck>().enemyCheck() == "finalboss") {
                other.GetComponent<EnemyMoveHitBoss>().IceEffect();
            }
        }
    }

    private IEnumerator DestroyObject(GameObject bullet) {
        yield return new WaitForSeconds(2f);
        Destroy(bullet);
    }
}
