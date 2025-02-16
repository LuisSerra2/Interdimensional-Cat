using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool _canSwitch = true;

    [Header("MainMenu")]
    public List<MainMenuData> mainMenuData = new List<MainMenuData>();

    public bool animationChange;


    private void Start()
    {
        for (int i = 0; i < mainMenuData.Count; i++)
        {
            mainMenuData[i].inCenter = (i == 0);
        }

        SetupMainMenuButtons();
    }

    private void SetupMainMenuButtons()
    {
        foreach (MainMenuData menu in mainMenuData)
        {
            foreach (ButtonsData item in menu.buttons)
            {
                item.button.onClick.AddListener(() => ChangePanel(item.GoToPanelIndex));
            }
        }
    }

    private void ChangePanel(int panelIndex) 
    {
        if (mainMenuData[panelIndex].inCenter == true) return;

        if (_canSwitch)
        {
            _canSwitch = false;

            for (int i = 0; i < mainMenuData.Count; i++)
            {
                if (mainMenuData[i].inCenter)
                {
                    if (animationChange)
                    {
                        mainMenuData[i].ScaleAnimationOut();
                        mainMenuData[panelIndex].ScaleAnimationIn(() =>
                        {
                            _canSwitch = true;
                        });

                    } else
                    {
                        mainMenuData[i].PositionAnimationOut();
                        mainMenuData[panelIndex].PositionAnimationIn(() =>
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

[System.Serializable]
public class MainMenuData 
{
    public bool inCenter { get; set; }

    public RectTransform panel;
    public CanvasGroup panelCG;

    public Vector2 positionIn;
    public Vector2 positionOut;

    public List<ButtonsData> buttons = new List<ButtonsData>();

    public void PositionAnimationIn(Action action)
    {
        panel.DOAnchorPos(positionIn, 0.5f).OnComplete(() =>
        {
            action.Invoke();
            inCenter = true;
        });
    }

    public void PositionAnimationOut()
    {      
        panel.DOAnchorPos(positionOut, 0.5f);
        inCenter = false;
    }

    public void ScaleAnimationIn(Action action)
    {
        panelCG.enabled = true;
        panelCG.DOFade(1, 0.5f);
        panel.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            panelCG.enabled = false;
            action.Invoke();
            inCenter = true;
        });
    }

    public void ScaleAnimationOut()
    {
        panelCG.enabled = true;
        panelCG.DOFade(0, 1f);
        panel.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            inCenter = false;
        });
    }
}

[System.Serializable]
public class ButtonsData
{
    public Button button;
    public int GoToPanelIndex;
}
