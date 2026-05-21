# V-Evolution 🦠

[cite_start]**V-Evolution** est un jeu mobile casual d'action de type *Beat 'em up* / *Roguelite Survival* développé sous Unity[cite: 1, 2, 20]. [cite_start]Le joueur y incarne un virus traversant les âges qui doit lutter pour sa survie face à d'innombrables vagues d'ennemis cherchant à l'éradiquer.

> [cite_start]*"Survivez, combattez, évoluez."* [cite: 3]

[cite_start]Ce projet a été réalisé dans le cadre du **Projet Immersion - Madbox** (ING3 - Visual Computing - 2022/2023)[cite: 1].

---

## 🎮 Concept & Gameplay

### Pitch
[cite_start]Vous êtes un virus en constante évolution[cite: 2, 19]. [cite_start]Votre objectif est de résister et de nettoyer des niveaux successifs composés chacun d'environ **5 vagues d'ennemis**[cite: 3, 6]. 

### Boucle de Gameplay (Loop)
* [cite_start]**Boucle principale :** Se déplacer et survivre aux vagues successives[cite: 3, 21]. [cite_start]L'attaque du joueur sur les ennemis proches est entièrement automatique[cite: 10].
* [cite_start]**Boucle secondaire (Progression) :** Les ennemis éliminés lâchent de la monnaie[cite: 17]. [cite_start]Entre chaque vague, une phase de transition ("Market") permet au joueur d'utiliser cette monnaie pour acheter des améliorations temporaires[cite: 16, 17, 21].
* [cite_start]**Conditions de victoire/défaite :** Éliminer toutes les vagues pour passer au niveau suivant[cite: 3, 21]. [cite_start]En cas de mort, le joueur recommence le niveau du début ou dispose d'une option pour relancer la vague actuelle[cite: 7, 21].

### Statistiques du Joueur Améliorables
* [cite_start]Points de vie (Life) [cite: 11, 18]
* [cite_start]Dégâts d'attaque (Attack Damage) [cite: 11, 18]
* [cite_start]Vitesse d'attaque (Attack Speed) [cite: 11]
* [cite_start]Portée d'attaque (Attack Range) [cite: 11]
* [cite_start]Vitesse de déplacement (Move Speed) [cite: 11, 18]

---

## 👾 Bestiaire (Types d'Ennemis)

[cite_start]Les vagues sont composées de trois grandes catégories d'ennemis aux comportements distincts qui foncent vers le joueur[cite: 11, 20]:

1.  [cite_start]**DPS :** Vitesse de déplacement et d'attaque élevée, mais très peu de points de vie[cite: 20].
2.  [cite_start]**RANGED :** Attaquent à distance en lançant des projectiles, vitesse de déplacement lente et points de vie faibles[cite: 20].
3.  [cite_start]**TANK :** Très hauts points de vie et dégâts d'attaque élevés, mais se déplacent lentement[cite: 20].

---

## 🛠️ Spécifications Techniques & Contrôles

* [cite_start]**Moteur de jeu :** Unity [cite: 20]
* [cite_start]**Plateforme cible :** Android (Smartphone) [cite: 2, 19]
* [cite_start]**Orientation :** Mode Portrait [cite: 19]
* [cite_start]**Caméra :** Vue 2D à la 3e personne, caméra fixe centrée sur le personnage [cite: 19]
* [cite_start]**Contrôles :** Joystick virtuel sur l'écran tactile pour déplacer le virus dans toutes les directions[cite: 10, 19].
* [cite_start]**Assets graphiques (Mesh/2D) :** Récupérés sur l'Asset Store d'Unity et Itch.io (Coût global du projet : 3$)[cite: 20].
* [cite_start]**Inspirations majeures :** *Survivor.io*, *Z Defense*, *Plague Inc.* [cite: 19, 20]

---

## 🚀 Défis Rencontrés & Apprentissages

[cite_start]Dans le cadre du développement de cette version démo, l'équipe a fait face à plusieurs défis techniques majeurs[cite: 1]:
* [cite_start]**Différences d'environnement (Émulateur vs Réel) :** L'adaptation des éléments de l'UI (notamment la sensibilité et le positionnement du Joystick tactile) entre l'éditeur Unity et le comportement réel sur divers formats d'écrans de smartphones[cite: 1].
* [cite_start]**Gestion de la portée du projet :** Le game design initial s'est révélé très ambitieux vis-à-vis des contraintes de temps imparties, menant à des arbitrages (comme la simplification du système d'évolution à une seule devise) pour garantir une démo stable et jouable[cite: 16, 17, 1].

---

## 👥 Équipe du Projet (Crédits)

[cite_start]Projet développé avec passion par[cite: 1]:
* [cite_start]**LARREGLE Théo** [cite: 1]
* [cite_start]**PENARANDA Antoine** [cite: 1]
* [cite_start]**PRIJAC Malo** [cite: 1]
* [cite_start]**XIONG Jian** [cite: 1]
