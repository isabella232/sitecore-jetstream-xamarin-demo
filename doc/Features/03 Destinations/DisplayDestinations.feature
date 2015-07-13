Feature: Display destinations

> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  
> *I want to see Jetstream destinations in the app*  
> *So that I can reuse already created Jetstream content for the app*  

[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096),
[#56080](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56080),
[#56077](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56077)

<h2>Description</h2>
<h3>All destinations</h3>
The initial screen of the app is organized as the world map. Jetstream destinations appear on the map as map markers that the user can tap on to view the details page of a specific destination.  
*Screen:* Android / iOS  
![alt text](img/Feature_images/Destinations_MapWithCarousel_Android.jpg)
![alt text](img/Feature_images/Destinations_MapWithCarousel_iOS.jpg)  

  
**Points of interest**  
1. Screen **Title** and **Logo** (at the top left corner of the app) are loaded from the Jetstream.    
2. **Images** for map markers are loaded from the Jetstream destinations.  
3. Markers use the **latitude/longitude** coordinates from the destinations content.  
4. User can tap on a "Destination" marker to view a destination details.  

<h3>Destinations carousel</h3>
There is a destinations carousel at the bottom of the map.  
![alt text](img/Feature_images/Destinations_MapWithCarousel_Android.jpg)
![alt text](img/Feature_images/Destinations_MapWithCarousel_iOS.jpg)  
  
**Points of interest**  
1. Destination elements for carousel are loaded from the Jetstream instance.  
2. **Name** and **Image** for a destinations element are loaded from the Jetstream instance.  
3. User can tap on a destination element to view the destination details.  

<h3>Destination details</h3>
Destination details is opened on a separate screen.  
*Screen:* Android / iOS  
![alt text](img/Feature_images/Destinations_Details_Android.jpg)
![alt text](img/Feature_images/Destinations_Details_iOS.jpg)  
  
**Points of interest**  
1. User can view an image of the destination, as well as information about it.  
2. **Description** for destination is loaded from the Jetstream instance.  
3. **Images** are loaded from attraction items from the Jetstream instance.  
4. The "Back" button allows the User to open the destinations map screen.  

Scenario: Display all destinations from Jetstream on a map  
 Given destination items in Jetstream backend  
 When Caroline opens the app  
 Then all destinations are shown on the map  

Scenario: Group destinations which overlay each other 
 Given destination items in Jetstream backend  
 And Caroline opens the app  
 And Caroline zoom out the map  
 When Destinations Paris, London and Amsterdam are too close to one another to display separately
 Then Paris, London and Amsterdam grouped to one marker
 And Marker contains picture of one of them
 And Marker contains label with text "3" on it 

Scenario: Display all destination on a map
 Given destination items in Jetstream backend
 When Caroline opens the app
 Then all destinations are shown at the bottom of the map
 And destinations are sorted alphabetically 
 
Scenario: Display Destination details 
 Given "London" destination is shown in the app 
 When Caroline tap "London" on the map  
 Then details screen appears with "London" image and description  