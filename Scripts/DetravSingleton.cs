using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erumpere3D.Scripts
{



    partial class DetravSingleton : Node
    {
        public float[] PlatformSizes = new float[] { 0.5f, 0.75f, 1f, 1.5f, 2f };

        public int PlatformSize
        {
            get => platformSize;
            set
            {
                value = Math.Clamp(value, 0, PlatformSizes.Length - 1);
                platformSize = value;
            }
        }
        public int BallSpeed
        {
            get => ballSpeed;
            set
            {
                value = Math.Clamp(value, 3, 10);
                ballSpeed = value;
            }
        }
        public static DetravSingleton Instance { get; private set; }

        public override void _Ready()
        {
            using var config = new ConfigFile();
            config.Load("user://scores.cfg");
            BestScore = config.GetValue("player", "best_score", 0).AsInt32();



            Instance = this;
            ProcessMode = ProcessModeEnum.Always;

            Levels.Add((1, "res://Scenes/Levels/Level01.tscn"));
            Levels.Add((2, "res://Scenes/Levels/Level02.tscn"));
            Levels.Add((3, "res://Scenes/Levels/Level03.tscn"));
            Levels.Add((4, "res://Scenes/Levels/Level04.tscn"));
            Levels.Add((5, "res://Scenes/Levels/Level05.tscn"));

        }
        private int score;
        private bool paused;
        private bool completeLevel;
        private int ballSpeed = 5;
        private int platformSize = 2;

        public int Score
        {
            get => score;
            set
            {
                if (value < 0) value = 0;
                score = value;
            }
        }
        public List<(int, string)> Levels { get; } = new List<(int, string)>();
        public int BestScore { get; set; }
        public int Balls { get; set; }

        public void Save()
        {
            using var config = new ConfigFile();
            config.Load("user://scores.cfg");
            config.SetValue("player", "best_score", BestScore);
            config.Save("user://scores.cfg");
        }

        public override void _Notification(int what)
        {

            if (what == NotificationWMCloseRequest || what == NotificationWMGoBackRequest)
            {


                var pauseProcessor = GetTree().Root.FindNode<IPauseProcessor>();

                if (pauseProcessor == null)
                {
                    GetTree().Quit();
                }
                else
                {
                    pauseProcessor.QuitRequest();
                }
            }
        }

        public void RestartLevel()
        {
            PlatformSize = 2;
            BallSpeed = 5;
        }
    }
}
