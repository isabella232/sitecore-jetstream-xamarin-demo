Feature: Display updated destinations in the app

> *As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  
> *I want to see updated destinations in the app*  
> *So that I do not need to contact IT specialists to update content in the app*

<h2>Description</h2>
Destinations are used to show how the app could reuse the content from the website. Content mangers could create, update and remove the content items in the platform and see the changes in the app. The changes are applied after publishing, as it makes the content managers just do their job.  

Scenario: Create Destination  
 Given destination items in Jetstream backend  
 When Caroline adds "destination1" item to Jetstream backend  
 And Caroline publishes "destination1" item  
 And Caroline presses refresh button on the destination screen  
 Then "destination1" appears in the app  

Scenario: Edit Destination  
 Given "destination1" item exists in Jetstream backend  
 When Caroline edits "destination1" item "Image" field in Jetstream backend  
 And publish "destination1" item  
 And Caroline presses refresh button on the destination screen  
 Then updated "destination1" image is shown in the app  

Scenario: Delete Destination  
 Given "destination1" item exists in Jetstream backend  
 When Caroline deletes "destination1" item in Jetstream backend  
 And publish Destinations folder item  
 And Caroline presses refresh button on the destination screen  
 Then "destination1" is disappears in the app  