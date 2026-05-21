# V-Evolution

> **Un jeu mobile casual d'action - Beat 'em up / Roguelite Survival**

Un jeu vidéo développé avec **Unity** et **C#**.

---

## 📋 Table des matières

- [À propos](#-à-propos)
- [Démarrage rapide (Jouer au jeu)](#-démarrage-rapide-jouer-au-jeu)
- [Installation (Développement)](#-installation-développement)
- [Utilisation](#-utilisation)
- [Concept & Gameplay](#-concept--gameplay)
- [Bestiaire](#-bestiaire)
- [Spécifications Techniques](#-spécifications-techniques)
- [Défis Rencontrés](#-défis-rencontrés)
- [Ressources](#-ressources)

---

## 🎮 À propos

**V-Evolution** est une démonstration de jeu vidéo casual d'action créée avec le moteur **Unity**.

Vous êtes un virus en constante évolution. Survivez à des vagues d'ennemis successives, collectez des ressources et améliorez vos statistiques pour progresser dans le jeu.

> *"Survivez, combattez, évoluez."*

Ce projet a été réalisé dans le cadre du **Projet Immersion - Madbox** (ING3 - Visual Computing - 2022/2023).

### Technologies utilisées

- **Engine** : Unity
- **Langage** : C#
- **Plateforme cible** : Android (Smartphone)

---

## 🚀 Démarrage rapide (Jouer au jeu)

Si vous voulez simplement **jouer au jeu** sans avoir besoin d'installer Unity :

1. **Téléchargez la version compilée**
   - Allez sur la page [**Releases**](https://github.com/Malo-Prijac/V-Evolution/releases)
   - Téléchargez la dernière version (fichier `.apk` pour Android)

2. **Installez le jeu sur votre appareil Android**
   - Transférez le fichier `.apk` sur votre smartphone
   - Ouvrez le fichier et suivez les instructions d'installation

3. **Lancez le jeu**
   - Tapez sur l'icône de l'application pour commencer à jouer !

---

## 📦 Installation (Développement)

### Pour les développeurs qui veulent modifier le projet

#### Prérequis

- **Unity** (version compatible avec le projet)
- **Git**

#### Étapes

Vous avez deux options pour installer le projet :

##### Option 1 : Télécharger le Unity Package

1. **Téléchargez le Unity Package**
   - Allez sur la page [**Releases**](https://github.com/Malo-Prijac/V-Evolution/releases)
   - Téléchargez le fichier `.unitypackage`

2. **Importer le package dans Unity**
   - Ouvrez ou créez un projet Unity
   - Allez sur **Assets** → **Import Package** → **Custom Package**
   - Sélectionnez le fichier `.unitypackage` téléchargé
   - Cliquez sur "Import"

3. **Charger la scène principale**
   - Dans l'éditeur Unity, naviguez vers la scène principale
   - Cliquez sur "Play" pour lancer le jeu

##### Option 2 : Cloner le repository (Peut-être quelques bugs sur cette version)

1. **Cloner le repository**
   ```bash
   git clone git@github.com:Malo-Prijac/V-Evolution.git
   cd V-Evolution
   ```

2. **Ouvrir le projet dans Unity**
   - Lancez Unity Hub
   - Cliquez sur "Open Project"
   - Sélectionnez le dossier du projet

3. **Attendre le chargement**
   - Unity va télécharger et compiler les assets (peut prendre quelques minutes)

4. **Charger la scène principale**
   - Dans l'éditeur Unity, naviguez vers la scène principale
   - Cliquez sur "Play" pour lancer le jeu

---

## 🎮 Utilisation

### Pour jouer en développement

Pour jouer ou tester le jeu directement dans Unity :

1. Ouvrez le projet dans Unity
2. Appuyez sur le bouton **Play** dans l'éditeur
3. Explorez et testez les mécaniques du jeu

### Pour créer votre propre Build

Si vous voulez créer une version compilée pour Android :

1. Dans Unity, allez sur **File** → **Build Settings**
2. Sélectionnez **Android** comme plateforme cible
3. Cliquez sur **Build** ou **Build and Run**
4. Choisissez un dossier de destination
5. Attendez que Unity compile le jeu en `.apk`

---

## 🎮 Concept & Gameplay

### Pitch

Vous êtes un virus en constante évolution. Votre objectif est de résister et de nettoyer des niveaux successifs composés chacun d'environ **5 vagues d'ennemis variés**. Entre chaque vague, utilisez l'argent récolté pour améliorer vos statistiques et progresser dans le jeu.

### Boucle de Gameplay (Loop)

* **Boucle principale :** Se déplacer et survivre aux vagues successives. L'attaque du joueur sur les ennemis proches est entièrement automatique.
* **Boucle secondaire (Progression) :** Les ennemis éliminés lâchent de la monnaie. Entre chaque vague, une phase de transition ("Market") permet au joueur d'utiliser ses ressources pour s'améliorer.
* **Conditions de victoire/défaite :** Éliminer toutes les vagues pour passer au niveau suivant. En cas de mort, le joueur recommence le niveau du début ou peut utiliser des vies supplémentaires.

### Statistiques du Joueur Améliorables

* Points de vie (Life)
* Dégâts d'attaque (Attack Damage)
* Vitesse d'attaque (Attack Speed)
* Portée d'attaque (Attack Range)
* Vitesse de déplacement (Move Speed)

---

## 👾 Bestiaire

Les vagues sont composées de trois grandes catégories d'ennemis aux comportements distincts qui foncent vers le joueur :

1. **DPS :** Vitesse de déplacement et d'attaque élevée, mais très peu de points de vie.
2. **RANGED :** Attaquent à distance en lançant des projectiles, vitesse de déplacement lente et points de vie faibles.
3. **TANK :** Très hauts points de vie et dégâts d'attaque élevés, mais se déplacent lentement.

---

## 🛠️ Spécifications Techniques

* **Moteur de jeu :** Unity
* **Plateforme cible :** Android (Smartphone)
* **Orientation :** Mode Portrait
* **Caméra :** Vue 2D à la 3e personne, caméra fixe centrée sur le personnage
* **Contrôles :** Joystick virtuel sur l'écran tactile pour déplacer le virus dans toutes les directions
* **Assets graphiques :** Récupérés sur l'Asset Store d'Unity et Itch.io (Coût global : 3$)
* **Inspirations majeures :** *Survivor.io*, *Z Defense*, *Plague Inc.*

---

## 🚀 Défis Rencontrés

* **Différences d'environnement (Émulateur vs Réel) :** L'adaptation des éléments de l'UI (notamment la sensibilité et le positionnement du Joystick tactile) entre l'éditeur Unity et les appareils réels.
* **Gestion de la portée du projet :** Le game design initial s'est révélé très ambitieux vis-à-vis des contraintes de temps imparties, menant à des arbitrages dans la conception.

---

## 📁 Structure du projet

### Dossiers principaux

- **Assets/** : Contient tous les assets du jeu
  - Scripts C#
  - Sprites et textures
  - Modèles 3D
  - Sons et musiques
  - Scènes

- **ProjectSettings/** : Paramètres de configuration d'Unity

---

## 📚 Ressources

- 📄 [V-Evolution_Soutenance_finale.pdf](./V-Evolution_Soutenance_finale.pdf) - Documentation complète
- 🎮 [Releases](https://github.com/Malo-Prijac/V-Evolution/releases) - Télécharger le jeu compilé

---

## 👥 Auteurs

**Théo Larregle**
**Antoine Penaranda**
**Malo Prijac**
**Jian Xiong**

---
