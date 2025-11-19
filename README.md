<div align="center">
<!-- Carte principale en thème sombre -->
<div style="max-width: 900px; margin: 30px auto; background-color: #0d1117; color: #e6edf3; padding: 32px; border-radius: 18px; border: 1px solid #30363d; box-shadow: 0 0 25px rgba(0,0,0,0.6);">
<!-- Logo perso du Pendu -->
<img src="https://cdn-icons-png.flaticon.com/512/686/686379.png" alt="Logo Pendu" width="120" style="margin-bottom: 18px;">
<h1 style="margin: 0; font-size: 2.4rem;">Le Pendu – Version Graphique C#</h1>
<p style="margin-top: 10px; font-size: 1.05rem; opacity: 0.9;">
     Jeu du pendu moderne avec interface graphique, niveaux de difficulté et effets sonores.
</p>
<hr style="border: 0; border-top: 1px solid #30363d; margin: 24px 0;">
<!-- Présentation -->
<h2 style="margin-top: 0;">🎮 Présentation</h2>
<p style="text-align: left;">
     Ce projet est une adaptation du célèbre <strong>jeu du Pendu</strong>, développée en <strong>C#</strong> avec une
<strong>interface graphique</strong>. Le joueur doit deviner un mot secret avant que le dessin du pendu ne soit
     entièrement complété.
</p>
<p style="text-align: left;">
     Le but de ce projet est de progresser en programmation C#, en conception d’interface utilisateur et en logique de
     jeu tout en proposant une expérience amusante et moderne.
</p>
<!-- Règles détaillées -->
<h2>📜 Règles du jeu</h2>
<h3 style="text-align: left; margin-bottom: 4px;">1️⃣ Début de la partie</h3>
<ul style="text-align: left;">
<li>Le joueur lance le jeu et choisit un <strong>niveau de difficulté</strong> (Facile, Moyen ou Difficile).</li>
<li>Un <strong>mot secret</strong> est sélectionné aléatoirement dans une liste interne.</li>
<li>Le pendu est vide : aucune partie du dessin n’est encore affichée.</li>
</ul>
<h3 style="text-align: left; margin-bottom: 4px;">2️⃣ Tour de jeu</h3>
<ul style="text-align: left;">
<li>Le joueur propose une lettre (via des boutons ou via l’interface graphique).</li>
<li>Si la lettre se trouve dans le mot :
<ul>
<li>Elle apparaît à toutes les positions correspondantes dans le mot affiché.</li>
</ul>
</li>
<li>Si la lettre ne se trouve pas dans le mot :
<ul>
<li>Une nouvelle partie du <strong>dessin du pendu</strong> est ajoutée (tête, corps, bras, jambes, etc.).</li>
</ul>
</li>
<li>Les lettres déjà essayées ne peuvent plus être rejouées, afin d’éviter les répétitions inutiles.</li>
</ul>
<h3 style="text-align: left; margin-bottom: 4px;">3️⃣ Niveaux de difficulté</h3>
<ul style="text-align: left;">
<li><strong>Facile :</strong> mots simples, nombre d’erreurs autorisées plus élevé.</li>
<li><strong>Moyen :</strong> mots un peu plus longs ou variés, erreurs limitées.</li>
<li><strong>Difficile :</strong> mots plus complexes, peu d’erreurs possibles, tension maximale.</li>
</ul>
<h3 style="text-align: left; margin-bottom: 4px;">4️⃣ Fin de la partie</h3>
<ul style="text-align: left;">
<li><strong>Victoire :</strong> le joueur a découvert toutes les lettres du mot avant que le dessin du pendu ne soit complet.</li>
<li><strong>Défaite :</strong> toutes les tentatives ont été utilisées, le pendu est entièrement dessiné.</li>
<li>Un message d’information apparaît pour indiquer si la partie est gagnée ou perdue, et il est possible de relancer immédiatement une nouvelle partie.</li>
</ul>
<!-- Fonctionnalités -->
<h2>🛠️ Fonctionnalités</h2>
<div style="display: flex; flex-wrap: wrap; justify-content: center; gap: 18px; margin-bottom: 10px;">
<div style="flex: 1 1 260px; min-width: 240px; background-color: #161b22; border-radius: 12px; padding: 14px 16px; border: 1px solid #30363d;">
<img src="https://cdn-icons-png.flaticon.com/512/2920/2920238.png" alt="Interface" width="60">
<h3>Interface graphique soignée</h3>
<p style="font-size: 0.95rem;">
         Fenêtre claire, boutons lisibles, affichage visuel du mot et du pendu. L’interface a été pensée
         pour être simple à comprendre, même pour un joueur débutant.
</p>
</div>
<div style="flex: 1 1 260px; min-width: 240px; background-color: #161b22; border-radius: 12px; padding: 14px 16px; border: 1px solid #30363d;">
<img src="https://cdn-icons-png.flaticon.com/512/4370/4370591.png" alt="Difficulté" width="60">
<h3>Niveaux de difficulté</h3>
<p style="font-size: 0.95rem;">
         Trois niveaux (Facile, Moyen, Difficile) modifiant le nombre d’erreurs autorisées et parfois la
         complexité des mots. Idéal pour s’adapter au niveau de chacun.
</p>
</div>
<div style="flex: 1 1 260px; min-width: 240px; background-color: #161b22; border-radius: 12px; padding: 14px 16px; border: 1px solid #30363d;">
<img src="https://cdn-icons-png.flaticon.com/512/727/727245.png" alt="Son" width="60">
<h3>Effets sonores</h3>
<p style="font-size: 0.95rem;">
         Sons intégrés pour accompagner les bonnes réponses, les erreurs et la fin de partie. Ils rendent
         l’expérience plus vivante et immersive.
</p>
</div>
<div style="flex: 1 1 260px; min-width: 240px; background-color: #161b22; border-radius: 12px; padding: 14px 16px; border: 1px solid #30363d;">
<img src="https://cdn-icons-png.flaticon.com/512/545/545680.png" alt="Rejouer" width="60">
<h3>Rejouabilité immédiate</h3>
<p style="font-size: 0.95rem;">
         Après chaque partie, une nouvelle peut être lancée sans quitter l’application. Le mot change, ce
         qui permet de rejouer autant de fois que l’on veut.
</p>
</div>
</div>
<!-- Avis famille -->
<h2>⭐ Avis de ma famille</h2>
<div style="text-align: left; margin-top: 10px;">
<h3>👨 Mon père</h3>
<p style="font-style: italic; font-size: 0.98rem;">
       « J’ai vraiment aimé ton jeu, et ta mère aussi d’ailleurs ! C’est super bien fait, l’interface est claire
       et on voit que tu as passé du temps sur le code. »
</p>
<h3>👩 Ma mère</h3>
<p style="font-style: italic; font-size: 0.98rem;">
       « Le niveau difficile… il était vraiment dur ! Je n’ai réussi qu’au bout de la sixième tentative.
       Mais justement, c’est ça qui m’a plu : le jeu donne envie de recommencer. »
</p>
<h3>👧 Ma sœur</h3>
<p style="font-style: italic; font-size: 0.98rem;">
       « J’adore l’interface graphique ! Comment t’as fait pour mettre tout ça en code ? C’est trop stylé de
       voir le pendu se dessiner comme ça, on dirait un vrai jeu pro. »
</p>
</div>
<!-- Installation (lien à modifier) -->
<h2>📥 Installation</h2>
<p style="text-align: left;">
     Le projet est disponible sur GitHub à cette adresse (tu pourras modifier le lien avec le tien) :
</p>
<p style="text-align: left; font-weight: bold;">
     👉 <a href="https://github.com/TON-PSEUDO/TON-REPO" style="color: #58a6ff;">https://github.com/arnaudjcrt/Pendu.git</a>
</p>
<p style="text-align: left;">
     Il suffit de récupérer le projet, de l’ouvrir dans <strong>Visual Studio</strong>, puis de lancer l’exécution
     pour jouer au Pendu.
</p>
<!-- Auteur -->
<h2>👤 Auteur</h2>
<p>
<strong>Arnaud</strong><br>
     Étudiant passionné par la programmation, le C#, les interfaces graphiques et les projets créatifs.
</p>
</div>
</div>
