using UnityEngine;

public class ParticleAutoReturn : MonoBehaviour
{
    private ParticleSystem ps;
    #region Unity Lifecycle
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), ps.main.duration);
    }
    #endregion
    #region Return

    private void ReturnToPool()
    {
        ParticlePool.Instance.ReturnParticle(gameObject);
    }

    #endregion
}
