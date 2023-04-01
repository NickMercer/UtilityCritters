using System;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

[Serializable]
public class AnimalStats
{
    [Header("Health")]
    [SerializeField, ShowOnly]
    private int _health;
    public int Health => _health;
    [ShowOnly]
    public int MaxHealth;
    
    [Space(10)]
    [Header("Hunger")]
    [SerializeField, ShowOnly]
    private int _hunger;
    public int Hunger => _hunger;
    [ShowOnly]
    public int MaxHunger;
    
    [Space(10)]
    [Header("Thirst")]
    [SerializeField, ShowOnly]
    private int _thirst;
    public int Thirst => _thirst;
    [ShowOnly]
    public int MaxThirst;
    
    [Space(10)]
    [Header("Energy")]
    [SerializeField, ShowOnly]
    private int _energy;
    public int Energy => _energy;
    
    [ShowOnly]
    public int MaxEnergy;

    public float HungerPercentage => Hunger / (float) MaxHunger;

    public float ThirstPercentage => Thirst / (float) MaxThirst;

    public float EnergyPercentage => Energy / (float) MaxEnergy;

    public float HealthPercentage => Health / (float) MaxHealth;
    

    public AnimalStats(int maxHealth, int maxHunger, int maxThirst, int maxEnergy)
    {
        MaxHealth = maxHealth;
        _health = maxHealth;

        MaxHunger = maxHunger;
        // Set Hunger to a random value between 25% and 60%
        _hunger = Mathf.RoundToInt(maxHunger * Random.Range(0.25f, 0.6f));

        MaxThirst = maxThirst;
        // Set Thirst to a random value between 25% and 60%
        _thirst = Mathf.RoundToInt(maxThirst * Random.Range(0.1f, 0.35f));

        MaxEnergy = maxEnergy;
        // Set Energy to a random value between 50% and 100%
        _energy = Mathf.RoundToInt(maxEnergy * Random.Range(0.5f, 1f));
    }

    public void Tick()
    {
        _hunger = Mathf.Clamp(_hunger + 1, 0, MaxHunger);
        _thirst = Mathf.Clamp(_thirst + 1, 0, MaxThirst);
        _energy = Mathf.Clamp(_energy - 1, 0, MaxEnergy);

        if (_energy < 5)
            _health = Mathf.Clamp(_health - 1, 0, MaxHealth);
        
        if (_hunger == MaxHunger)
            _health = Mathf.Clamp(_health - 1, 0, MaxHealth);
        
        if (_thirst == MaxThirst)
            _health = Mathf.Clamp(_health - 1, 0, MaxHealth);
    }

    public void UpdateHunger(int hunger)
    {
        _hunger += hunger;
    }

    public void UpdateThirst(int thirst)
    {
        _thirst += thirst;
    }

    public void RegenerateEnergy(float secondsToRegen, int regenPerSecond)
    {
        DOTween.To(() => Energy, 
            (x) => _energy = x, 
            Mathf.Min(_energy + (regenPerSecond * (int)secondsToRegen), MaxEnergy), 
            secondsToRegen)
            .Play();
    }

    public void UpdateHealth(int health)
    {
        DOTween.To(() => Health, 
            x => _health = x,
            Mathf.Clamp(_health + health, 0, MaxHealth),
            1f)
            .Play();
    }
}