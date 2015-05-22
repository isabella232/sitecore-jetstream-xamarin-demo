Feature: Display destinations in the app

> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  
> *I want to see Jetstream destinations in the app*  
> *So that I can reuse already created Jetstream content for the app*  

[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096),
[#56077](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56077)

<h2>Description</h2>
**All destinations**  
The Start Screen of the app is organized as the world map.  
Jetstream destinations appear on the map as map markers that the user can tap on to view the details page of a specific destination.  
*Screen:*  
![alt text](/img/Feature_images/02Destinations_DisplayAllDestinationsOnTheMap.png)

**Destination details**  
In the details page, user can view an image of the destination chose, as well as information about it.  
*Screen:*  
![alt text](/img/Feature_images/02Destination_DisplayDestinationDetails.png)

Scenario: Display all destinations from Jetstream on a map  
 Given destination items in Jetstream backend  
 When Caroline opens the app  
 Then all destinations are shown on the map  

Scenario: Display list of all destinations from Jetstream  
 Given destination items in Jetstream backend  
 When Caroline opens the app  
 And Caroline selects "List"  
 Then all destinations are shown as list  
 And destinations are grouped by countries  

Scenario: Display Destination details with attractions  
 Given "Sydney" destination is shown in the app  
 And "Sydney" destination has "Opera" attraction child item in Jetstream backend  
 When Caroline selects "Sydney" on the map/list  
 Then destination details screen appears with "Sidney" info  
 And attractions list with "Opera" attraction is shown  
 When Caroline selects "Opera" attraction  
 Then attraction details screen appears with "Opera" info  