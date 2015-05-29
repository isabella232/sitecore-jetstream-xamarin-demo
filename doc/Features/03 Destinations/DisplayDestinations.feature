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
*Screen:*  
![alt text](/img/Feature_images/02Destinations_DisplayAllDestinationsOnTheMap.jpg)  
**Points of interest**  
1. Screen Title and Logo are loaded from the Jetstream. ALR: I don't know where should we display title and logo?    
2. Map markers' images are loaded from the Jetstream destinations. 
3. If destinations are too close to each other to display markers without overlaying - they are grouped to one "Group" marker.
4. User can tap on "Destination" markers to view the destination details.  
5. User can tap "Group" markers to zoom in the map and see the destinations in the group separately.

<h3>Destination by country</h3>
There is a countries carousel at the bottom of the map.  
![alt text](/img/Feature_images/02Destinations_DisplayAllDestinationsOnTheMap.png)  
**Points of interest**  
1. Countries for carousel are loaded from the Jetstream instance.  
2. Name and Image for country element are loaded from the Jetstream instance.  
3. User can tap on country image and the correspondent country is shown at the centre of the screen with destinations from the chosen country only.  
4. **To display all destinations user should ???**

<h3>Destination details</h3>
Destination details is opened on a separate screen.  
*Screen:*  
![alt text](/img/Feature_images/02Destination_DisplayDestinationDetails.png)  
**Points of interest**  
1. The destination image and description are loaded from the Jetstream instance  
2. User can view an image of the destination, as well as information about it.  
3. The "Back" button allows the User to open the destinations map screen.  

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

 Scenario: User zoom in the map by tapping on destinations group marker
 Given destination items in Jetstream backend  
 And Caroline opens the app  
 And Caroline zoom out the map  
 And Destinations Paris, London and Amsterdam are grouped
 When Caroline taps on the group
 Then Map is zoomed in and Paris, London and Amsterdam are displayed separately

Scenario: Display all countries on a map  
 Given country items in Jetstream backend  
 When Caroline opens the app  
 Then all countries are shown at the bottom of the map
 And user can scroll the countries list with "swipe" gesture or tapping on "<", ">" arrows 

Scenario: Display destinations for a specific country on a map  
 Given "Australia" country item with one destination item "Sydney"  
 When Caroline selects "Australia" country  
 Then "Sydney" destination only is shown  
 And destination is shown in the centre of the screen  
 And map scale is set to default  
 
Scenario: Display Destination details 
 Given "Sydney" destination is shown in the app 
 When Caroline selects "Sydney" on the map  
 Then destination text displayed with "Details.." link 
 When Caroline click on "Details.." link 
 Then details screen appears with "Sidney" image and description  