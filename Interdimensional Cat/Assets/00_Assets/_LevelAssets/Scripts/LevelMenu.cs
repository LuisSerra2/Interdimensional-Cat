using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelMenu : MonoBehaviour
{
    [Header("Level")]
    public Button[] levelsButtons;
    public List<LevelsData> levelsData = new List<LevelsData>();

    public bool resetPlayerPrefab;
    public bool animationChange;

    bool _canSwitch = true;

    private void Start()
    {
        for (int i = 0; i < levelsData.Count; i++)
        {
            levelsData[i].inCenter = (i == 0);
        }

        SetupButtonsLevels();

        if (animationChange)
        {
            foreach (LevelsData level in levelsData)
            {
                level.levelPanel.localPosition = new Vector3(0, -15, 0);
                level.levelPanel.localScale = Vector3.zero;
            }

            for (int i = 0; i < levelsData.Count; i++)
            {
                if (levelsData[i].inCenter)
                {
                    levelsData[i].levelPanel.localScale = Vector3.one;
                }
            }

        }
    }

    private void SetupButtonsLevels()
    {
        int totalChildCount = 0;

        foreach (LevelsData manager in levelsData)
        {
            totalChildCount += manager.levelPanel.transform.childCount;
        }

        levelsButtons = new Button[totalChildCount];

        int index = 0;
        foreach (LevelsData manager in levelsData)
        {
            int childCount = manager.levelPanel.transform.childCount;
            for (int c = 0; c < childCount; c++)
            {
                Button levelButton = manager.levelPanel.transform.GetChild(c).GetComponent<Button>();
                levelsButtons[index] = levelButton;

                int levelIndex = index + 1;
                levelButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (levelIndex == 1) ? "Tutorial" : (levelIndex - 1).ToString();
                levelButton.onClick.AddListener(() => OnLevelButtonClick(levelIndex));

                index++;
            }
        }

        for (int i = 0; i < levelsData.Count; i++)
        {
            int buttonIndex = i;

            levelsData[i].buttonLeft = levelsData[i].levelPanel.parent.transform.GetChild(1).GetComponent<Button>();
            levelsData[i].buttonRight = levelsData[i].levelPanel.parent.transform.GetChild(2).GetComponent<Button>();

            levelsData[i].buttonLeft.onClick.AddListener(() => ChangePanel(levelsData, GetPanelIndex(buttonIndex, -1)));
            levelsData[i].buttonRight.onClick.AddListener(() => ChangePanel(levelsData, GetPanelIndex(buttonIndex, +1)));

            if (levelsData[i].inCenter)
            {
                levelsData[i].buttonLeft.gameObject.SetActive(true);
                levelsData[i].buttonRight.gameObject.SetActive(true);
            } else
            {
                levelsData[i].buttonLeft.gameObject.SetActive(false);
                levelsData[i].buttonRight.gameObject.SetActive(false);
            }
        }


        for (int i = 0; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].interactable = false;
        }

        for (int i = 0; i < Mathf.Min(GetResetPlayerPrefab(), levelsButtons.Length); i++)
        {
            levelsButtons[i].interactable = true;
        }
    }

    private int GetResetPlayerPrefab()
    {
        int unlockLevel;
        if (resetPlayerPrefab)
        {
            PlayerPrefs.SetInt("UnlockLeel", 1);
            PlayerPrefs.SetInt("ReachedIndex", 1);
            resetPlayerPrefab = false;
        }

        unlockLevel = PlayerPrefs.GetInt("UnlockLeel", 1);
        return unlockLevel;
    }

    private void OnLevelButtonClick(int index)
    {
        int maxLevel = levelsButtons.Length; 
        if (index >= maxLevel + 1)
        {
            SceneManager.LoadScene("EndScreen"); 
        } else
        {
            string levelName = (index == 1) ? "Tutorial" : (index - 1).ToString();
            SceneManager.LoadScene(levelName);
        }
    }

    private int GetPanelIndex(int currentIndex, int direction)
    {
        int nextIndex = currentIndex + direction;

        if (nextIndex >= levelsData.Count)
        {
            nextIndex = 0;
        } else if (nextIndex < 0)
        {
            nextIndex = levelsData.Count - 1;
        }

        return nextIndex;
    }

    private void ChangePanel<T>(List<T> data, int panelIndex) where T : IInCenter
    {
        if (data[panelIndex].inCenter == true) return;

        if (_canSwitch)
        {
            _canSwitch = false;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].inCenter)
                {
                    if (animationChange)
                    {
                        data[i].ScaleAnimationOut();
                        data[panelIndex].ScaleAnimationIn(() =>
                        {
                            _canSwitch = true;
                        });

                    } else
                    {
                        data[i].PositionAnimationOut();
                        data[panelIndex].PositionAnimationIn(() =>
                        {
                            _canSwitch = true;
                        });
                    }
                    break;
                }
            }
        }
    }
}



public interface IInCenter
{
    bool inCenter { get; set; }
    void PositionAnimationIn(Action action);
    void PositionAnimationOut();

    void ScaleAnimationIn(Action action);
    void ScaleAnimationOut();
}

[System.Serializable]
public class LevelsData : IInCenter
{
    public bool inCenter { get; set; }

    public RectTransform levelPanel;

    [HideInInspector]
    public Button buttonLeft;
    [HideInInspector]
    public int panelIndexLeft;

    [HideInInspector]
    public Button buttonRight;
    [HideInInspector]
    public int panelIndexRight;

    public Vector2 positionIn;
    public Vector2 positionOut;

    public void PositionAnimationIn(Action action)
    {
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(true);
        levelPanel.DOAnchorPos(positionIn, 0.5f).OnComplete(() =>
        {
            action.Invoke();
            inCenter = true;
        });
    }

    public void PositionAnimationOut()
    {
        buttonLeft.gameObject.SetActive(false);
        buttonRight.gameObject.SetActive(false);
        levelPanel.DOAnchorPos(positionOut, 0.5f);
        inCenter = false;
    }

    public void ScaleAnimationIn(Action action)
    {
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(true);
        levelPanel.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            action.Invoke();
            inCenter = true;
        });
    }
    public void ScaleAnimationOut()
    {
        buttonLeft.gameObject.SetActive(false);
        buttonRight.gameObject.SetActive(false);
        levelPanel.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            inCenter = false;
        });
    }
}