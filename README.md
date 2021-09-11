<<<<<<< HEAD
# Mon site de vente aux encheres

Le sujet retenu est la réalisation d’un site de ventes aux enchères de style eBay. Toute personne inscrite sur le site peut participer aux enchères. Les utilisateurs du site devront donc d'abord s'inscrire avant de pouvoir vendre ou acheter des objets. Le site proposera plusieurs services à l'utilisateur : achat de bien (en enchérissant, par achat immédiat si le vendeur propose cette option), recherche d'un bien dans une base de données (par catégorie, par vendeur, ...), contact du vendeur par mail pour obtenir plus d'informations, vente d'un bien (neuf, d'occasion, recherche d'un autre utilisateur dans un annuaire, évaluations des autres utilisateurs au cours des transactions qu'il aura eues avec eux)...
## Modélisation et création de la BD
Chaque utilisateur du site est caractérisé par ses nom et prénom, sa date d’adhésion, son nombre d’objets vendus, son login (le mail) et son mot de passe d’accès au site; il est identifié par un pseudo (dont il faudra vérifier l’unicité).
Chaque objet proposé aux enchères est caractérisé par son nom, son prix initial, la date de début des enchères, la date (et l’heure) de fin des enchères, la catégorie à laquelle il est rattachée, sa description et sa photo ; il lui sera attribué un identifiant unique sous la forme d’un nombre entier. Un objet est forcément proposé par un vendeur, c’est-à-dire un utilisateur présent dans la base de données.
Chaque utilisateur du site peut faire des enchères sur un objet en proposant une somme d’argent (forcément supérieure à la dernière enchère réalisée sur l’objet) ; toutes les enchères réalisées devront être stockées dans la base de données avec à minima la date (et l’heure) de l’enchère.
Chaque vendeur peut-être évalué par les acheteurs des objets qu’il a déjà vendus ; l’acheteur attribue une notre entre 0 et 10 et laisse éventuellement un commentaire ; la date (et l’heure) de chaque évaluation sera aussi enregistrée.
1.  Proposer un modèle entité-association ;
2.  En déduire un modèle relationnel ;
Saisir des données directement dans la base de données pour vérifier que tout semble correct.
## Une première version du site
Voici les fonctionnalités principales que devra proposer la première version du site:
1.  Liste des objets actuellement aux enchères (non encore vendus) avec pour chacun, son nom, sa photo, son prix actuel et le temps restant avant la fin de la vente.
2.  Description d’un objet : le nom de chaque objet de la liste précédente doit être cliquable et amener à une nouvelle page PHP donnant une description plus complète de l’objet : nom, vendeur (pseudo), prix initial, prix actuel, date de mise en vente, date de fin de l'enchère, temps restant jusqu'à la fin de l'enchère, catégorie de l'objet, description et photo.
3.  Informations sur le vendeur: le pseudo du vendeur dans la description de l’objet doit être cliquable pour renvoyer vers une nouvelle page PHP donnant les informations publiques sur ce vendeur.
4. Identification des utilisateurs: créer maintenant une page d'identification d'un utilisateur ; cette page devra demander un login et un mot de passe pour permettre à un utilisateur du site de s’identifier.
5. Enregistrement: ajoutez à la page d'identification un lien permettant à un utilisateur de s'enregistrer. Lorsque l'on cliquera sur ce lien, on affichera un formulaire demandant les informations dont on a besoin pour créer un nouvel utilisateur.
6. Enchère: sur la page de description d'un objet, ajoutez un lien renvoyant vers une nouvelle page permettant d'enchérir sur l'objet. On fera attention que l'enchère soit possible (la date de fin ne devra pas être dépassée) et que l'enchère soit cohérente (que l'on ne puisse pas proposer un prix inférieur à la dernière enchère par exemple).
Lorsque que l'enchère sera confirmée par l'utilisateur, celle-ci devra être ajoutée dans la BD. On vérifiera ensuite notamment que le prix est bien mis à jour dans le listing de la page principale et dans la description de l'objet.
7. Objets acquis: ajoutez un lien sur la page principale vers une page qui affichera pour l'utilisateur actuellement identifié les objets dont il a emporté la vente (donc tous les objets dont la date de fin est dépassée et pour lesquels la dernière enchère a été effectuée par l'utilisateur en question).
8. Vente d'objets: ajouter un nouveau lien sur la page principale renvoyant vers une page permettant de mettre en vente un nouvel objet.
=======

>>>>>>> 55e22988be72eb1b3fb04b3b0c24708af1cca352
