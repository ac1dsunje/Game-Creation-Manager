using _Game.Scripts.Interactive.Employees.Events;
using UnityEngine;

namespace _Game.Scripts
{
public class AudioManager: MonoBehaviour
{
    public void PlaySound(SoundData sound, Vector2 position)
    {
        if (sound.Audio != null)
        {
            AudioSource.PlayClipAtPoint(sound.Audio, position, sound.Volume);
        }
    }
}
}