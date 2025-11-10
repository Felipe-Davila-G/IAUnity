using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoZombie : Enemigo
{

    private NavMeshAgent agente;


    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();
    }
    
    public override void EstadoIdle()
    {
        base.EstadoIdle();
        agente.SetDestination(transform.position);


    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        agente.SetDestination(target.position);
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        agente.SetDestination(transform.position);
        transform.LookAt(target,Vector3.up);
    }

    public override void EstadoMuerto()
    {
        base.EstadoMuerto();
        
        agente.enabled = false;
    }
}
