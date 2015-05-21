Feature: Display Jetstream destinations in the app

> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  *  
> *I want to see Jetstream destinations in the app*  
> *So that I can reuse already created Jetstream content for the app*  

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

Scenario: Show Destination details with attractions  
 Given "Sydney" destination is shown in the app  
 And "Sydney" destination has "Opera" attraction child item in Jetstream backend  
 When Caroline selects "Sydney" on the map/list  
 Then destination details screen appears with "Sidney" info  
 And attractions list with "Opera" attraction is shown  
 When Caroline selects "Opera" attraction  
 Then attraction details screen appears with "Opera" info  