using System;
using System.Collections;
using System.Collections.Generic;
using Game.MatchGame.ScriptsOnly.Scripts.CardMatchGame;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.MatchGame.ScriptsOnly.Scripts.Ephemeral.MainGame
{
    public class MainGameController : MonoBehaviour, IMainGameController
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject cardList;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject resetButton;
        [SerializeField] private GameObject replayButton;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Sprite cardBack;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Slider sizeSlider;
        [SerializeField] private Card spritePreload;
        [SerializeField] private Text sizeLabel;
        [SerializeField] private Text timer;
        [SerializeField] private Text score;
        [SerializeField] private Text turns;
        [SerializeField] private Text winMsg;

        public static MainGameController Instance;
        private static int _gameSize = 2;

        private Card[] _cards;
        private int _spriteSelected;
        private int _cardSelected;
        private int _cardLeft;
        private bool _gameStart;
        private float _time;
        private int _winningCount;
        private int _missingCount;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetupGame(false, false, true);
        }

        private void SetupGame(bool gameStart, bool showGamePanel, bool showInfoPanel)
        {
            LoadGameData();
            _gameStart = gameStart;
            var panels = new List<GameObject> { gamePanel, infoPanel, menuPanel };
            var visibilityFlags = new List<bool> { showGamePanel, !showInfoPanel, !showGamePanel };
        }

        private void LoadGameData()
        {
            sizeSlider.value = _gameSize;
            SetGameSize();
        }

        public void SetGameSize()
        {
            _gameSize = (int)sizeSlider.value;
            sizeLabel.text = _gameSize + " X " + _gameSize;
        }
     
    }
}