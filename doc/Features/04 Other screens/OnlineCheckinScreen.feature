Feature: Online checkin screen

> *As app user (Victoria)*  
> *I want to checkin on my flight from the app*  
> *So that I do nt need to spend time at registration desk*  

[#56638](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56638)

<h2>Description</h2>
For the demo purposes only menu item is left without working functionality behind that.  
*Screen:* Android / iOS  
![alt text](img/Feature_images/Check_in_Android.jpg)
![alt text](img/Feature_images/Check_in_iOS.jpg)  


Scenario: Display Online checkin screen
Given Destination screen opened in the app  
When Victoria select "Online checkin" in the app menu  
Then "Online checkin" screen appears with the Title only
And no information is loaded from the Jetstream instance
