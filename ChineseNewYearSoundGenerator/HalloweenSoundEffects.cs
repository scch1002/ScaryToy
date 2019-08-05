﻿using System;
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
            Resource.Raw.music1,
            Resource.Raw.huahaoyueyuan_30s,
            Resource.Raw.jinjihesui_30s,
            Resource.Raw.xinchunle_30s,
            Resource.Raw.xinfunian_30s,
            Resource.Raw.yinxiao_170,
            Resource.Raw.yinxiao_276
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