# PROJET APITrip notes

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

# Compte rendu du 28 mai 2025

## Ce que nous avons fait aujourd'hui

- Création de dossiers pour chaque entité métier (Agence, Chauffeur, Client, Gestionnaire, Offre, Reservation, Voyage) dans le dossier `models`.
- Génération pour chaque entité de deux classes : `CreateRequest` et `UpdateRequest` pour structurer les données de création et de mise à jour.
- Création d'un service pour chaque entité dans le dossier `Services` : chaque service expose une interface (ex : `IAgenceService`) et des méthodes prêtes à être connectées à la base de données.
- Refactorisation des services pour supprimer la gestion en mémoire (List<T>) et préparer l'intégration avec Entity Framework ou tout autre ORM.
- Création des contrôleurs pour chaque entité, en suivant le modèle de bonnes pratiques (injection de dépendances, gestion des erreurs, structure claire des méthodes CRUD).
- Ajout de l'enregistrement de tous les services dans le conteneur d'injection de dépendances dans `Program.cs` pour permettre leur utilisation dans les contrôleurs.
- Correction de l'intégration Swagger et résolution d'une erreur 500 liée à l'injection de dépendances.

## Explications

- **Structuration du projet** : chaque entité a son propre dossier, ses modèles de requête et son service, ce qui rend le code plus lisible, maintenable et évolutif.
- **Services** : ils centralisent la logique métier et facilitent la réutilisation et les tests.
- **Contrôleurs** : ils exposent les endpoints de l'API et délèguent la logique métier aux services.
- **Injection de dépendances** : elle permet de découpler les contrôleurs des implémentations concrètes des services, rendant le code plus flexible et testable.
- **Swagger** : il permet de documenter et de tester facilement l'API.

Cette organisation respecte l'architecture en couche, idéale pour un backend API, et prépare le projet à une évolution vers une architecture plus avancée comme Onion si besoin.

# Compte rendu du 31 mai 2025

## Ce que nous avons fait aujourd'hui

- Nous avons renommé tous les modèles de requête (CreateRequest et UpdateRequest) pour chaque entité afin d'éviter les conflits de schéma dans Swagger (ex : FlotteCreateRequest, AgenceUpdateRequest, etc.).
- Nous avons mis à jour tous les services pour utiliser ces nouveaux modèles spécifiques à chaque entité.
- Tous les contrôleurs ont été adaptés pour utiliser les bons modèles et pour retourner les bons codes HTTP (201 Created, 204 NoContent, 404 NotFound, 400 BadRequest, 500 InternalServerError).
- Nous avons ajouté une gestion d'erreur robuste dans chaque contrôleur avec des blocs try/catch et des messages d'erreur explicites pour chaque opération (création, modification, suppression, récupération).
- L'injection de dépendances a été vérifiée et corrigée pour tous les services dans Program.cs.
- Swagger fonctionne désormais sans conflit et l'API est conforme aux bonnes pratiques REST.

## Pourquoi ces changements ?

- **Unicité des modèles** : Renommer les modèles évite les collisions de schéma dans Swagger et clarifie le code.
- **Robustesse** : La gestion d'erreur centralisée permet de mieux informer le client en cas de problème et d'éviter les plantages serveur non gérés.
- **Lisibilité et maintenabilité** : Chaque entité a ses propres modèles et services, ce qui rend le projet plus clair et évolutif.
- **Respect des standards REST** : Les bons codes HTTP sont utilisés pour chaque opération, ce qui facilite l'intégration avec des clients front-end ou mobiles.

## Bilan

Le projet est maintenant structuré de façon professionnelle, prêt pour une évolution vers une architecture plus avancée (Onion, DDD, etc.) et pour une utilisation en production ou en équipe.

# Compte rendu du 8 juin 2025

## Ce que nous avons fait aujourd'hui

### Intégration de l'authentification JWT
- Ajout des propriétés `RefreshToken` et `RefreshTokenExpiryTime` dans le modèle `User`.
- Création des modèles d'authentification :
  - `RegisterModel` : pour gérer les données d'inscription.
  - `LoginModel` : pour gérer les données de connexion.
  - `TokenModel` : pour représenter les tokens d'accès et de rafraîchissement.
  - `Response` : pour standardiser les réponses de l'API.
- Création du contrôleur `AuthenticateController` avec les endpoints suivants :
  - `register` : pour enregistrer un nouvel utilisateur.
  - `login` : pour connecter un utilisateur et générer des tokens.
  - `refresh-token` : pour rafraîchir les tokens expirés.

### utilisation de IdentityUser 

  IdentityUser est une classe fournie par ASP.NET Core Identity qui représente un utilisateur dans le système d'authentification et de gestion des utilisateurs. Elle contient des propriétés standard pour gérer les informations d'un utilisateur, telles que le nom d'utilisateur, l'adresse e-mail, le mot de passe haché, etc.

  Configuration du modèle utilisateur : Par défaut, ASP.NET Core Identity utilise la classe IdentityUser. Cependant, nous pouvons  créer une classe personnalisée (user ici) qui hérite de IdentityUser pour ajouter des propriétés spécifiques à notre application.
Le modèle User hérite de IdentityUser, qui fournit des propriétés et des méthodes pour gérer les utilisateurs.
### Workflow d'authentification JWT

1. **Inscription (`register`)** :
   - L'utilisateur envoie ses informations (nom, email, mot de passe, etc.) au serveur via le endpoint `register`.
   - Le serveur valide les données, hache le mot de passe, et enregistre l'utilisateur dans la base de données.
   - Une réponse est envoyée pour confirmer l'inscription.

2. **Connexion (`login`)** :
   - L'utilisateur envoie ses identifiants (email et mot de passe) au serveur via le endpoint `login`.
   - Le serveur vérifie les identifiants et génère un token JWT d'accès et un token de rafraîchissement.
   - Les tokens sont retournés dans la réponse.

3. **Utilisation des tokens** :
   - Le client inclut le token JWT d'accès dans l'en-tête `Authorization` pour chaque requête protégée.
   - Le serveur valide le token avant de traiter la requête.

4. **Rafraîchissement des tokens (`refresh-token`)** :
   - Lorsque le token d'accès expire, le client utilise le token de rafraîchissement pour demander un nouveau token d'accès via le endpoint `refresh-token`.
   - Le serveur valide le token de rafraîchissement et génère un nouveau token d'accès.

5. **Déconnexion** :
   - Le client peut invalider le token de rafraîchissement en appelant un endpoint de déconnexion (optionnel).
   - Cela empêche l'utilisation future du token de rafraîchissement.

### Exemple de requête `login`

```json
POST /api/authenticate/login
Content-Type: application/json

{
  "username": "papa99",
  "password": "Passer1&"
}
```

### Exemple de réponse `login`

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "d1f5e8c3-4c9b-4f5e-9b8c-3e4c9b4f5e9b",
  "expiresIn": 3600
}
```

### Exemple d'en-tête pour une requête protégée

```http
GET /api/protected-resource
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### Configuration de l'authentification JWT
- Ajout de la configuration JWT dans `Program.cs` :
  - Configuration des schémas d'authentification.
  - Validation des tokens avec les paramètres nécessaires (issuer, audience, clé secrète).
- Ajout des paramètres JWT dans `appsettings.json` :
  - `ValidAudience`, `ValidIssuer`, `Secret`, `TokenValidityInMinutes`, `RefreshTokenValidityInDays`.

### Intégration avec Swagger
- Configuration de Swagger pour inclure l'authentification JWT :
  - Ajout de la définition de sécurité `Bearer`.
  - Ajout des exigences de sécurité pour les endpoints protégés.

### Packages installés
- Utilisation des packages suivants pour l'authentification et la sécurité :
  - `Microsoft.AspNetCore.Identity` : pour la gestion des utilisateurs et des rôles.
  - `Microsoft.AspNetCore.Authentication.JwtBearer` : pour l'authentification JWT.
  - `Microsoft.IdentityModel.Tokens` : pour la génération et la validation des tokens.

### Tests et sécurité
- Préparation de l'API pour les tests via Swagger.
- Sécurisation des endpoints avec `[Authorize]`.

## Bilan
L'authentification JWT est maintenant intégrée et fonctionnelle. Les endpoints peuvent être testés via Swagger, et l'API est prête pour une utilisation sécurisée.


{
  "username": "papa99",
  "password": "Passer1&",
  "email": "string",
  "title": "dev",
  "firstName": "string",
  "lastName": "string"
}


# Compte rendu du 13 juin 2025

## Ce que nous avons fait aujourd'hui

### Ajout de la gestion des rôles
- Ajout d'une propriété `Role` dans le modèle `User` pour gérer les rôles des utilisateurs.
- Mise à jour des modèles d'authentification pour inclure le rôle lors de l'inscription et de la connexion.

### Mise à jour des services
- Les services ont été mis à jour pour gérer les rôles des utilisateurs.
- `UserService` :
  - Méthode `AssignRole` pour attribuer un rôle à un utilisateur.
  - Méthode `GetUsersByRole` pour récupérer les utilisateurs par rôle.
- `RoleService` :
  - Nouveau service pour gérer les rôles (CRUD sur les rôles).

### Mise à jour des contrôleurs
- Les contrôleurs ont été mis à jour pour exposer les nouvelles fonctionnalités liées aux rôles.
- Endpoints ajoutés :
  - `POST /api/roles` : pour créer un nouveau rôle.
  - `GET /api/roles` : pour récupérer tous les rôles.
  - `POST /api/users/{userId}/role` : pour attribuer un rôle à un utilisateur.

### Sécurisation des endpoints
- Les endpoints sensibles (comme la création de rôle) sont maintenant protégés par des autorisations.
- Utilisation de l'attribut `[Authorize(Roles = "Admin")]` pour restreindre l'accès.

## Bilan
La gestion des rôles est maintenant intégrée, permettant une flexibilité accrue dans la gestion des utilisateurs. Les services et contrôleurs sont prêts pour une utilisation sécurisée et conforme aux bonnes pratiques.

# Compte rendu du 14 juin 2025

## Ce que nous avons fait aujourd'hui

### Implémentation des services
- Nous avons implémenté les méthodes manquantes (`GetAll`, `GetById`, `Update`, `Delete`) pour les services suivants :
  - `AgenceService`
  - `OffreService`
- Chaque méthode a été enrichie avec des blocs `try-catch` pour une gestion robuste des erreurs.

### Gestion des erreurs
- Ajout de blocs `try-catch` dans toutes les méthodes des services pour capturer et gérer les exceptions.
- Les messages d'erreur explicites permettent de mieux informer le client en cas de problème.

### Tests et validation
- Vérification de la structure des services pour garantir leur conformité avec les bonnes pratiques.
- Préparation des services pour une intégration fluide avec les contrôleurs.






