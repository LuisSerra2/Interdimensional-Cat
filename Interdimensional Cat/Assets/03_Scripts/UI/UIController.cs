using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform Menu;
    [SerializeField] private float duration = .5f;

    [Header("Buttons")]
    [SerializeField] private Button Resume;
    [SerializeField] private Button MainMenu;

    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI fishText;
    [SerializeField] private int fishAmount;
    [SerializeField] private TextMeshProUGUI runesText;
    [SerializeField] private int runesAmount;

    private bool IsActive;

    private void Start()
    {
        IsActive = false;
        fishText.text = fishAmount.ToString() + "x";
        runesText.text = runesAmount.ToString() + "x";
        InitializeButtons();
    }

    private void OnEnable()
    {
        GameController.Instance.OnMenu += OnMenu;
    }


    private void OnDisable()
    {
        GameController.Instance.OnMenu -= OnMenu;
    }
    private void OnMenu()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            MenuAnimation(Vector3.one, duration);
        } else
        {
            MenuAnimation(Vector3.zero, duration);
        }
    }

    private void MenuAnimation(Vector3 value, float duration)
    {
        Menu.DOScale(value, duration);
    }

    private void InitializeButtons()
    {
        Resume.onClick.AddListener(OnResumeButton);
        MainMenu.onClick.AddListener(OnMainMenuButton);
    }

    private void OnResumeButton()
    {
        OnMenu();
        GameController.Instance.ChangeState(GameState.Playing);
    }
    private void OnMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
