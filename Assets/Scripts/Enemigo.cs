using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Estados estadoActual;
    public float distanciaSeguir;
    public float distanciaAtacar;
    public float distanciaEscapar;

    public bool AutoSeleccionarTarget = true;
    public Transform target;
    public float distancia;

    public bool vivo= true;


    public void Awake()
    {
        if (AutoSeleccionarTarget)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(CalcularDistancia());

    }

    private void LateUpdate()
    {
        VerificarEstado();
    }

    private void VerificarEstado()
    {
        switch (estadoActual)
        {
            case Estados.idle:
                EstadoIdle();
                break;
            case Estados.seguir:
                EstadoSeguir();
                break;
            case Estados.atacar:
                EstadoAtacar();
                break;
            case Estados.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }

    public void CambiarEstado(Estados nuevoEstado)
    {
        switch (nuevoEstado)
        {
            case Estados.idle:
                Debug.Log("Cambiando a estado Idle.");
                break;
            case Estados.seguir:
                Debug.Log("Cambiando a estado Seguir.");
                break;
            case Estados.atacar:
                Debug.Log("Cambiando a estado Atacar.");
                break;
            case Estados.muerto:
                Debug.Log("Cambiando a estado Muerto.");
                vivo = false;
                break;
            default:
                break;
        }

        estadoActual = nuevoEstado;
    }

    public virtual void EstadoIdle()
    {
        Debug.Log("El enemigo esta en reposo.");
        if (distancia < distanciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
        

    }
    public virtual void EstadoSeguir()
    {
        Debug.Log("El enemigo sigue al jugador.");
        if (distancia < distanciaAtacar)
        {
            CambiarEstado(Estados.atacar);
        }
        else if (distancia > distanciaEscapar)
        {
            CambiarEstado(Estados.idle);
        }
    }
    public virtual void EstadoAtacar()
    {
        Debug.Log("El enemigo ataca al jugador.");
        if (distancia > distanciaAtacar + 0.4f)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoMuerto()
    {
        
        
    }

    IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            if (target != null)
            {
                distancia = Vector3.Distance(transform.position, target.position);
                yield return new WaitForSeconds(0.3f);
            }
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtacar);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaSeguir);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanciaEscapar);
    }

    private void OnDrawGizmos()
    {
        int icono = (int)estadoActual;
        icono++;
        Gizmos.DrawIcon(transform.position + Vector3.up*2.2f, +icono+".png",false);
    }
}

public enum Estados
{
    idle = 0,
    seguir = 1,
    atacar = 2,
    muerto = 3

}