using UnityEngine;
using UnityEngine.UI;

// This class handles a matching card

namespace Ephemeral.ScriptsOnly.Scripts
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