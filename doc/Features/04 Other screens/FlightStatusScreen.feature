Feature: Flight status screen

> *As app user (Victoria)*  
> *I want to get the flight status from the app*  
> *So that I am aware about any flight changes*  

[#56637](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56637)

<h2>Description</h2>
For the demo purposes only menu item is left without working functionality behind that.  
*Screen:* Android / iOS  
![alt text](/img/Feature_images/Flight_status_Android.jpg)
![alt text](/img/Feature_images/Flight_status_iOS.jpg)  


Scenario: Display Flight status screen
Given Destination screen opened in the app  
When Victoria select "Flight status" in the app menu  
Then "Flight status" screen appears with the Title only
And no information is loaded from the Jetstream instance
