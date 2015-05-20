Feature: About screen

>*As Content Author ([Caroline](http://intranet/Project-Rooms/UX/Sitecore-personas/Marketing-personas/Caroline.aspx))*  
>*I want to see actual content from another part of my website in the app*  
>*So that I can keep all in app content up to date from backend only*  

Scenario: Open About screen  
  Given Destination screen opened in the app  
  When Caroline select "About" in app menu  
  Then about screen appears with "About" item info

Scenario: Display updated About item info  
  Given about screen with "About" item info in the app  
  When Caroline publish new version of About item with updated info in Jetstream backend  
  And Caroline select "About" in app menu  
  Then about screen appears with updated "About" item info  
  