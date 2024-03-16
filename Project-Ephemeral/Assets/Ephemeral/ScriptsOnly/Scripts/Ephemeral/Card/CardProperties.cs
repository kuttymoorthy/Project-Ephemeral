using UnityEngine;

// This class handles the card properties

namespace Ephemeral.ScriptsOnly.Scripts
{
    public partial class Card : MonoBehaviour
    {
        // Properties
        public int SpriteID
        {
            set
            {
                _spriteID = value;
                _flipped = true;
                ChangeSprite();
            }
            get => _spriteID;
        }
        public int ID
        {
            set => _id = value;
            get => _id;
        }
        // Reset card default rotation
        public void ResetRotation()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            _flipped = true;
        }
    }
}