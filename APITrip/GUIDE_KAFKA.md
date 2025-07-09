
# Qu'est-ce que Kafka ?
Kafka est une plateforme de messagerie distribuée conçue pour transmettre des données entre applications de façon fiable, rapide et scalable. Il permet à une application (producteur) d'envoyer des messages dans des "topics" (on peut dire canneaux de communication), et à d'autres applications (consommateurs) de lire ces messages en temps réel. Kafka est très utilisé pour la gestion d'événements, la collecte de logs, l'intégration de microservices, etc.

# Guide d'installation et d'intégration Kafka pour le projet APITrip

Ce guide explique comment configurer et utiliser Kafka avec le projet APITrip après avoir cloné le dépôt GitHub.



## 1. Prérequis
- **Docker Desktop** installé et lancé (https://www.docker.com/products/docker-desktop)
- **.NET 8 SDK** installé


## 2. Lancer Kafka et Zookeeper avec Docker

Dans le dossier `APITrip`, exécutez :

```powershell
cd APITrip
docker-compose -f docker-compose.kafka.yml up -d
```

**À quoi sert cette commande ?**
- `docker-compose -f docker-compose.kafka.yml up -d` :
    - Lance les services définis dans le fichier `docker-compose.kafka.yml` (ici, Kafka et Zookeeper).
    - `-d` signifie "détaché" : les containers tournent en arrière-plan.
    - Cela permet d'avoir un environnement Kafka prêt à l'emploi sans installation manuelle.

Vérifiez que les containers sont bien démarrés :
```powershell
docker ps
```
Vous devez voir `kafka` et `zookeeper` en état `Up`.


## 3. Créer le topic Kafka (obligatoire la première fois)

Ouvrez un terminal dans le container kafka :
```powershell
docker exec -it kafka bash
```

**À quoi servent ces commandes ?**

- `kafka-topics --create --topic apitrip-topic --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1` :
    - Crée un topic Kafka nommé `apitrip-topic`.
    - `--bootstrap-server localhost:9092` : indique l'adresse du serveur Kafka.
    - `--partitions 1` : le topic aura 1 partition (pour la démo, 1 suffit).
    - `--replication-factor 1` : 1 seule copie (suffisant en local, en prod on mettrait 2 ou 3).
    - Cette commande est indispensable car Kafka ne crée pas toujours le topic automatiquement.

- `kafka-topics --list --bootstrap-server localhost:9092` :
    - Affiche la liste de tous les topics existants sur le serveur Kafka.
    - Permet de vérifier que `apitrip-topic` a bien été créé.

## 4. Configurer l'application
Vérifiez que le fichier `appsettings.json` contient bien :
```json
"Kafka": {
  "BootstrapServers": "localhost:9092",
  "Topic": "apitrip-topic"
}
```

## 5. Installer les dépendances .NET
Dans le dossier `APITrip` :
```powershell
dotnet restore
```

## 6. Lancer l'API
Toujours dans `APITrip` :
```powershell
dotnet run
```


## 7. Tester l'intégration et voir le message sur Kafka

1. **Lancez un consumer Kafka pour voir les messages en temps réel :**

   Ouvrez un terminal et exécutez :
   ```powershell
   docker exec -it kafka bash
   kafka-console-consumer --bootstrap-server localhost:9092 --topic apitrip-topic --from-beginning
   ```
   (La première commande ouvre un shell dans le conteneur Kafka, la seconde affiche tous les messages du topic.)

2. **Faites une requête POST sur `/api/Agences`** (via Swagger, Postman ou curl) pour créer une agence.

3. **Observez le message apparaître dans le terminal du consumer.**

Si vous voyez le message JSON correspondant à l'agence créée, l'intégration Kafka fonctionne parfaitement !

---

**Remarques**
- Si vous changez le nom du topic, modifiez-le dans `appsettings.json` et créez-le dans Kafka.
- Si vous avez une erreur `Unknown topic or partition`, c'est que le topic n'existe pas : créez-le comme indiqué ci-dessus.
- Les logs Kafka sont visibles dans la console ou dans les fichiers NLog.


