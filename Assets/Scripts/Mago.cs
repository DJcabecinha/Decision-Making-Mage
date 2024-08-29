using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{
    private enum Estado{Idle, Fugir, Teleportar, Defender, Curar, Atacar};
    private Estado estadoAtual = Estado.Idle;

    private float perception = 20f;
    private float close = 3f;
    public LayerMask enemy;
    private float vidaMago = 100;
    public float vidaAtual;
    private int cast;
    public GameObject projectilePrefab;
    private Transform target;
    private float contador;

    private Collider[] closeRange;
    private Collider[] enemies;
    private bool fireballAttack = false;

    public Inimigos inimigos;

    public GameObject escudo;

    public bool flecha = false;

    void Start()
    {
        contador = 0;
        cast = 8;
        vidaAtual = vidaMago;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = Physics.OverlapSphere(transform.position, perception, enemy);
        closeRange = Physics.OverlapSphere(transform.position, close, enemy);


        if (enemies.Length > 0)
        {
            
            if (enemies.Length >= 5)
            {
                estadoAtual = Estado.Fugir;
            }
            else
            {
                if (closeRange.Length > 0)
                {
                    contador += Time.deltaTime;
                    if (closeRange.Length > 1 || contador >= 8)
                    {
                        estadoAtual = Estado.Teleportar;
                        contador = 0;
                    }
                    else
                    {
                        estadoAtual = Estado.Defender;
                    }
                }
                else
                {
                    if (flecha == true)
                    {
                        estadoAtual = Estado.Defender;
                    }
                    else
                    {
                        if (vidaAtual < 30)
                        {
                            estadoAtual = Estado.Curar;
                        }
                        else
                        {
                            estadoAtual = Estado.Atacar;
                        }
                    }
                }
            }
        }
        else
        {
            
            estadoAtual = Estado.Idle;
        }

        RealizarAção();
    }

    void RealizarAção()
    {
        escudo.SetActive(false);

        switch (estadoAtual)
            {
            case Estado.Idle:
                break;

            case Estado.Defender:
                escudo.SetActive(true);
                break;

            case Estado.Fugir:
                gameObject.SetActive(false);
                break;

            case Estado.Teleportar:
                transform.position = new Vector3(Random.Range(0, 20), 1, Random.Range(0, 20));
                break;

            case Estado.Atacar:
                if (!fireballAttack)
                {
                    StartCoroutine(attack(cast));                 
                }
                if (cast == 0)
                {
                    fireballAttack = false;
                    cast = 8;
                    AtirarMagia();
                }
                break;

            case Estado.Curar:
                vidaAtual = 100;
                break;
        }
    }

    IEnumerator attack(int castDelay)
    {
        fireballAttack = true;
        yield return new WaitForSeconds(castDelay);
        cast = 0;
    }

    void AtirarMagia()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        target = enemies[randomIndex].transform;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        mageAttack projScript = projectile.GetComponent<mageAttack>();
        projScript.SetTarget(target);
    }
}
