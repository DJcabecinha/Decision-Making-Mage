using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;
    public Mago mago;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        mago = targetTransform.GetComponent<Mago>();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroi o projetil se o alvo n�o existir mais
            return;
        }

        mago.flecha = mago.flecha = true;
        // Move o projetil em dire��o ao target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Opcional: Destrua o projetil se ele estiver muito pr�ximo do target
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
            mago.vidaAtual = mago.vidaAtual - 5;
            mago.flecha = mago.flecha = false;
            // Aqui voc� pode adicionar efeitos de impacto, como dano ou part�culas
        }
    }
}
