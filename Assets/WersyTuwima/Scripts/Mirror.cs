using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private Transform _reflection;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _offsetY;

    [SerializeField]
    private Animator _playerAnimator;
    [SerializeField]
    private Animator _reflectionAnimator;

    private void Update()
    {
        Vector3 reflectedPosition = _player.position;
        reflectedPosition.y = 2 * _offsetY - _player.position.y;

        _reflection.transform.position = reflectedPosition;

        bool isPlayerFlipped = _player.localScale.x < 0;

        _reflection.localScale = new Vector3(isPlayerFlipped ? -1 : 1, 1, 1);

        SyncAnimator();

        // This solution is far from perfect, because it changes the transparency
        // of individual parts, but Unity has no simple way to fix it.
        ApplyForAllParts(_reflection, (spriteRenderer) =>
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        });
    }

    private void ApplyForAllParts(Transform parent, System.Action<SpriteRenderer> action)
    {
        foreach (SpriteRenderer spriteRenderer in parent.GetComponentsInChildren<SpriteRenderer>())
        {
            action(spriteRenderer);
        }
    }

    private void SyncAnimator()
    {
        foreach (AnimatorControllerParameter parameter in _playerAnimator.parameters)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Bool:
                    _reflectionAnimator.SetBool(parameter.name, _playerAnimator.GetBool(parameter.name));
                    break;
                case AnimatorControllerParameterType.Float:
                    _reflectionAnimator.SetFloat(parameter.name, _playerAnimator.GetFloat(parameter.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    _reflectionAnimator.SetInteger(parameter.name, _playerAnimator.GetInteger(parameter.name));
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if (_playerAnimator.GetBool(parameter.name))
                        _reflectionAnimator.SetTrigger(parameter.name);
                    break;
            }
        }
    }
}