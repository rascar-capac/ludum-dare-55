using MoreMountains.Feedbacks;
using UnityEngine;

public class ButtonBox : MonoBehaviour
{
    public Target Health => _health;
    public bool IsDead => _health.IsDead;

    [SerializeField] private Target _health;
    [SerializeField] private MMF_Player _pressedFeedback;
    [SerializeField] private Sprite _pressedButtonSprite;
    [SerializeField] public SpriteRenderer _button;

    private Sprite _normalButtonSprite;

    public void PlayFeedback(float intensity)
    {
        _pressedFeedback.PlayFeedbacks();
        _pressedFeedback.FeedbacksIntensity = intensity * 5;
    }

    public void UpdateCooldownFeedbacks(float nextAvailableTime)
    {
        if(Time.time < nextAvailableTime)
        {
            _button.color = Game.Data.ButtonGradient.Evaluate((Time.time - nextAvailableTime + Game.Data.SpawningCooldown) / Game.Data.SpawningCooldown);
            _button.sprite = _pressedButtonSprite;
        }
        else
        {
            _button.sprite = _normalButtonSprite;
        }

        //TODO: add spread effect when ready
    }

    private void Awake()
    {
        _health.Initialize(Game.Data.ButtonHealth);
        _normalButtonSprite = _button.sprite;
    }
}
