using System.Collections.Generic;
using SimpleUtilityFramework.Animals;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private AnimalConfig _animalData;

    [SerializeField]
    private TMP_Text _namePlate;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField]
    private List<string> _animalNames;
    
    private void Awake()
    {
        InitializeAnimal(_animalData);
    }

    private void InitializeAnimal(AnimalConfig animalData)
    {
        //Set Species Data
        var animalName = _animalNames[Random.Range(0, _animalNames.Count)];
        _spriteRenderer.sprite = animalData.Sprite;
        _namePlate.text = $"{animalName} the {animalData.SpeciesName}";
        
        //Randomize Animal a bit
        _spriteRenderer.color = new Color(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), Random.Range(0.7f, 1f));
        var size = Random.Range(0.7f, 1f);
        transform.localScale = new Vector3(size, size, size);
    }
}
