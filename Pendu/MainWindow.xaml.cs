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
        // --- VARIABLES PRINCIPALES DU JEU ---
        private string mot;                  // Le mot à deviner
        private string hiddenWord;           // Le mot masqué avec des '_'
        private int vie;                     // Nombre de vies restantes
        private int erreurs;                 // Nombre d'erreurs commises
        private string difficulty = "Facile";// Niveau de difficulté actuel
        private int tempsRestant;            // Temps restant pour la partie
        private DispatcherTimer timer;       // Timer pour le décompte du temps
        private Random rand = new Random();  // Générateur de nombres aléatoires
        private bool timerStarted = false;   // Indique si le timer a commencé

        private MediaPlayer mediaPlayerFond = new MediaPlayer(); // Lecteur audio pour le fond sonore

        // --- LISTES DE MOTS PAR DIFFICULTÉ ---
        private string[] motsFaciles = { "chat", "chien", "pomme", "maison", "lampe", "table" };
        private string[] motsMoyens = { "ordinateur", "fenetre", "garage", "voiture", "piscine", "bouteille" };
        private string[] motsDifficiles = { "astronomie", "microprocesseur", "hippopotame", "programmation", "montgolfiere" };

        public MainWindow()
        {
            InitializeComponent();  // Initialise l'interface graphique (WPF)
            StartNewGame();         // Lance une nouvelle partie au démarrage
        }

        // --- DÉMARRE UNE NOUVELLE PARTIE ---
        private void StartNewGame()
        {
            erreurs = 0;            // Remise à zéro des erreurs
            vie = 6;                // Le joueur a 6 tentatives
            ResetTimer();           // Réinitialise le chronomètre
            StopTimer();            // Stoppe tout ancien timer encore actif

            // Sélection d’un mot aléatoire selon la difficulté
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

            // Création du mot caché (ex: "CHAT" → "____")
            hiddenWord = new string('_', mot.Length);

            // Mise à jour de l’interface graphique
            WordText.Text = string.Join(" ", hiddenWord.ToCharArray());   // Affiche les lettres cachées séparées
            StatusText.Text = $"Tentatives restantes : {vie}";            // Affiche les vies
            PenduImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pendu1.png")); // Image initiale

            BuildAlphabetButtons();  // Construit les boutons A-Z

            timerStarted = false;    // Le timer ne démarre qu’à la première lettre
            mediaPlayerFond.Stop();  // Stoppe la musique de fond
            TimerText.Text = $"Temps : {tempsRestant}s"; // Affiche le temps initial
        }

        // --- CRÉE LES BOUTONS DE L’ALPHABET ---
        private void BuildAlphabetButtons()
        {
            AlphabetPanel.Children.Clear(); // Efface d’éventuels anciens boutons

            for (char c = 'A'; c <= 'Z'; c++) // Boucle de A à Z
            {
                Button btn = new Button
                {
                    Content = c.ToString(),   // Affiche la lettre
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(3)
                };

                btn.Click += Letter_Click;     // Associe l’événement clic
                AlphabetPanel.Children.Add(btn); // Ajoute le bouton à l’interface
            }
        }

        // --- QUAND LE JOUEUR CLIQUE SUR UNE LETTRE ---
        private void Letter_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;

            // Si le timer n’a pas encore commencé, on le démarre
            if (!timerStarted)
            {
                StartTimer();
                timerStarted = true;
                PlaySound("fond"); // Lance le son de fond
            }

            char letter = clicked.Content.ToString()[0]; // Récupère la lettre du bouton
            clicked.IsEnabled = false;                   // Désactive le bouton

            // Si la lettre est dans le mot
            if (mot.Contains(letter))
            {
                char[] temp = hiddenWord.ToCharArray(); // Convertit le mot caché en tableau de caractères
                for (int i = 0; i < mot.Length; i++)
                {
                    if (mot[i] == letter)
                        temp[i] = letter;               // Révèle la lettre correcte
                }

                hiddenWord = new string(temp);           // Met à jour le mot caché
                WordText.Text = string.Join(" ", hiddenWord.ToCharArray());

                // Si le mot est complètement trouvé
                if (hiddenWord == mot)
                {
                    StatusText.Text = "🎉 Bravo, tu as gagné !"; // Message victoire
                    StopTimer();                                // Stop le timer
                    DisableAllButtons();                        // Bloque les autres lettres
                    PlaySound("gagne");                         // Joue le son de victoire
                    mediaPlayerFond.Stop();                     // Stop la musique de fond
                    return;
                }
            }
            else
            {
                // Mauvaise lettre : on retire une vie
                vie--;
                erreurs++;
                StatusText.Text = $"❌ Mauvais choix ! Tentatives restantes : {vie}";
                UpdateImage(); // Met à jour l’image du pendu

                // Si le joueur a perdu
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

        // --- MET À JOUR L’IMAGE DU PENDU SELON LES ERREURS ---
        private void UpdateImage()
        {
            int index = Math.Min(erreurs + 1, 7); // Évite de dépasser l’image finale
            string packUri = $"pack://application:,,,/Images/Pendu{index}.png";
            PenduImage.Source = new BitmapImage(new Uri(packUri)); // Affiche la nouvelle image
        }

        // --- JOUE UN SON (victoire, échec, fond, etc.) ---
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
                else
                {
                    MediaPlayer effectPlayer = new MediaPlayer();
                    effectPlayer.Open(new Uri(chemin, UriKind.Absolute)); // Ouvre le fichier son
                    effectPlayer.Play();                                  // Joue le son

                    // Libère le lecteur une fois le son terminé
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

        // --- DÉSACTIVE TOUS LES BOUTONS DE L’ALPHABET ---
        private void DisableAllButtons()
        {
            foreach (Button b in AlphabetPanel.Children.OfType<Button>())
                b.IsEnabled = false;
        }

        // --- RECOMMENCE UNE NOUVELLE PARTIE ---
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        // --- CHANGE LA DIFFICULTÉ DU JEU ---
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

            StartNewGame(); // Redémarre avec la nouvelle difficulté
        }

        // --- DÉMARRE LE CHRONOMÈTRE ---
        private void StartTimer()
        {
            StopTimer(); // Évite les doublons de timer

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Défilement toutes les secondes
            timer.Tick += Timer_Tick;                 // Action à chaque "tick"
            timer.Start();
        }

        // --- RÉINITIALISE LE TEMPS SELON LA DIFFICULTÉ ---
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

        // --- ARRÊTE LE CHRONOMÈTRE ---
        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick; // Désabonne l'événement
                timer = null;
            }
        }

        // --- GÈRE CHAQUE "TICK" DU TIMER (chaque seconde) ---
        private void Timer_Tick(object sender, EventArgs e)
        {
            tempsRestant--; // Décrémente le temps
            TimerText.Text = $"Temps : {tempsRestant}s";

            // Si le temps est écoulé, partie perdue
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
