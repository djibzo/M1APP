# Cour du 17 Mai 2025

## Résumé de la journée

Aujourd'hui, nous avons travaillé sur plusieurs aspects du développement backend pour une API. Voici les points principaux abordés :

1. **Création et mise à jour des modèles** :
   - Nous avons défini les classes `CreateRequest` et `UpdateRequest` pour les entités `User` et `Flotte`.
   - Ces classes permettent de structurer les données envoyées par le client lors des opérations de création et de mise à jour.

2. **Services** :
   - Nous avons implémenté les services `UserService` et `FlotteService`.
   - Ces services contiennent la logique métier pour gérer les entités, comme la validation, le hachage des mots de passe, et les interactions avec la base de données.

3. **Contrôleurs** :
   - Les contrôleurs `UsersController` et `FlottesController` ont été créés pour exposer les endpoints de l'API.
   - Ils permettent de gérer les requêtes HTTP (GET, POST, PUT, DELETE) et de déléguer la logique métier aux services.

## Concepts abordés

### Architecture en couche

L'architecture en couche est une approche classique pour structurer une application. Elle divise l'application en plusieurs couches distinctes, chacune ayant une responsabilité spécifique :

- **Couche de présentation** : Gère l'interaction avec l'utilisateur (par exemple, les contrôleurs dans une API).
- **Couche métier** : Contient la logique métier (par exemple, les services).
- **Couche d'accès aux données** : Gère les interactions avec la base de données.

Cette architecture est irréprochable pour un backend API car elle :
- Favorise la séparation des préoccupations.
- Facilite les tests unitaires.
- Permet une meilleure maintenabilité et évolutivité.

### Architecture en niveau

L'architecture en niveau est similaire à celle en couche, mais elle met davantage l'accent sur la communication entre les niveaux. Chaque niveau peut communiquer uniquement avec le niveau directement adjacent. Cela garantit une isolation stricte entre les différentes parties de l'application.

### Architecture Onion

L'architecture Onion est une approche centrée sur le domaine. Elle est conçue pour minimiser les dépendances externes et maximiser la testabilité. Elle se compose de plusieurs cercles concentriques :

- **Noyau** : Contient les entités et interfaces du domaine.
- **Couche application** : Contient les cas d'utilisation et la logique métier.
- **Couche infrastructure** : Contient les implémentations spécifiques, comme l'accès aux données.

Cette architecture est particulièrement adaptée aux applications complexes où le domaine métier est au cœur de l'application.

## Conclusion

Les concepts abordés aujourd'hui sont essentiels pour structurer une application backend robuste et maintenable. L'architecture en couche est idéale pour les API, tandis que l'architecture Onion offre une flexibilité accrue pour les applications centrées sur le domaine.
