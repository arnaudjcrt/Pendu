using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Pendue
{
    public partial class MainWindow : Window
    {
        private string mot;
        private string hiddenWord;
        private int vie;
        private int erreurs;
        private string difficulty = "Facile";
        private int tempsRestant;
        private DispatcherTimer timer;
        private Random rand = new Random();
        private bool timerStarted = false;

        private MediaPlayer mediaPlayerFond = new MediaPlayer();
        private List<MediaPlayer> effectPlayers = new List<MediaPlayer>();

        // Listes de mots par difficulté
        private string[] motsFaciles = { "chat", "chien", "pomme", "maison", "lampe", "table" };
        private string[] motsMoyens = { "ordinateur", "fenetre", "garage", "voiture", "piscine", "bouteille" };
        private string[] motsDifficiles = { "astronomie", "microprocesseur", "hippopotame", "programmation", "montgolfiere" };

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            erreurs = 0;
            vie = 5;  // Initialisation cohérente
            ResetTimer();

            // Stopper timer précédent si actif
            StopTimer();

            switch (difficulty)
            {
                case "Facile":
                    mot = motsFaciles[rand.Next(motsFaciles.Length)].ToUpper();
                    break;
                case "Moyen":
                    mot = motsMoyens[rand.Next(motsMoyens.Length)].ToUpper();
                    break;
                case "Difficile":
                    mot = motsDifficiles[rand.Next(motsDifficiles.Length)].ToUpper();
                    break;
            }

            hiddenWord = new string('_', mot.Length);
            WordText.Text = string.Join(" ", hiddenWord.ToCharArray());
            StatusText.Text = $"Tentatives restantes : {vie}";
            PenduImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pendu1.png"));

            BuildAlphabetButtons();

            timerStarted = false;
            mediaPlayerFond.Stop();
            TimerText.Text = $"Temps : {tempsRestant}s";
        }

        private void BuildAlphabetButtons()
        {
            AlphabetPanel.Children.Clear();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button btn = new Button
                {
                    Content = c.ToString(),
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(3)
                };
                btn.Click += Letter_Click;
                AlphabetPanel.Children.Add(btn);
            }
        }

        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;

            if (!timerStarted)
            {
                StartTimer();
                timerStarted = true;
                PlaySound("fond");
            }

            char letter = clicked.Content.ToString()[0];
            clicked.IsEnabled = false;

            if (mot.Contains(letter))
            {
                char[] temp = hiddenWord.ToCharArray();
                for (int i = 0; i < mot.Length; i++)
                {
                    if (mot[i] == letter)
                        temp[i] = letter;
                }

                hiddenWord = new string(temp);
                WordText.Text = string.Join(" ", hiddenWord.ToCharArray());

                if (hiddenWord == mot)
                {
                    StatusText.Text = "🎉 Bravo, tu as gagné !";
                    StopTimer();
                    DisableAllButtons();
                    PlaySound("gagne");
                    mediaPlayerFond.Stop();
                    return;
                }
            }
            else
            {
                vie--;
                erreurs++;
                StatusText.Text = $"❌ Mauvais choix ! Tentatives restantes : {vie}";
                UpdateImage();

                if (vie == 0)
                {
                    StatusText.Text = $"😞 Perdu ! Le mot était : {mot}";
                    StopTimer();
                    DisableAllButtons();
                    PlaySound("perd");
                    mediaPlayerFond.Stop();
                    return;
                }
            }
        }

        private void UpdateImage()
        {
            int index = Math.Min(erreurs + 1, 7);
            string packUri = $"pack://application:,,,/Images/Pendu{index}.png";
            PenduImage.Source = new BitmapImage(new Uri(packUri));
        }

        private void PlaySound(string son)
        {
            try
            {
                string chemin = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "son", son + ".mp3");

                if (!File.Exists(chemin))
                {
                    Console.WriteLine($"⚠️ Fichier introuvable : {chemin}");
                    return;
                }

                if (son == "fond")
                {
                    mediaPlayerFond.Open(new Uri(chemin, UriKind.Absolute));
                    mediaPlayerFond.MediaEnded += (s, e) =>
                    {
                        mediaPlayerFond.Position = TimeSpan.Zero;
                        mediaPlayerFond.Play();
                    };
                    mediaPlayerFond.Play();
                }
                else
                {
                    MediaPlayer effectPlayer = new MediaPlayer();
                    effectPlayer.Open(new Uri(chemin, UriKind.Absolute));
                    effectPlayer.Play();
                    effectPlayer.MediaEnded += (s, e) =>
                    {
                        effectPlayer.Close();
                        effectPlayer = null;
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lecture son : " + ex.Message);
            }
        }

        private void DisableAllButtons()
        {
            foreach (Button b in AlphabetPanel.Children.OfType<Button>())
                b.IsEnabled = false;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void ChangeDifficulty_Click(object sender, RoutedEventArgs e)
        {
            if (difficulty == "Facile")
            {
                difficulty = "Moyen";
                DifficultyButton.Content = "Difficulté : Moyen";
            }
            else if (difficulty == "Moyen")
            {
                difficulty = "Difficile";
                DifficultyButton.Content = "Difficulté : Difficile";
            }
            else
            {
                difficulty = "Facile";
                DifficultyButton.Content = "Difficulté : Facile";
            }

            StartNewGame();
        }

        private void StartTimer()
        {
            // Stopper timer existant avant d'en créer un nouveau
            StopTimer();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void ResetTimer()
        {
            switch (difficulty)
            {
                case "Facile": tempsRestant = 60; break;
                case "Moyen": tempsRestant = 45; break;
                case "Difficile": tempsRestant = 30; break;
            }
            TimerText.Text = $"Temps : {tempsRestant}s";
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer = null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tempsRestant--;
            TimerText.Text = $"Temps : {tempsRestant}s";

            if (tempsRestant <= 0)
            {
                StopTimer();
                StatusText.Text = $"⏰ Temps écoulé ! Le mot était : {mot}";
                DisableAllButtons();
                PlaySound("perd");
                mediaPlayerFond.Stop();
            }
        }
    }
}
