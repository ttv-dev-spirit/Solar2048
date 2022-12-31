#nullable enable
using UnityEngine;
using Zenject;

namespace Solar2048
{
    public sealed class FieldSquare : MonoBehaviour
    {
        [Inject]
        private void Construct(GameField gameField)
        {
            gameField.RegisterSquare(this);
        }
    }
}