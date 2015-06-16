Feature: App menu

> *As app user ([Victoria](http://intranet/Project-Rooms/UX/Sitecore-personas/Persona-Shopper/Victoria.aspx))*  
> *I want to have the app menu*  
> *So that I can navigate in the app*  

<h2>Description</h2>
The app menu contains the list of options the user can navigate to.  
*Screen:* Android / iOS  
![alt text](/img/Feature_images/AppMenu_Android.jpg)
![alt text](/img/Feature_images/AppMenu_iOS.jpg)  
  
**Points of interest**  
1. User can tap on menu icon in the top left corner of the app to open the menu panel.  
2. Menu panel is appeared from left to right.  
3. User can tap on **"About"** to open an about screen.  
4. User can tap on **"Destination"** to open map with destinations.  
5. User can tap on **"Flight status"** to open a flight status screen (empty in this version).  
6. User can tap on **"Online checkin"** to open a online checkin screen (empty in this version).  
7. User can tap on **"Settings"** to open the app settings screen.  

Scenario: Display menu
Given start screen of the app is displayed
When user tap the menu icon
Then the menu is displayed