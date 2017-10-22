using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Views;

namespace HalloweenSoundGenerator
{
    [Activity(Label = "Halloween Sounds", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@android:style/Theme.Material.Light")]
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

            nowButton.Click += SeoundEffectNow_Click;
            startButton.Click += StartButton_Click;

            nowButton.Touch += NowButton_Touch;

            if (savedInstanceState != null)
            {
                _repeatEffectsRunning = savedInstanceState.GetBoolean(_repeatEffectsRunning_string);
                SetStateOfStartStopButton(startButton);
            }

            _halloweenSoundEffects = new HalloweenSoundEffects(this);
        }

        private void NowButton_Touch(object sender, Android.Views.View.TouchEventArgs e)
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
                StopService(new Intent(this, typeof(SoundEffectService)));
                startButton.Text = "Start Auto Play";
                SetRegularState(startButton);
                _repeatEffectsRunning = false;
                return;
            }

            StartService(new Intent(this, typeof(SoundEffectService)));
            startButton.Text = "Stop Auto Play";
            SetPressedState(startButton);
            Toast.MakeText(this, "Horror sounds will play in the background.", ToastLength.Long).Show();
            _repeatEffectsRunning = true;
        }

        private async void SeoundEffectNow_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _halloweenSoundEffects.PlaySoundEffect());
        }

        private void SetStateOfStartStopButton(Button startButton)
        {
            if (_repeatEffectsRunning)
            {
                startButton.Text = "Stop Auto Play";
                return;
            }

            startButton.Text = "Start Auto Play";
        }

        private void SetPressedState(Button button)
        {
            button.SetTextColor(new Color(GetColor(Android.Resource.Color.HoloOrangeDark)));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButtonPressed);
        }

        private void SetRegularState(Button button)
        {
            button.SetTextColor(new Color(GetColor(Android.Resource.Color.Black)));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButton);
        }
    }
}

