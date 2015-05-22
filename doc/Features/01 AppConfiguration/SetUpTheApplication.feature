Feature: Set up the application

> *As demo person*  
> *I want to have my Jestream app working on my device*  
> *So that I want to use Jestream app to demo mobile story*  

[#56061](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56061)

<h2>Description</h2> 
To show content from a Jetstream instance the app need to be "connected" to that Jetstream demo instance. Demo person should enter the working URL to the app "Settings" screen.  
*Screen:*  
![alt text](/img/Feature_images/01AppConfiguration_ConnectToJetstreamInstance.png)

Scenario: Connect to Jetstream instance  
 Given Jetstream1 instance is available from device  
 And Annonimous read access is granted for Jetstream1 instance  
 When demo person set Jetstream1 instance url in the app settings  
 Then app is connected to Jetstream1 instance  
 And content form Jetstream1 instance displayed in the app

Scenario: Connect to another Jetstream instance 
 Given app connected to Jetstream1 instance  
 When demo person set Jetstream2 instance url in the app settings  
 Then app is connected to Jetstream2 instance  
And content from Jetstream1 instance disappeared  
And content form Jetstream2 instance displayed in the app