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
        private readonly Random _random = new Random();
        private readonly Context _context;
        //private int lastPlayed;

        public HalloweenSoundEffects(Context context)
        {
            _context = context;
            //lastPlayed = 0;
        }

        private static int[] SoundEffects = new[]
        {
            Resource.Raw.bells_fast,
            Resource.Raw.bells_hit,
            Resource.Raw.christmas_sound_effects_merry_christmas_santa,
            Resource.Raw.deck_the_halls_christmas_jingle_played_with_bells,
            Resource.Raw.santa_claus_merry_christmas_ho_ho_ho,
            Resource.Raw.sant_ho_ho_ho_1,
            Resource.Raw.single_jingle_bell_quick,
            Resource.Raw.sint,
            Resource.Raw.sint2
        };

        public void PlaySoundEffect()
        {
            var soundeffectKey = RandomSoundEffect();

            var mediaPlayer = MediaPlayer.Create(_context, soundeffectKey);
            mediaPlayer.Completion += (object sender, EventArgs e) =>
            {
                mediaPlayer.Release();
                mediaPlayer.Dispose();
            };

            mediaPlayer.Start();
        }

        private int RandomSoundEffect()
        {
            return SoundEffects[_random.Next(0, SoundEffects.Length - 1)];
        }

        //private int NextSoundEffect()
        //{
        //    if (lastPlayed == SoundEffects.Length - 1)
        //    {
        //        lastPlayed = 0;
        //    }
        //    else
        //    {
        //        lastPlayed++;
        //    }

        //    return SoundEffects[lastPlayed];
        //}
    }
}