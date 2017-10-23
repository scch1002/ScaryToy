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
using System.Threading;
using Android.Media;

namespace HalloweenSoundGenerator
{
    [Service(Name = "cseu.SoundEffectService")]
    public class SoundEffectService : Service
    {
        private Random _random = new Random();
        private Timer _soundEffectTimer;
        private HalloweenSoundEffects _halloweenSoundEffects;

        public SoundEffectService()
        {
            _halloweenSoundEffects = new HalloweenSoundEffects(this);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            StartSoundEffects();

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            _soundEffectTimer.Dispose();
            base.OnDestroy();
        }
    
        private void StartSoundEffects()
        {
            _soundEffectTimer = new Timer(PlaySoundEffect, null, new TimeSpan(0, _random.Next(1, 5), 0), new TimeSpan(0, 0, 0));
        }

        private void PlaySoundEffect(Object status)
        {
            _halloweenSoundEffects.PlaySoundEffect();
            //_soundEffectTimer.Change(new TimeSpan(0, 0, _random.Next(1, 5)), new TimeSpan(0, 0, 0));
            _soundEffectTimer.Change(new TimeSpan(0, _random.Next(1, 5), 0), new TimeSpan(0, 0, 0));
        }
    }
}