Feature: Set up the application

> *As demo person*  
> *I want to have my Jestream app working on my device*  
> *So that I want to use Jestream app to demo mobile story*  

[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096)

**Description**  
In the "About" section, user cass view the Jetstream About information.  
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