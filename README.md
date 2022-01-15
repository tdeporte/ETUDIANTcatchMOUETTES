# ETUDIANTScatchMOUETTES

## Scénario

Simulation de bizutage de 1ere année de DUT Génie civil Développement durable de La Rochelle:

Durant leur seconde semaine en DUT GCDD, les étudiants de première année sont amenés à réaliser divers activités ludiques,
Une de ces activités est de tenter de capturer une mouette, animal symbole de La Rochelle notre ville bien aimée.
Cette simulation se situe à la place de la Grosse Horloge, La Rochelle.

### Simulation automatique:

Durant la simulation un essaim de mouette devra automatiquement ramasser les miettes de pain sur le sol. 
Lorsqu'un étudiant repère une mouette à portée il se dirigera vers elle pour la capturer,
s'il touche la mouette, elle sera considérée comme capturée et disparait.

## Description du projet

### Scenes

#### Menu principal
Le menu principal contient 3 boutons menant à la simulation, au menu des options ou permettant de quitter l'application.

#### Options
Le menu des options contient 3 sliders permettant de configurer le nombre d'entités présentes dans la simulation.

#### Scène de jeu
La scène principale met en scène la simulation, un compteur affiche le nombre de mouettes et de miettes restantes et l'utilisateur peut déplacer la caméra à l'aidedes touches ZQSD, A pour monter en altitude et E pour descendre.

#### Ecran de fin
La scène de fin contient un bouton pour charger à nouveau la simulation et un bouton menant au menu principal.

### Acteurs

#### Etudiant
Les étudiants se déplacent et se tournent à intervalles variables, si une mouette entre dans leur champ de vision ils gagneront en vitesse et se dirigeront vers elle. Les étudiants restent dans le périmètre de jeu en évitant les murs qui le définnissent mais peuvent tomber dans la mer, c'est aussi une simulation de prévention !

#### Mouette
A leur apparition les mouettes repèrent la miette la plus proche et se dirigent vers elle pour la manger, une fois au sol elle mange la miette une mouette y reste pour un cours temps aléatoire. Elle s'envole ensuite pour un temps aléatoire puis cherche la miette la plus proche à nouveau.

#### Miette
Les miettes apparaissent réparties aléatoirement sur le terrain et disparraissent au contact d'une mouette.

## Sources:
Script de caméra : https://forum.unity.com/threads/fly-cam-simple-cam-script.67042/
Prefab de l'Etudiant : https://assetstore.unity.com/packages/3d/characters/distant-lands-free-characters-178123
