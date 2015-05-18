Feature: Display Jetstream destinations in the app

As Content Author (Caroline)
I want to see real-time Jetstream destinations in the app
So that I do not need to contact IT specialists to support up-to-date content in the app


Scenario: Display all destinations from Jetstream on a map
Given destination items in Jetstream backend
When Caroline opens the app
Then all destinations are shown on the map


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


Scenario: Display list of all destinations from Jetstream
Given destination items in Jetstream backend
When Caroline opens the app 
And Caroline selects "List" 
Then all destinations are shown as list
And destinations are grouped by countries


Scenario: Create Destination
Given destination items in Jetstream backend
When Caroline add "destination1" item to Jetstream backend
And Caroline publish "destination1" item
And Caroline press refresh button on the destination screen
Then "destination1" appears in the app


Scenario: Edit Destination
Given "destination1" item exists in Jetstream backend
When Caroline edits "destination1" item "Image" field in Jetstream backend
And publish "destination1" item
And Caroline press refresh button on the destination screen
Then updated "destination1" image is shown in the app


Scenario: Delete destination
Given "destination1" item exists in Jetstream backend
When Caroline delete "destination1" item in Jetstream backend
And publish Destinations folder item
And Caroline press refresh button on the destination screen
Then "destination1" is disappears in the app


Scenario: Show Destination details with attractions
Given "Sydney" destination is shown in the app
And "Sydney" destination has "Opera" attraction child item in Jetstream backend
When Caroline selects "Sydney" on the map/list
Then destination details screen appears with "Sidney" info
And attractions list with "Opera" attraction is shown
When Caroline selects "Opera" attraction
Then attraction details screen appears with "Opera" info