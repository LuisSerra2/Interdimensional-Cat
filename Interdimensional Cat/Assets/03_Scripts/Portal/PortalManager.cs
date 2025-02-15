using UnityEngine;

public enum PortalRunes
{
    One,
    Two,
    Three
}

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject portalVisual;

    private const string PortalBlend = "PortalBlend";
    private int runesIndex = 0;

    private Animator portalAnimator;

    public PortalRunes runes;

    private void OnEnable()
    {
        GameController.Instance.OnPickRune += RunesController;
    }

    private void OnDisable()
    {
        GameController.Instance.OnPickRune -= RunesController;
    }

    private void Start()
    {
        if (portalVisual != null)
        {
            portalAnimator = portalVisual.GetComponentInChildren<Animator>();
        }

        switch (runes)
        {
            case PortalRunes.One:
                PortalAnimation(.5f);
                break;
            case PortalRunes.Two:
                PortalAnimation(0);
                break;
            case PortalRunes.Three:
                if (portalVisual.activeInHierarchy)
                    portalVisual.SetActive(false);
                break;
        }
    }

    private void PortalAnimation(float value)
    {
        portalAnimator.SetFloat(PortalBlend, value);
    }

    private void RunesController()
    {
        runesIndex++;

        switch (runes)
        {
            case PortalRunes.One:
                if (runesIndex == 1)
                {
                    PortalAnimation(1);
                    GameController.Instance.OnPortalUnlockEvent();
                }
                break;
            case PortalRunes.Two:
                if (runesIndex == 1)
                {
                    PortalAnimation(0.5f);
                } else if (runesIndex == 2)
                {
                    PortalAnimation(1f);
                    GameController.Instance.OnPortalUnlockEvent();
                }
                break;
            case PortalRunes.Three:
                if (runesIndex == 1)
                {
                    portalVisual.SetActive(true);
                    PortalAnimation(0);
                } else if (runesIndex == 2)
                {
                    PortalAnimation(0.5f);
                } else if (runesIndex == 3)
                {
                    PortalAnimation(1f);
                    GameController.Instance.OnPortalUnlockEvent();
                }
                break;
        }
    }
}
