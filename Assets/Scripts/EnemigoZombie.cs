using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoZombie : Enemigo
{

    private NavMeshAgent agente;
    public Animator animaciones;


    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();
    }
    
    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animaciones != null) animaciones.SetFloat("velocidad", 0);
        if (animaciones != null) animaciones.SetBool("atacando", false);
        agente.SetDestination(transform.position);


    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        if (animaciones != null) animaciones.SetFloat("velocidad", 1);
        if (animaciones != null) animaciones.SetBool("atacando", false);
        agente.SetDestination(target.position);
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        if (animaciones != null) animaciones.SetFloat("velocidad", 0);
        if (animaciones != null) animaciones.SetBool("atacando", true);
        agente.SetDestination(transform.position);
        transform.LookAt(target,Vector3.up);
    }

    public override void EstadoMuerto()
    {
        base.EstadoMuerto();
        if (animaciones != null) animaciones.SetBool("vivo", false);
        agente.enabled = false;
    }

    [ContextMenu("Matar")]
    public void Matar()
    {
        CambiarEstado(Estados.muerto);
    }
}
