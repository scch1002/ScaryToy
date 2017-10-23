using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Views;
using System.Linq;

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
                StopService(new Intent(this, typeof(SoundEffectService)));
                startButton.Text = "Start Auto Play";
                SetRegularState(startButton);
                _repeatEffectsRunning = false;
                return;
            }

            StartService(new Intent(this, typeof(SoundEffectService)));
            startButton.Text = "Stop Auto Play";
            SetPressedState(startButton);
            Toast.MakeText(this, "Halloween sounds will play in the background, once every 1 to 5 minutes.", ToastLength.Long).Show();
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
            button.SetTextColor(new Color(GetColor(Android.Resource.Color.HoloOrangeDark)));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButtonPressed);
        }

        private void SetRegularState(Button button)
        {
            button.SetTextColor(new Color(GetColor(Android.Resource.Color.Black)));
            button.SetBackgroundResource(Resource.Drawable.HalloweenButton);
        }

        private bool IsSoundEffectServiceRunning()
        {
            var manager = (ActivityManager)GetSystemService(ActivityService);

            return manager.GetRunningServices(int.MaxValue)
                .Any(service => service.Service.ClassName
                    .Equals("cseu.SoundEffectService", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

