using UnityEngine;

public class NeedsRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthObject;

    [SerializeField]
    private GameObject _healthBackground;

    [SerializeField]
    private GameObject _hungerObject;
    
    [SerializeField]
    private GameObject _thirstObject;

    [SerializeField]
    private GameObject _energyObject;

    [SerializeField]
    private Animal _animal;

    private void Awake()
    {
        _animal.StatsTicked += OnStatsTicked;
    }

    private void OnDestroy()
    {
        _animal.StatsTicked -= OnStatsTicked;
    }

    private void OnStatsTicked(AnimalStats stats)
    {
        _healthBackground.SetActive(stats.Health < stats.MaxHealth);
        _healthObject.SetActive(stats.Health < stats.MaxHealth);
        _healthObject.transform.localScale = new Vector3(stats.Health/(float)stats.MaxHealth, _healthObject.transform.localScale.y);
        _hungerObject.SetActive(stats.Hunger >= stats.MaxHunger/2);
        _thirstObject.SetActive(stats.Thirst >= stats.MaxThirst/2);
        _energyObject.SetActive(stats.Energy <= stats.MaxEnergy/2);
    }
}
