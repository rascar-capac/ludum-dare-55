using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public float Health {get; private set;}
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public bool IsDead => Health <= 0;

    [SerializeField] SpriteRenderer _spriteRenderer;

    private Color _initialColor;

    public UnityEvent OnDied {get;} = new();

    public void Initialize(float initial_health)
    {
        Health = initial_health;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(ApplyDamageColorFlashRoutine());

        if(Health <= 0)
        {
            _spriteRenderer.color = Game.Data.DeadColor;
            OnDied.Invoke();
        }
    }

    private IEnumerator ApplyDamageColorFlashRoutine()
    {
        _spriteRenderer.color = Game.Data.DamageTakenColor;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = IsDead ? Game.Data.DeadColor : _initialColor;
    }

    private void Awake()
    {
        _initialColor = _spriteRenderer.color;
    }
}
