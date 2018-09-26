using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishingLine : MonoBehaviour
{
    public bool reeledIn = true;

    [SerializeField]
    private Transform lineStart;
    [SerializeField]
    private Transform lineEnd;
    [SerializeField]
    private int numberOfJoints = 10;
    private float restLenght;
    private LineRenderer lineRenderer;
    private LineParticle[] lineParticles;
    private Vector3[] linePositions;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineParticles = new LineParticle[numberOfJoints];
        linePositions = new Vector3[lineParticles.Length];
        lineRenderer.positionCount = lineParticles.Length;
        for (int i = 0; i < lineParticles.Length; i++)
        {
            lineParticles[i] = new LineParticle();
        }
    }

    private void FixedUpdate()
    {
        if (reeledIn) OnReeledIn();
        else OnThrown();
        SetLineRenderPositions();
        lineRenderer.SetPositions(linePositions);
    }

    private void OnReeledIn()
    {
        restLenght = .15f / numberOfJoints;
        lineParticles[0].pos = lineStart.position;
        CalculateLineParticles();
        lineEnd.position = lineParticles[lineParticles.Length - 1].pos;
    }

    private void OnThrown()
    {
        restLenght = CalculateRestLenght();
        lineParticles[0].pos = lineStart.position;
        lineParticles[lineParticles.Length - 1].pos = lineEnd.position;
        CalculateLineParticles();
    }

    private void CalculateLineParticles()
    {
        for (int i = 1; i < lineParticles.Length; i++)
        {
            Verlet(lineParticles[i], Time.deltaTime);
        }

        for (int i = 0; i < lineParticles.Length - 1; i++)
        {
            PoleConstraint(lineParticles[i], lineParticles[i + 1], restLenght);
        }
    }

    private float CalculateRestLenght()
    {
        var delta = lineEnd.position - lineStart.position;
        var deltaLenght = delta.magnitude;
        return deltaLenght / lineParticles.Length;
    }

    private void Verlet(LineParticle p, float dt)
    {
        var temp = p.pos;
        p.pos += p.pos - p.oldPos + (p.acceleration * dt * dt);
        p.oldPos = temp;
    }

    private void PoleConstraint(LineParticle p1, LineParticle p2, float restLength)
    {
        var delta = p2.pos - p1.pos;

        var deltaLength = delta.magnitude;

        var diff = (deltaLength - restLength) / deltaLength;

        p1.pos += delta * diff * 0.5f;
        p2.pos -= delta * diff * 0.5f;
    }

    private void SetLineRenderPositions()
    {
        for (int i = 0; i < lineParticles.Length; i++)
        {
            linePositions[i] = lineParticles[i].pos;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (linePositions.Length == 0)
    //        return;

    //    for (int i = 0; i < linePositions.Length; i++)
    //    {
    //        Gizmos.DrawWireSphere(linePositions[i], .25f);
    //    }
    //}
}
