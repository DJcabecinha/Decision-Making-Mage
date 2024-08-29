using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public int atributo;
    private int[] numeros = { 0, 1 };
    public Transform target;
    public GameObject projectilePrefab;
    private float speed = 3.5f;
    public float attackRange;
    private bool isAttackingRange = false;
    private GameObject player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        atributo = Random.Range(0, numeros.Length);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (atributo == 0)
        {
            Guerreiro();
        }
        else if (atributo == 1)
        {
            Arqueiro();
        }
    }

    void Guerreiro()
    {
        attackRange = Vector3.Distance(transform.position, target.position);

        if (attackRange < 3)
        {

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void Arqueiro()
    {
        attackRange = Vector3.Distance(transform.position, target.position);

        if (attackRange < 10)
        {
            if (!isAttackingRange)
            {
                StartCoroutine(ataqueLonge());
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    IEnumerator ataqueLonge()
    {
        isAttackingRange = true;
        yield return new WaitForSeconds(5);
        
        if (player.activeSelf)
        {
            Atirar();
        }

        isAttackingRange = false;
    }

    void Atirar()
    {
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Flecha projScript = projectile.GetComponent<Flecha>();
        projScript.SetTarget(target);
    }
}
