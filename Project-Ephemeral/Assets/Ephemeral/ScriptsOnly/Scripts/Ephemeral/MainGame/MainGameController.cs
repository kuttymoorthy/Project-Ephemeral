using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Ephemeral.ScriptsOnly.Scripts
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
        [SerializeField] private Text matches;
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
        private IScoringSystem _scoringSystem;
        private IGameStateManager _gameStateManager;
        private int _score;

        //1D3182
        private void Awake()
        {
            Instance = this;
            _scoringSystem = new ScoringSystem();
            _gameStateManager = new GameStateManager();
        }

        private void Start()
        {
#if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.LandscapeRight;
#endif
            SetupGame(false, false, true);
        }

        private void SetupGame(bool gameStart, bool showGamePanel, bool showInfoPanel)
        {
            LoadGameData();
            _gameStart = gameStart;
            var panels = new List<GameObject> { gamePanel, infoPanel, menuPanel };
            var visibilityFlags = new List<bool> { showGamePanel, !showInfoPanel, !showGamePanel };
            SetPanelVisibility(panels, visibilityFlags);
        }

        private void LoadGameData()
        {
            _gameStateManager.LoadGameData(out _winningCount, out _missingCount, out _gameSize, out _score);
            _scoringSystem.Score = _score;
            sizeSlider.value = _gameSize;
            SetGameSize();
        }

        public void SetGameSize()
        {
            _gameSize = (int)sizeSlider.value;
            sizeLabel.text = _gameSize + " X " + _gameSize;
        }

        private void SetPanelVisibility(List<GameObject> panels, List<bool> visibilityFlags)
        {
            if (panels.Count != visibilityFlags.Count)
            {
                return;
            }

            for (var i = 0; i < panels.Count; i++)
            {
                panels[i].SetActive(visibilityFlags[i]);
            }
        }

        public void StartCardGame()
        {
            if (_gameStart) return;

            _gameStart = true;
            SetPanelVisibility(new List<GameObject> { gamePanel, infoPanel, menuPanel, replayButton, resetButton },
                new List<bool> { true, true, false, false, true });
            SetGamePanel();
            ResetInfo();
            _cardSelected = _spriteSelected = -1;
            _cardLeft = _cards.Length;
            SpriteCardAllocation();
            StartCoroutine(HideFace());
            _time = 0;
        }
        private void SetGamePanel()
        {
            var isOdd = _gameSize % 2;
            _cards = new Card[_gameSize * _gameSize - isOdd];

            // Clear existing cards
            foreach (Transform child in cardList.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            var panelSize = gamePanel.GetComponent<RectTransform>().sizeDelta;
            var rowSize = panelSize.x;
            var colSize = panelSize.y;
            var scale = 1.0f / _gameSize;
            var xInc = rowSize /_gameSize;
            var yInc = colSize/ _gameSize;
            var curX = -(rowSize - xInc) / 2;
            var curY = -(colSize - yInc) / 2;

            var initialX = curX;

            for (var i = 0; i < _gameSize; i++)
            {
                curX = initialX;
                for (var j = 0; j < _gameSize; j++)
                {
                    GameObject cardObj;
                    var index = i * _gameSize + j;
                    if (isOdd == 1 && i == _gameSize - 1 && j == _gameSize - 1)
                    {
                        index = _gameSize / GameConstants.GameLayoutMinSize * _gameSize +
                                _gameSize / GameConstants.GameLayoutMinSize;
                        cardObj = _cards[index].gameObject;
                    }
                    else
                    {
                        cardObj = Instantiate(prefab, cardList.transform, true);
                        _cards[index] = cardObj.GetComponent<Card>();
                        _cards[index].ID = index;
                        cardObj.transform.localScale = new Vector3(scale, scale);
                    }

                    cardObj.transform.localPosition = new Vector3(curX, curY, 0);
                    curX += xInc;
                }

                curY += yInc;
            }
        }
        
        


        private void ResetInfo()
        {
            _winningCount = 0;
            _missingCount = 0;
            score.text = $"Score: {_scoringSystem.Score}";
            matches.text = $"Matches: {_winningCount}";
            turns.text = $"Turns: {_missingCount}";
        }

        private void SpriteCardAllocation()
        {
            int[] selectedID = new int[_cards.Length / 2];

            for (int i = 0; i < _cards.Length / 2; i++)
            {
                int value = Random.Range(0, sprites.Length - 1);

                for (int j = i; j > 0; j--)
                {
                    if (selectedID[j - 1] == value)
                        value = (value + 1) % sprites.Length;
                }

                selectedID[i] = value;
            }

            foreach (var cardObj in _cards)
            {
                cardObj.Active();
                cardObj.SpriteID = -1;
                cardObj.ResetRotation();
            }

            for (var i = 0; i < _cards.Length / 2; i++)
            for (var j = 0; j < 2; j++)
            {
                var value = Random.Range(0, _cards.Length - 1);
                while (_cards[value].SpriteID != -1)
                    value = (value + 1) % _cards.Length;

                _cards[value].SpriteID = selectedID[i];
            }
        }

        private IEnumerator HideFace()
        {
            yield return new WaitForSeconds(0.3f);

            foreach (var objCard in _cards)
                objCard.Flip();

            yield return new WaitForSeconds(GameConstants.GameWaitForSeconds);
        }

        public Sprite GetSprite(int spriteId)
        {
            return sprites[spriteId];
        }

        public Sprite CardBack()
        {
            return cardBack;
        }

        public bool CanClick()
        {
            return _gameStart;
        }

        private void RestoreGameLayout()
        {
            if (sizeSlider != null)
            {
                // Increment the game size
                int newGameSize = Mathf.Clamp(_gameSize + 1, GameConstants.GameLayoutMinSize,
                    GameConstants.GameLayoutMaxSize);
                // Set the slider value
                sizeSlider.value = newGameSize;
                // Update the game size
                _gameSize = GameConstants.GameLayoutMinSize;
            }
        }

        public void CardClicked(int spriteId, int cardId)
        {
            if (_spriteSelected == -1)
            {
                _spriteSelected = spriteId;
                _cardSelected = cardId;
            }
            else
            {
                if (_spriteSelected == spriteId)
                {
                    _cards[_cardSelected].Inactive();
                    _cards[cardId].Inactive();
                    _cardLeft -= 2;
                    _winningCount++;
                    _scoringSystem.IncrementScore();
                    _scoringSystem.CheckCombo();
                    score.text = $"Score: {_scoringSystem.Score}"; // Update UI
                    matches.text = $"Matches: {_winningCount}";
                    CheckGameWin();
                }
                else
                {
                    _cards[_cardSelected].Flip();
                    _cards[cardId].Flip();
                    _missingCount++;
                    _scoringSystem.ResetComboStreak();
                    turns.text = $"Turns: {_missingCount}";
                }

                _cardSelected = _spriteSelected = -1;
            }
        }

        private void CheckGameWin()
        {
            if (_cardLeft != 0) return;
            _gameStart = false;
            DisplayWinInfo(true);
            SetPanelVisibility(new List<GameObject> { replayButton, resetButton }, new List<bool> { true, false });
            AudioPlayer.Instance.PlayAudio(1);
        }

        public void ReStartGame()
        {
            // Set the game size and start the game
            DisplayWinInfo(false);
            RestoreGameLayout();
            SetGameSize();
            StartCardGame();
            SaveGameData();
        }

        private void EndGame()
        {
            _gameStart = false;
            SetPanelVisibility(new List<GameObject> { gamePanel, infoPanel, menuPanel },
                new List<bool> { false, false, true });
            DisplayWinInfo(false);
        }

        public void GiveUp()
        {
           _scoringSystem.ResetScore();
           _scoringSystem.ResetComboStreak();
           _gameStateManager.ResetGameData();
            sizeSlider.value = GameConstants.GameLayoutMinSize;
            SetGameSize();
            EndGame();
        }

        public void DisplayWinInfo(bool boolValue)
        {
            if (boolValue)
            {
                winMsg.text = GameConstants.WinMsgTxt;
            }
            else
            {
                winMsg.text = "";
            }
        }

        private void Update()
        {
            if (_gameStart)
            {
                _time += Time.deltaTime;
                var timeSpan = TimeSpan.FromSeconds(_time);
                var timerText = $"Time: {timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                timer.text = timerText;
            }
        }
        
        private void SaveGameData()
        {
            _gameStateManager.SaveGameData(_winningCount, _missingCount, _gameSize,_scoringSystem.Score);
        }
    }
}