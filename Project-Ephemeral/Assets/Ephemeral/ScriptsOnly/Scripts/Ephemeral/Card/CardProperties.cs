﻿using UnityEngine;

namespace Game.MatchGame.ScriptsOnly.Scripts.Ephemeral
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