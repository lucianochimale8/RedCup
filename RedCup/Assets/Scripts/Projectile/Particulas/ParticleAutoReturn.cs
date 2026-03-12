using UnityEngine;

public class ParticleAutoReturn : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.Play(true);
        StartCoroutine(WaitAndReturn());
    }

    private System.Collections.IEnumerator WaitAndReturn()
    {
        yield return new WaitUntil(() => !ps.isPlaying);
        ParticlePool.Instance.ReturnParticle(gameObject);
    }
}
