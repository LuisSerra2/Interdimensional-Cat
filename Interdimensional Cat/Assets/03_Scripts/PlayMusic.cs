using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public MusicType Type;

    private void Start()
    {
        GameController.Instance.PlayMusic(Type);
    }
}
