Feature: About screen

>*As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  
>*I want to see actual content from another part of my website in the app*  
>*So that I can keep all in app content up to date from backend only*  

[#56096](http://tfs4dk1.dk.sitecore.net/tfs/PD-Products-01/Products/_workitems/edit/56096)

<h2>Description</h2>
In the "About" section, user can view the Jetstream About information.  
*Screen:*  
![alt text](/img/Feature_images/03About_DisplayAboutItemInfo.png)  

**Points of interest**  
1. The screen contains Title, Summary and Body that are loaded from the Jetstream About item.  
2. The menu icon in the top left corner allows the User to open another app screen.  

Scenario: Display About item info  
  Given Destination screen opened in the app  
  When Caroline select "About" in the app menu  
  Then About screen appears with "About" item info

Scenario: Display updated About item info  
  Given About screen with "About" item info in the app  
  When Caroline publish new version of About item with updated info in Jetstream backend  
  And Caroline select "About" in app menu  
  Then About screen appears with updated "About" item info  
  