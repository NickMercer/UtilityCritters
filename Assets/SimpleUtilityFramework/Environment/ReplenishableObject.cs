using System.Collections;
using SimpleUtilityFramework.Environment;
using UnityEngine;

public class ReplenishableObject : MonoBehaviour, IConsumable
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _secondsToRespawn = 1f;

    public bool IsAvailable { get; private set; } = true;

    public void Consume(IConsumer consumer)
    {
        _spriteRenderer.color = Color.gray;
        IsAvailable = false;
        StartCoroutine(Replenish());
    }

    private IEnumerator Replenish()
    {
        yield return new WaitForSeconds(_secondsToRespawn);
        _spriteRenderer.color = Color.white;
        IsAvailable = true;
    }
}
