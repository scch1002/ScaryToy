using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace HalloweenSoundGenerator
{
    public class HalloweenSoundEffects
    {
        private readonly Dictionary<int, MediaPlayer> _soundDictionary = new Dictionary<int, MediaPlayer>();
        private readonly Random _random = new Random();
        private readonly Context _context;
        private int lastPlayed;

        public HalloweenSoundEffects(Context context)
        {
            _context = context;
            lastPlayed = 0;
        }

        private static int[] soundEffects = new[]
        {
            Resource.Raw.creepy_whispers_2,
            Resource.Raw.creepy_whispers_2_1,
            Resource.Raw.demonic_woman_scream,
            Resource.Raw.evil_laugh,
            Resource.Raw.evil_witch_cackle,
            Resource.Raw.ghost_voice_i_see_you,
            Resource.Raw.ghost_voice_why_cant_you_see_me,
            Resource.Raw.halloween_001_wav_120b,
            Resource.Raw.oh_god_wah,
            Resource.Raw.zombie,
            Resource.Raw.zombie_brains,
            Resource.Raw.Female_Scream_Horror138499973,
            Resource.Raw.Scary1449371204
        };

        public void PlaySoundEffect()
        {
            var soundeffectKey = RandomSoundEffect();

            if(!_soundDictionary.ContainsKey(soundeffectKey))
            {
                _soundDictionary.Add(soundeffectKey, MediaPlayer.Create(_context, soundeffectKey));
            }

            _soundDictionary[soundeffectKey].Start();
        }

        private int RandomSoundEffect()
        {
            return soundEffects[_random.Next(1, soundEffects.Length - 1)];
        }

        //private int NextSoundEffect()
        //{
        //    if (lastPlayed == soundEffects.Length - 1)
        //    {
        //        lastPlayed = 0;
        //    }
        //    else
        //    {
        //        lastPlayed++;
        //    }

        //    return soundEffects[lastPlayed];
        //}
    }
}