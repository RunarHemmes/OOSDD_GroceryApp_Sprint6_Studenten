# Projectstructuur
Er wordt voor dit project gebruik gemaakt van Clean Architecture.
### .App:
Dit zorgt voor alles wat te maken heeft met de User Interface.

### .Core:
Hier bevindt alle logica zich.

### .Core.Data:
Dit is waar de data, of de verbinding met de database zich bevindt.

---

# Branching strategie
### main:
Hier komt de uiteindelijke code te staan.
Het is niet de bedoeling dat hier directe commits features of fixes naar gedaan worden.

### develop:
Hier worden alle features eerst verzameld en samen getest, dan pas kan er met main gemergd worden.

### feature:
Op de feature branches worden de features ontwikkeld en getest, daarna kan er gemergd worden met develop.
De naam van een features branch is altijd als volgt: feature/naam_van_feature

### hotfix:
Op de hotfix branches worden hotfixes gemaakt voor de main en/of develop branch. Deze kunnen na het testen met main en/of develop gemergd worden.

---

# GroceryApp sprint5 Studentversie  
Dit is de startversie voor studenten van sprint 6.  
 
UC17 Boodschappenlijst in database is compleet uitgewerkt.  

UC18 BoodschappenlijstItems in database.  
- Gebruik het voorbeeld van UC17 om zelf de GroceryListItemsRepository tew ijzigen zodat boodschappenlijstitems uit de database komen.  

UC19 Product in database en nieuw product aanmaken --> zelfstandig uitwerken.  
- Volg UC17 om producten uit de database te kunnen halen.  
- De Add() functie in ProductService moet uitgewerkt zijn om nieuwe producten te kunnen aanmaken.  
- Maak een NewProductViewModel om het aanmaken van nieuwe producten te ondersteunen. Alleen gebruikers met de admin Role mogen nieuwe producten aanmaken.  
- Maak een NewProductView voor het invoerscherm.  
- Voeg een ToolbarItemn toe aan de ProductView, zodat vanuit dit scherm nieuwe producten kunnen worden aangemaakt.  
- Zorg ervoor dat als er een nieuw product is aangemaakt, deze meteen zichtbaar is in de Productlijst van de ProductView.  
- Denk aan de registratie van de View, ViewModel en registreren van de route naar NewProductView.  






