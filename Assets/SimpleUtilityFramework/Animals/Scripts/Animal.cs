using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Animals;
using SimpleUtilityFramework.Environment;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour, IConsumer
{
    private const float TickSpeedInSeconds = 0.9f;

    [Header("Dependencies")]
    [SerializeField]
    private AnimalConfig _animalData;
    public AnimalConfig AnimalData => _animalData;
    
    [SerializeField]
    private TMP_Text _namePlate;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Brain _brain;
    
    [SerializeField]
    private FoodSource _foodSource;
    public FoodSource FoodSource => _foodSource;
    
    [Space(10), Header("Settings")]
    [SerializeField]
    private List<string> _animalNames;

    [Space(10), Header("Debug Data")]
    [SerializeField]
    private AnimalStats _stats;
    public AnimalStats Stats => _stats;

    
    private string _animalName;
    
    private bool _shouldTickStats = true;
    private Coroutine _tickRoutine;
    
    public Vector3 SpawnPoint { get; private set; }

    public Action<AnimalStats> StatsTicked;

    #region Initialize Animal
    
    private void Awake()
    {
        SpawnPoint = transform.position;
        
        //As of now, predators are not food sources. This will have to change if we want to make a real food chain
        if(AnimalData.FoodSourceType == FoodType.Animal)
            Destroy(_foodSource);
        
        InitializeAnimal(_animalData);
        _tickRoutine = StartCoroutine(StatsTick());
    }
    
    private void InitializeAnimal(AnimalConfig animalData)
    {
        //Set Species Data
        _spriteRenderer.sprite = animalData.Sprite;
        
        _animalName = _animalNames[Random.Range(0, _animalNames.Count)];
        var fullName = $"{_animalName} the {animalData.SpeciesName}";
        _namePlate.text = fullName;
        name = fullName;
        
        //Randomize Animal a bit
        _spriteRenderer.color = new Color(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), Random.Range(0.7f, 1f));
        var size = Random.Range(0.7f, 1f);
        transform.localScale = new Vector3(size, size, size);
        
        //Set stats
        _stats = animalData.GetAnimalStats();
    }
    
    private IEnumerator StatsTick()
    {
        while (_shouldTickStats)
        {
            yield return new WaitForSeconds(TickSpeedInSeconds);
            _stats.Tick();
            StatsTicked?.Invoke(_stats);
        }
    }

    private void OnDestroy()
    {
        _shouldTickStats = false;
        StopCoroutine(_tickRoutine);
    }

    #endregion
    
    #region AI
    
    private void Start()
    {
        //Start AI Routine
        _brain.Initialize(this);
        PickNextAIAction();
    }
    
    private void PickNextAIAction()
    {
        var nextAction = _brain.Decide();
        Debug.Log($"{_animalName} decided to {nextAction.Action.GetType().Name}");
        StartCoroutine(nextAction.Action.Act(_brain.Blackboard, nextAction.Target, PickNextAIAction));
    }
    
    #endregion

    #region IConsumer Implementation
    
    public void Feed(int foodAmount)
    {
        _stats.UpdateHunger(-foodAmount);
        _stats.UpdateHealth(foodAmount/2);
    }

    public void Drink(int thirstToQuench)
    {
        _stats.UpdateThirst(-thirstToQuench);
    }
    
    #endregion

    #region Animations

    public void AnimateEating(float duration)
    {
        transform.DOShakeRotation(duration, 30f);
    }
    
    public void AnimateDrinking(float duration)
    {
        transform.DORotate(new Vector3(0, 0, -30), duration);
    }
    
    public void ResetAnimation(float duration)
    {
        transform.DORotate(Vector3.zero, duration);
    }

    public void AnimateSleeping(float duration)
    {
        transform.DORotate(new Vector3(-45, 0, 0), duration);
    }

    #endregion
} 