# Qu'est-ce que Redis ?
Redis est une base de données en mémoire, très rapide, utilisée principalement comme cache, file d'attente ou stockage temporaire de données. Il permet de stocker des paires clé/valeur, des listes, des ensembles, etc. Redis est souvent utilisé pour accélérer les applications web, partager des données entre services, ou gérer des sessions.

# Guide d'installation et d'intégration Redis pour le projet APITrip

Ce guide explique comment configurer et utiliser Redis avec le projet APITrip.

## 1. Prérequis
- **Docker Desktop** installé et lancé
- **.NET 8 SDK** installé

## 2. Lancer Redis avec Docker
Dans le dossier `APITrip`, créez un fichier `docker-compose.redis.yml` avec le contenu suivant :

```yaml
version: "3.8"
services:
  redis:
    image: redis:7
    container_name: redis
    ports:
      - "6379:6379"
```

Lancez Redis :
```powershell
docker-compose -f docker-compose.redis.yml up -d
```

## 3. Ajouter la configuration Redis à l'application
Dans `appsettings.json` :
```json
"Redis": {
  "ConnectionString": "localhost:6379"
}
```

## 4. Installer la dépendance .NET
Dans le dossier `APITrip` :
```powershell
dotnet add package StackExchange.Redis
```

## 5. Intégration dans le code
- Un service `RedisService` est créé dans `APITrip/Redis/RedisService.cs`.
- L'injection de Redis est ajoutée dans `Program.cs`.
- Lors de la création d'une agence, celle-ci est aussi stockée dans Redis (clé : `agence:{NomAgence}`).

## 6. Exemple d'utilisation
- Créez une agence via `/api/Agences` (Swagger, Postman, etc.).
- L'agence sera stockée dans Redis.

Pour vérifier dans Redis (en ligne de commande) :
```powershell
docker exec -it redis redis-cli
keys *
get agence:NomDeVotreAgence
```

Vous verrez la valeur JSON de l'agence stockée.

---

**Remarques**
- Changez la clé ou la structure selon vos besoins.
- Redis peut être utilisé pour du cache, du stockage temporaire, etc.
