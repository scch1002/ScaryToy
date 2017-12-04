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
            Resource.Raw.alvinwhatup2__fireworks,
            Resource.Raw.davidbain__10_second_count_down,
            Resource.Raw.djnicke__crowd_cheering_and_clapping,
            Resource.Raw.ecfike__let_s_go_have_a_party_man_1,
            Resource.Raw.fireworks_new_years_eve,
            Resource.Raw.greg_baumont__uknownbottle,
            Resource.Raw.idabrandao__champagne_glasses,
            Resource.Raw.kenrt__champagne_cork,
            Resource.Raw.klankbeeld__mortar_grenade_shell_002,
            Resource.Raw.reitanna__pinkie_it_s_party_time,
            Resource.Raw.reitanna__vocal_party_favor,
            Resource.Raw.signtoast__big_ben_chimes_in_new_year_1988,
            Resource.Raw.tiny_rocket,
            Resource.Raw.xtrgamr__countdownandcheer_02
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