using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Threading.Tasks;

namespace HalloweenSoundGenerator
{
    [Activity(Label = "Halloween Sounds", MainLauncher = true, Theme = "@android:style/Theme.Material.Light")]
    public class MainActivity : Activity
    {
        private const string _repeatEffectsRunning_string = "repeatEffectsRunning";
        private bool _repeatEffectsRunning;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            SetContentView(Resource.Layout.Main);

            var nowButton = FindViewById<Button>(Resource.Id.effect_now);
            var startButton = FindViewById<Button>(Resource.Id.start_stop);

            nowButton.Click += SeoundEffectNow_Click;
            startButton.Click += StartButton_Click;

            if (savedInstanceState != null)
            {
                _repeatEffectsRunning = savedInstanceState.GetBoolean(_repeatEffectsRunning_string);
                SetStateOfStartStopButton(startButton);
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
                startButton.Text = "Start";
                _repeatEffectsRunning = false;
                return;
            }

            StartService(new Intent(this, typeof(SoundEffectService)));
            startButton.Text = "Stop";
            _repeatEffectsRunning = true;
        }

        private async void SeoundEffectNow_Click(object sender, EventArgs e)
        {
            await Task.Run(() => new HalloweenSoundEffects(this).PlaySoundEffect());
        }

        private void SetStateOfStartStopButton(Button startButton)
        {
            if (_repeatEffectsRunning)
            {
                startButton.Text = "Stop";
                return;
            }

            startButton.Text = "Start";
        }
    }
}

