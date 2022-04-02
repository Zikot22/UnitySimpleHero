using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] private float rangeAttackCooldown;
    [SerializeField] private float meleeAttackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private float xMeleeAttackRange;
    [SerializeField] private float yMeleeAttackRange;
    [SerializeField] private int meleeDamage;
    private Animator anim;
    private HeroMovement heroMovement;
    private float rangeCooldownTimer = Mathf.Infinity;
    private float meleeCooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        heroMovement = GetComponent<HeroMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && rangeCooldownTimer > rangeAttackCooldown && heroMovement.canRangeAttack())
            RangeAttack();

        if (Input.GetKeyDown(KeyCode.V) && meleeCooldownTimer > meleeAttackCooldown && heroMovement.canMeleeAttack())
        {
            MeleeAttack();
        }

        rangeCooldownTimer += Time.deltaTime;
        meleeCooldownTimer += Time.deltaTime;
    }

    private void MeleeAttack()
    {
        anim.SetTrigger("meleeAttack");
        meleeCooldownTimer = 0;

        Collider2D[] enemies = Physics2D.OverlapBoxAll(meleeAttackPosition.position, new Vector2(xMeleeAttackRange, yMeleeAttackRange), 0);
        foreach (Collider2D enemy in enemies)
        {
            if(enemy.tag == "Enemy" || enemy.tag == "NPC_damagable")
            {
                enemy.GetComponent<Health>().TakeDamage(meleeDamage);
            }
        } 
    }

    private void RangeAttack()
    {
        anim.SetTrigger("fireballAttack");
        rangeCooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection
            (Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}