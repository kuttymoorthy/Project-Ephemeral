using UnityEngine;
using UnityEngine.UI;

namespace Game.MatchGame.ScriptsOnly.Scripts.Ephemeral
{
    public partial class Card : MonoBehaviour
    {
        // Private fields
        private int _spriteID;
        private int _id;
        private bool _flipped;
        private bool _turning;

        [SerializeField] private Image img;
    }
}