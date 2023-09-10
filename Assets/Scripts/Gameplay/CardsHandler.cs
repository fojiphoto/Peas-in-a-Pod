using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardsHandler : MonoBehaviour
{
    [SerializeField] private Transform m_CardsContainer;
    [SerializeField] private GameObject m_CardPrefab;

    [SerializeField] private GameObject m_LevelCompletePanel;
    [SerializeField] private GameObject m_LevelFailPanel;

    [SerializeField] private Button m_NextButton;
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private GridLayoutGroup m_GridComponent;

    [SerializeField] private TextMeshProUGUI ScoreText;

    private int targetScore;
    private float animationDuration = 2f;

    private List<Card> m_Card = new();

    private void OnEnable()
    {
        GameEvents.GameplayEvents.CardsSpawnRequest.Register(GenerateCards);
        GameEvents.GameplayEvents.CardRemoveRequested.Register(OnCardRemoveRequested);
        GameEvents.GameplayEvents.ResetGame.Register(OnResetGame);
        GameEvents.TimerEvents.TimerComplete.Register(OnTimerComplete);
    }

    private void OnDisable()
    {
        GameEvents.GameplayEvents.CardsSpawnRequest.UnRegister(GenerateCards);
        GameEvents.GameplayEvents.CardRemoveRequested.UnRegister(OnCardRemoveRequested);
        GameEvents.GameplayEvents.ResetGame.UnRegister(OnResetGame);
        GameEvents.TimerEvents.TimerComplete.UnRegister(OnTimerComplete);

    }
    private void Start()
    {
        m_NextButton.onClick.AddListener(NextLevel);
        m_RestartButton.onClick.AddListener(RestartLevel);
    }


    void GenerateCards(List<Item> cards,int rows)
    {
        m_GridComponent.constraintCount = rows;
        SpawnAndInitializeCardsInternal(cards);
    }

    private void SpawnAndInitializeCardsInternal(List<Item> cards)
    {
        while (cards.Count > 0)
        {
            GameObject cardGO = Instantiate(m_CardPrefab, m_CardsContainer);
            Card card = cardGO.GetComponent<Card>();

            int index = Random.Range(0, cards.Count);
            
            card.Initialize(cards[index]);
            cards.RemoveAt(index);
            
            m_Card.Add(card);
        }

        Invoke(nameof(DisableGridComponent), 0.5f);
    }

    private void DisableGridComponent()
    {
        m_GridComponent.enabled = false;
    }

    void OnCardRemoveRequested(CardRequestObject cardRequestA, CardRequestObject cardRequestB)
    {

        Card card = GetItemData(cardRequestA.ID);
        m_Card.Remove(card);
        card.Destroy();
        
        card = GetItemData(cardRequestB.ID);
        
        m_Card.Remove(card);
        card.Destroy();

        CheckGameComplete();
    }

    void OnResetGame()
    {
        for (int i = 0; i < m_Card.Count; i++)
        {
            m_Card[i].Hide();
        }
    }
    private void OnTimerComplete()
    {
        GameEvents.GameplayEvents.GameComplete.Raise(false);
        m_LevelFailPanel.SetActive(true);
        FailSoundEffect();
    }

    private void FailSoundEffect() 
    {
        AudioManager.Instance.SetSoundEffectsVolume(1);
        AudioManager.Instance.SetMusicVolume(0.25f);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.Fail);
    }  
    private void PassSoundEffect() 
    {
        AudioManager.Instance.SetSoundEffectsVolume(1);
        AudioManager.Instance.SetMusicVolume(0.25f);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.Pass);
    }

    void CheckGameComplete()
    {
        if (m_Card.Count <= 0)
        {
            GameEvents.GameplayEvents.GameComplete.Raise(true);
            m_LevelCompletePanel.SetActive(true);
            UpdateScore();
            PassSoundEffect();
        }
    }

    void UpdateScore() 
    {
        int Score = ScoringManager.Instance.GetScore();
        ScoreText.text = Score.ToString();
        StartCoroutine(CountScoreUp(Score));
        StartCoroutine(ScaleUpAnimation(ScoreText.transform,1.1f,1));
    }
    private IEnumerator CountScoreUp(int targetScore)
    {
        int startScore = 0; // Start counting from 0
        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            int newScore = Mathf.RoundToInt(Mathf.Lerp(startScore, targetScore, progress));
            ScoreText.text = newScore.ToString();
            yield return null;
        }

        // Ensure the score reaches the target exactly
        ScoreText.text = targetScore.ToString();
    }

    private IEnumerator ScaleUpAnimation(Transform objectToScale, float targetScale, float duration)
    {
        Vector3 initialScale = objectToScale.localScale;
        Vector3 target = new Vector3(targetScale, targetScale, targetScale);
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            objectToScale.localScale = Vector3.Lerp(initialScale, target, progress);
            yield return null;
        }

        // Ensure the object reaches the target scale exactly
        objectToScale.localScale = target;
    }

    void NextLevel() 
    {
        PlayerPrefsManager.Set(PlayerPrefsManager.CurrentLevel, PlayerPrefsManager.Get(PlayerPrefsManager.CurrentLevel,0) + 1);    
        SceneManager.LoadScene("GameplayScene");
        AudioManager.Instance.SetMusicVolume(0.75f);
    }

    void RestartLevel() 
    {
        SceneManager.LoadScene("GameplayScene");
        AudioManager.Instance.SetMusicVolume(0.75f);
    }

    public Card GetItemData(Guid ID) => m_Card.Find(x => x.ID == ID);
}
