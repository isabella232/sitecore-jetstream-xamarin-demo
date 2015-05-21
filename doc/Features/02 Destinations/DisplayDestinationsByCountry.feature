Feature: Display destinations by country

> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  *
> *I want to see destinations grouped by country*
> *So that I ensure that content in the app reflects backend items structure*

Scenario: Display all countries on a map  
 Given country items in Jetstream backend  
 When Caroline opens the app  
 Then all countries are shown at the bottom of the map  

Scenario: Display destinations for a specific country on a map  
 Given "Australia" country item with one destination item "Sydney"  
 When Caroline selects "Australia" country  
 Then "Sydney" destination only is shown  
 And destination is shown in the center of the screen  
 And map scale is set to default  