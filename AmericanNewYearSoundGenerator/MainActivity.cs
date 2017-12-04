using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Views;
using System.Linq;
using Android.Runtime;
using Android.Support.V4.Content;

namespace HalloweenSoundGenerator
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        private const string _repeatEffectsRunning_string = "repeatEffectsRunning";
        private bool _repeatEffectsRunning;
        private HalloweenSoundEffects _halloweenSoundEffects;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            SetContentView(Resource.Layout.Main);

            var nowButton = FindViewById<Button>(Resource.Id.effect_now);
            var startButton = FindViewById<Button>(Resource.Id.start_stop);

            startButton.Click += StartButton_Click;
            nowButton.Touch += NowButton_Touch;

            _repeatEffectsRunning = IsSoundEffectServiceRunning();

            if (savedInstanceState != null)
            {
                if (!_repeatEffectsRunning)
                {
                    _repeatEffectsRunning = savedInstanceState.GetBoolean(_repeatEffectsRunning_string);
                }
            }

            SetStateOfStartStopButton(startButton);

            _halloweenSoundEffects = new HalloweenSoundEffects(this);
        }

        private void NowButton_Touch(object sender, View.TouchEventArgs e)
        {
            var nowButton = FindViewById<Button>(Resource.Id.effect_now);

            if (e.Event.Action == MotionEventActions.Down)
            {
                SetPressedState(nowButton);
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                SetRegularState(nowButton);
                Task.Run(() => _halloweenSoundEffects.PlaySoundEffect());
            }
        }

        protected override void OnSaveInstanceState(Bundle savedInstanceState)
        {
            savedInstanceState.PutBoolean(_repeatEffectsRunning_string, _repeatEffectsRunning);

            base.OnSaveInstanceState(savedInstanceState);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var startButton = FindViewById<Button>(Resource.Id.start_stop);

            if (_repeatEffectsRunning)
            {
                StopAutoPlay();
                startButton.Text = "Start Auto Play";
                SetRegularState(startButton);
                _repeatEffectsRunning = false;
                return;
            }

            StartAutoPlay();
            startButton.Text = "Stop Auto Play";
            SetPressedState(startButton);
            Toast.MakeText(this, "New Years sounds will play in the background, once every 3 minutes.", ToastLength.Long).Show();
            _repeatEffectsRunning = true;
        }

        private void SetStateOfStartStopButton(Button startButton)
        {
            if (_repeatEffectsRunning)
            {
                startButton.Text = "Stop Auto Play";
                SetPressedState(startButton);
                return;
            }

            startButton.Text = "Start Auto Play";
            SetRegularState(startButton);
        }

        private void SetPressedState(Button button)
        {
            button.SetTextColor(Color.ParseColor("#F02036"));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButtonPressed);
        }

        private void SetRegularState(Button button)
        {
            button.SetTextColor(Color.ParseColor("#FEB242"));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButton);
        }

        private bool IsSoundEffectServiceRunning()
        {
            return (PendingIntent.GetBroadcast(this, 0,
                new Intent(this, typeof(SoundEffectReceiver)),
                PendingIntentFlags.NoCreate) != null);
        }

        private void StartAutoPlay()
        {
            var alarmIntent = new Intent(this, typeof(SoundEffectReceiver));
            var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var interval = Convert.ToInt64(new TimeSpan(0, 3, 0).TotalMilliseconds);
            var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();

            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, 0, interval, pending);
        }

        private void StopAutoPlay()
        {
            var alarmIntent = new Intent(this, typeof(SoundEffectReceiver));
            var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();

            alarmManager.Cancel(pending);
            pending.Cancel();
        }
    }
}

