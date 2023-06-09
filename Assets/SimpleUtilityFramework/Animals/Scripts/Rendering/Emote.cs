using DG.Tweening;
using UnityEngine;

public class Emote : MonoBehaviour
{
    [SerializeField]
    private Sprite _hungerEmote;

    [SerializeField]
    private Sprite _thirstSprite;

    [SerializeField]
    private Sprite _sleepEmote;

    [SerializeField]
    private Sprite _chaseEmote;

    [SerializeField]
    private Sprite _runEmote;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public void Initialize(EmoteType emoteType, float duration = 2f)
    {
        switch (emoteType)
        {
            case EmoteType.Hunger:
                _spriteRenderer.sprite = _hungerEmote;
                break;
            
            case EmoteType.Thirst:
                _spriteRenderer.sprite = _thirstSprite;
                break;
            
            case EmoteType.Sleep:
                _spriteRenderer.sprite = _sleepEmote;
                break;
            
            case EmoteType.Chase:
                _spriteRenderer.sprite = _chaseEmote;
                break;
            
            case EmoteType.Run:
                _spriteRenderer.sprite = _runEmote;
                break;
        }
        
        transform
            .DOLocalMoveY(transform.localPosition.y + 0.2f, duration)
            .OnComplete(() => Destroy(gameObject));
    }
}