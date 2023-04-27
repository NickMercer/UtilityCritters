using SimpleUtilityFramework.Environment;
using UnityEngine;

public class WaterSource : MonoBehaviour, IConsumable
{
    [SerializeField]
    private WaterSourceList _sourceList;
    
    [SerializeField]
    private int _thirstToQuench = 10;
    public int ThirstToQuench => _thirstToQuench;
    
    public bool IsAvailable { get; private set; } = true;

    private void Awake()
    {
        _sourceList.Register(this);
    }

    private void OnDestroy()
    {
        _sourceList.Unregister(this);
    }

    public void Consume(IConsumer consumer)
    {
        consumer.Drink(_thirstToQuench);
    }

    public void Occupy()
    {
        IsAvailable = false;
    }

    public void Vacate()
    {
        IsAvailable = true;
    }
}
