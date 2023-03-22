using System;
using UnityEngine;

[Serializable]
public class AnimalStats
{
    public int MaxHealth;

    [SerializeField]
    private int _health;
    public int Health => _health;

    public int MaxHunger;

    [SerializeField]
    private int _hunger;
    public int Hunger => _hunger;

    public int MaxThirst;

    [SerializeField]
    private int _thirst;
    public int Thirst => _thirst;

    public int MaxEnergy;

    [SerializeField]
    private int _energy;
    public int Energy => _energy;
    
    [SerializeField]
    private int _sleepThreshold;
    public int SleepThreshold => _sleepThreshold;
    
    
    public AnimalStats(int maxHealth, int maxHunger, int maxThirst, int maxEnergy, int sleepThreshold)
    {
        MaxHealth = maxHealth;
        _health = maxHealth;

        MaxHunger = maxHunger;
        _hunger = maxHunger;

        MaxThirst = maxThirst;
        _thirst = maxThirst;

        MaxEnergy = maxEnergy;
        _energy = maxEnergy;

        _sleepThreshold = sleepThreshold;
    }

    public void Tick()
    {
        _hunger = Mathf.Clamp(_hunger + 1, 0, MaxHunger);
        _thirst = Mathf.Clamp(_thirst + 1, 0, MaxThirst);
        _energy = Mathf.Clamp(_energy - 1, 0, MaxEnergy);
    }
}